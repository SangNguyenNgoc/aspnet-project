using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Application.Exception;
using MovieApp.Domain.User.Entities;
using MovieApp.Identity.Dtos;

namespace MovieApp.Identity.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;
    private readonly IMapper _mapper;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AuthService(IConfiguration config, UserManager<User> userManager,
        SignInManager<User> signInManager, IMapper mapper)
    {
        _config = config;
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"] ?? "default-key"));
    }

    public async Task<AuthResponse> Register(RegisterRequest request)
    {
        var newUser = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            UserName = request.Email,
            CreateDate = DateTime.Now,
            Avatar = _config["Url:DefaultAvatar"] ?? "",
            Gender = Gender.Unknown,
            Status = 1,
        };
        var createdUser = await _userManager.CreateAsync(newUser, request.Password!);

        if (!createdUser.Succeeded)
            throw new BadRequestException("Register failure", createdUser.Errors.Select(e => e.Description).ToList());
        var roleResult = await _userManager.AddToRoleAsync(newUser, "User");
        if (!roleResult.Succeeded)
            throw new AppException("Register failure", roleResult.Errors.Select(e => e.Description).ToList(), 500);
        var response = _mapper.Map<AuthResponse>(newUser);
        response.Token = await GenerateToken(newUser, null);
        response.Roles = ["User"];
        return response;
    }

    public async Task<AuthResponse> Login(LoginRequest request)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        //check status
        if (user == null) throw new BadRequestException("Invalid email or password");
        // Check if the user is locked out
        if (user.Status != 1) throw new BadRequestException("User is locked");
        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password!, false);
        if (!result.Succeeded) throw new BadRequestException("Invalid email or password");
        var response = _mapper.Map<AuthResponse>(user);
        var roles = await _userManager.GetRolesAsync(user);
        response.Token = await GenerateToken(user, roles);
        response.Roles = roles;
        return response;
    }

    public async Task<string> GenerateToken(User user, IList<string>? roles)
    { // Tạo claims
        roles ??= await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Name, user.UserName!),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credentials,
            Issuer = _config["JWT:Issuer"],
            Audience = _config["JWT:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}