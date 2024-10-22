using System.Security.Claims;
using MovieApp.Application.Feature.User.Dtos;
using MovieApp.Domain.User.Entities;
using MovieApp.Domain.User.Repositories;
using MovieApp.Infrastructure.Mail;

namespace MovieApp.Application.Feature.User.Service;

public class UserService : IUserService{
    private readonly IUserRepository _userRepository;
    private readonly EmailService _emailService;

    public UserService(IUserRepository userRepository, EmailService emailService){
        _userRepository = userRepository;
        _emailService = emailService;
    }

    //get all users
    public async Task<List<UserInfo>> GetAllUsers(){
        var users = await _userRepository.GetAllUsers();
        return users.Select(user => new UserInfo{
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Avatar = user.Avatar,
            DateOfBirth = user.DateOfBirth,
            FullName = user.FullName,
            Gender = GenderUtil.GetGenderDescription(user.Gender)
        }).ToList();
    }

    //get user by id
    public async Task<UserInfo> GetUserById(string userId){
        var user = await _userRepository.GetUserById(userId);
        if (user == null){
            return null;
        }
        return new UserInfo{
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Avatar = user.Avatar,
            DateOfBirth = user.DateOfBirth,
            FullName = user.FullName,
            Gender = GenderUtil.GetGenderDescription(user.Gender)
        };
    }

    //get my profile
    public async Task<UserInfo> GetMyProfile(string userId){
        
        if (string.IsNullOrEmpty(userId)){
            return null;
        }
        var user = await _userRepository.GetUserById(userId);
        return new UserInfo{
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Avatar = user.Avatar,
            DateOfBirth = user.DateOfBirth,
            FullName = user.FullName,
            Gender = GenderUtil.GetGenderDescription(user.Gender)
        };
    }

    //update user
    public async Task<bool> UpdateUser(string userId, UserUpdateRequest userUpdate){
        if (string.IsNullOrEmpty(userId)){
            return false;
        }
        var userEntity = new MovieApp.Domain.User.Entities.User
        {
            PhoneNumber = userUpdate.PhoneNumber,
            DateOfBirth = userUpdate.DateOfBirth,
            FullName = userUpdate.FullName,
            Gender = GenderUtil.GetGenderNum(userUpdate.Gender)
        };
        var result = await _userRepository.UpdateUser(userId, userEntity);
        return true;
    }

    //change email
    private async Task SendConfirmationEmail(string email, string token)
    {
        // Implement your email sending logic here
        // For example, you can use an email service to send the email with the confirmation link
        var confirmationLink = $"http://localhost:5295/api/v1/user/confirm-email?token={token}";
        await _emailService.SendEmailAsync(email, "Confirm your new email", $"Please confirm your new email by clicking the following link: {confirmationLink}");
    }
    public async Task<bool> ChangeEmail(string userId, string newEmail){
        if (string.IsNullOrEmpty(userId)){
            return false;
        }
        if (string.IsNullOrEmpty(newEmail)){
            return false;
        }
        // Generate a confirmation token (this is a simple example, you might want to use a more secure method)
        var token = Guid.NewGuid().ToString();
        var user = await _userRepository.ChangeEmail(userId, newEmail, token);
        if (!user){
            return false;
        }
        // Send the confirmation email (this is a placeholder, replace with actual email sending logic)
        await SendConfirmationEmail(newEmail, token);
        return true;
    }

    //confirm email
    public async Task<bool> ConfirmEmailChange(string token){
        if (string.IsNullOrEmpty(token)){
            return false;
        }
        var user = await _userRepository.ConfirmEmailChange(token);
        return true;
    }

    //send change password
    private async Task SendEmailForgotPassword(string email, string token)
    {
        // Implement your email sending logic here
        // For example, you can use an email service to send the email with the confirmation link
        var confirmationLink = $"http://localhost:5295/api/v1/user/forgot-password?token={token}";
        await _emailService.SendEmailAsync(email, "Confirm your change password", $"Please confirm your new email by clicking the following link: {confirmationLink}");
    }
    public async Task<bool> SendForgotPassword(string email){
        if (string.IsNullOrEmpty(email)){
            return false;
        }
        // Generate a confirmation token (this is a simple example, you might want to use a more secure method)
        var token = Guid.NewGuid().ToString();
        var user = await _userRepository.SendForgotPassword(email, token);
        if (!user){
            return false;
        }
        await SendEmailForgotPassword(email, token);
        return true;
    }

    //Forgot password
    public async Task<bool> ForgotPassword(string token, string newPassword, string confirmPassword){
        if (string.IsNullOrEmpty(token)){
            return false;
        }
        var user = await _userRepository.ForgotPassword(token, newPassword, confirmPassword);
        return true;
    }

    //update status
    public async Task<bool> UpdateStatus(string userId, int status){
        if (string.IsNullOrEmpty(userId)){
            return false;
        }
        var user = await _userRepository.UpdateStatus(userId, status);
        return true;
    }

    //change password
    public async Task<bool> ChangePassword(string userId, string oldPassword, string newPassword, string confirmPassword){
        if (string.IsNullOrEmpty(userId)){
            return false;
        }
        if (string.IsNullOrEmpty(oldPassword) || oldPassword == newPassword){
            return false;
        }
        if (newPassword != confirmPassword){
            return false;
        }

        var user = await _userRepository.ChangePassword(userId, oldPassword, newPassword);
        return true;
    }
}
