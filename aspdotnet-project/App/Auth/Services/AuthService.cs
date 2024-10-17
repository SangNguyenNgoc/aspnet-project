using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using aspdotnet_project.App.Auth.Dtos;
using aspdotnet_project.App.User.Entities;
using AutoMapper;
using course_register.API.Dtos;
using course_register.API.Exception;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace aspdotnet_project.App.Auth.Services;

public class AuthService : IAuthService
{
    private readonly SymmetricSecurityKey _key;
    private readonly IConfiguration _config;
    private readonly UserManager<User.Entities.User> _userManager;
    private readonly SignInManager<User.Entities.User> _signInManager;
    private readonly IMapper _mapper;

    public AuthService(IConfiguration config, UserManager<User.Entities.User> userManager, SignInManager<User.Entities.User> signInManager, IMapper mapper)
    {
        _config = config;
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"] ?? "default-key")); 

    }
    
    public async Task<AuthResponse> Register(RegisterRequest request)
    {
        var newUser = new User.Entities.User
        {
            FullName = request.FullName,
            Email = request.Email,
            UserName = request.Email,
            CreateDate = DateTime.Now,
            Avatar = _config["Url:DefaultAvatar"] ?? "",
            Gender = Gender.Unknown,
            Status = 1
        };
        var createdUser = await _userManager.CreateAsync(newUser, request.Password!);

        if (!createdUser.Succeeded)
        {
            throw new BadRequestException("Register failure",createdUser.Errors.Select(e => e.Description).ToList());
        }
        var roleResult = await _userManager.AddToRoleAsync(newUser, "User");
        if (!roleResult.Succeeded)
        {
            throw new AppException("Register failure", roleResult.Errors.Select(e => e.Description).ToList(), 500);
        }
        var response = _mapper.Map<AuthResponse>(newUser);
        response.Token = await GenerateToken(newUser);
        return response;
    }
    
    public async Task<AuthResponse> Login(LoginRequest request)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        //check status
        if (user == null)
        {
            throw new BadRequestException("Invalid email or password");
        }
        // Check if the user is locked out
        if (user.Status != 1)
        {
            throw new BadRequestException("User is locked");
        }
        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password!, false);
        if (!result.Succeeded)
        {
            throw new BadRequestException("Invalid email or password");
        }
        var response = _mapper.Map<AuthResponse>(user);
        response.Token = await GenerateToken(user);
        return response;
    }
    
    
    public async Task<string> GenerateToken(User.Entities.User user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        // Tạo claims
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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