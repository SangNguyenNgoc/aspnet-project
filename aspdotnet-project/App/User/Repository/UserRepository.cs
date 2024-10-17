using aspdotnet_project.App.User.Dtos;
using aspdotnet_project.App.User.Entities;
using aspdotnet_project.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace aspdotnet_project.App.User.Repository;

public class UserRepository : IUserRepository
{
    private readonly MyDbContext _context;
    private readonly EmailService _emailService;
    private readonly UserManager<User.Entities.User> _userManager;

    public UserRepository(MyDbContext context, EmailService emailService, UserManager<User.Entities.User> userManager)
    {
        _context = context;
        _emailService = emailService;
        _userManager = userManager;
    }

    public async Task<List<UserInfo>> GetAllUsers()
    {
        return await _context.Users.Select(user => new UserInfo
        {
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Avatar = user.Avatar,
            DateOfBirth = user.DateOfBirth,
            FullName = user.FullName,
            Gender = GenderUtil.GetGenderDescription(user.Gender)
        }).ToListAsync();
    }

    public async Task<UserInfo> GetUserById(string userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return null;
        }

        return new UserInfo
        {
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Avatar = user.Avatar,
            DateOfBirth = user.DateOfBirth,
            FullName = user.FullName,
            Gender = GenderUtil.GetGenderDescription(user.Gender)
        };
    }

    public async Task<UserInfo> GetMyProfile(string userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return null;
        }

        return new UserInfo
        {
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Avatar = user.Avatar,
            DateOfBirth = user.DateOfBirth,
            FullName = user.FullName,
            Gender = GenderUtil.GetGenderDescription(user.Gender)
        };
    }

    public async Task<bool> UpdateUser(string userId, UserInfo userInfo)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return false;
        }

        user.PhoneNumber = userInfo.PhoneNumber;
        user.DateOfBirth = userInfo.DateOfBirth;
        user.FullName = userInfo.FullName;
        user.Gender = GenderUtil.GetGenderNum(userInfo.Gender);

        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> ChangeEmail(string userId, string newEmail)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return false;
        }

        // Generate a confirmation token (this is a simple example, you might want to use a more secure method)
        var token = Guid.NewGuid().ToString();

        // Save the token and new email in the database (you might want to create a separate table for this)
        user.ChangeToken = token;
        user.NewEmail = newEmail;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        // Send the confirmation email (this is a placeholder, replace with actual email sending logic)
        await SendConfirmationEmail(newEmail, token);

        return true;
    }

    private async Task SendConfirmationEmail(string email, string token)
    {
        // Implement your email sending logic here
        // For example, you can use an email service to send the email with the confirmation link
        var confirmationLink = $"http://localhost:5295/api/v1/user/confirm-email?token={token}";
        await _emailService.SendEmailAsync(email, "Confirm your new email", $"Please confirm your new email by clicking the following link: {confirmationLink}");
    }

    public async Task<bool> ConfirmEmailChange(string token)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.ChangeToken == token);
        if (user == null)
        {
            return false;
        }

        // Update the email and clear the token and new email fields
        user.Email = user.NewEmail;
        user.ChangeToken = null;
        user.NewEmail = null;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return true;
    }

    // change password
     private async Task SendEmailForgotPassword(string email, string token)
    {
        // Implement your email sending logic here
        // For example, you can use an email service to send the email with the confirmation link
        var confirmationLink = $"http://localhost:5295/api/v1/user/forgot-password?token={token}";
        await _emailService.SendEmailAsync(email, "Confirm your change password", $"Please confirm your new email by clicking the following link: {confirmationLink}");
    }

    public async Task<bool> SendForgotPassword(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
        {
            return false;
        }
        // Generate a confirmation token (this is a simple example, you might want to use a more secure method)
        var token = Guid.NewGuid().ToString();

        // Save the token and new email in the database (you might want to create a separate table for this)
        user.ChangeToken = token;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        await SendEmailForgotPassword(email, token);
        return true;
    }
    
    public async Task<bool> ForgotPassword(string token, string newPassword, string confirmPassword)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.ChangeToken == token);
        if (user == null)
        {
            return false;
        }

        if (newPassword != confirmPassword)
        {
            return false;
        }

        // Update the password
        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);
        // Clear the token
        user.ChangeToken = null;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    //update status
    public async Task<bool> UpdateStatus(string userId, int status)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return false;
        }

        user.Status = status;
        await _context.SaveChangesAsync();
        return true;
    }
    
    //change password
    public async Task<bool> ChangePassword(string userId, ChangePasswordRequest changePasswordRequest)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return false;
        }

        var result = await _userManager.ChangePasswordAsync(user, changePasswordRequest.oldPassword, changePasswordRequest.NewPassword);
        if (!result.Succeeded)
        {
            return false;
        }

        return true;
    }
}

