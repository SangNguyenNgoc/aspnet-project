using System.Data;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MovieApp.Application.Exception;
using MovieApp.Application.Feature.User.Dtos;
using MovieApp.Domain.User.Entities;
using MovieApp.Domain.User.Repositories;
using MovieApp.Infrastructure.Mail;

namespace MovieApp.Application.Feature.User.Service;

public class UserService : IUserService
{
    private readonly EmailService _emailService;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<Domain.User.Entities.User> _userManager;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, EmailService emailService, 
        UserManager<Domain.User.Entities.User> userManager, IMapper mapper)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _userManager = userManager;
        _mapper = mapper;
    }

    //get all users
    public async Task<List<UserInfo>> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();
        return users.Select(user => _mapper.Map<UserInfo>(user)).ToList();
    }

    //get user by id
    public async Task<UserInfo> GetUserById(string userId)
    {
        var user = await _userRepository.GetUserById(userId)
            ?? throw new DataNotFoundException("User not found");
        return _mapper.Map<UserInfo>(user);
    }

    //get my profile
    public async Task<UserInfo> GetMyProfile(string userId)
    {
        var user = await _userRepository.GetUserById(userId)
            ?? throw new DataNotFoundException("User not found");
        return _mapper.Map<UserInfo>(user);;
    }

    //update user
    public async Task<UserInfo> UpdateUser(string userId, UserUpdateRequest userUpdate)
    {
        if (string.IsNullOrEmpty(userId))
        {
            throw new DataNotFoundException("User not found");
        };
        var userEntity = new Domain.User.Entities.User
        {
            PhoneNumber = userUpdate.PhoneNumber,
            DateOfBirth = userUpdate.DateOfBirth,
            FullName = userUpdate.FullName,
            Gender = GenderUtil.GetGenderNum(userUpdate.Gender)
        };
        var result = await _userRepository.UpdateUser(userId, userEntity)
            ?? throw new DataNotFoundException("User not found");
        return _mapper.Map<UserInfo>(result);
    }

    public async Task<bool> ChangeEmail(string userId, string newEmail)
    {
        // Generate a confirmation token (this is a simple example, you might want to use a more secure method)
        var token = Guid.NewGuid().ToString();
        var user = await _userRepository.ChangeEmail(userId, newEmail, token);
        if (!user)
        {
            throw new DataNotFoundException("Data not found");
        }
        // Send the confirmation email (this is a placeholder, replace with actual email sending logic)
        await SendConfirmationEmail(newEmail, token);
        return true;
    }

    //confirm email
    public async Task<bool> ConfirmEmailChange(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            throw new BadRequestException("Request(token) is invalid");
        }
        var result = await _userRepository.ConfirmEmailChange(token);
        if (!result)
        {
            throw new DataNotFoundException("User not found");
        }
        return true;
    }

    public async Task<bool> SendForgotPassword(string email)
    {
        // Generate a confirmation token (this is a simple example, you might want to use a more secure method)
        var token = Guid.NewGuid().ToString();
        var user = await _userRepository.SendForgotPassword(email, token);
        if (!user) throw new DataNotFoundException("User not found.");
        await SendEmailForgotPassword(email, token);
        return true;
    }

    //Forgot password
    public async Task<bool> ForgotPassword(string token, string newPassword, string confirmPassword)
    {
        if (string.IsNullOrEmpty(token)) throw new BadRequestException("Token is invalid or expired.");
        if (newPassword != confirmPassword)
        {
            throw new BadRequestException("New password and confirm password do not match.");
        }
        var result = await _userRepository.ForgotPassword(token, newPassword, confirmPassword);
        if (!result)
        {
            throw new DataException("User not found."); 
        }
        return true;
    }

    //update status
    public async Task<bool> UpdateStatus(string userId, int status)
    {
        await _userRepository.UpdateStatus(userId, status);
        return true;
    }

    //change password
    public async Task<bool> ChangePassword(string userId, ChangePasswordRequest request)
    {
        var user = await _userRepository.GetUserById(userId) 
            ?? throw new DataNotFoundException("User not found");
        if (!await _userManager.CheckPasswordAsync(user, request.OldPassword))
        {
            throw new BadRequestException("Old password's incorrect.");
        }
        if (request.OldPassword == request.NewPassword)
        {
            throw new BadRequestException("Old password and new password are same.");
        }
        if (request.NewPassword != request.ConfirmPassword)
        {
            throw new BadRequestException("Password and confirm password do not match.");
        }
        await _userRepository.ChangePassword(userId, request.OldPassword, request.NewPassword);
        return true;
    }

    //change email
    private async Task SendConfirmationEmail(string email, string token)
    {
        // Implement your email sending logic here
        // For example, you can use an email service to send the email with the confirmation link
        var confirmationLink = $"http://localhost:5295/api/v1/user/confirm-email?token={token}";
        await _emailService.SendEmailAsync(email, "Confirm your new email",
            $"Please confirm your new email by clicking the following link: {confirmationLink}");
    }

    //send change password
    private async Task SendEmailForgotPassword(string email, string token)
    {
        // Implement your email sending logic here
        // For example, you can use an email service to send the email with the confirmation link
        var confirmationLink = $"http://localhost:5295/api/v1/user/forgot-password?token={token}";
        await _emailService.SendEmailAsync(email, "Confirm your change password",
            $"Please confirm your new email by clicking the following link: {confirmationLink}");
    }
}