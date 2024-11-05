using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.User.Entities;
using MovieApp.Domain.User.Repositories;
using MovieApp.Infrastructure.Context;
using MovieApp.Infrastructure.Mail;

namespace MovieApp.Infrastructure.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly MyDbContext _context;
    private readonly UserManager<Domain.User.Entities.User> _userManager;

    public UserRepository(MyDbContext context, UserManager<Domain.User.Entities.User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // public async Task<List<UserInfo>> GetAllUsers()
    // {
    //     return await _context.Users.Select(user => new UserInfo
    //     {
    //         Email = user.Email,
    //         PhoneNumber = user.PhoneNumber,
    //         Avatar = user.Avatar,
    //         DateOfBirth = user.DateOfBirth,
    //         FullName = user.FullName,
    //         Gender = GenderUtil.GetGenderDescription(user.Gender)
    //     }).ToListAsync();
    // }
    //
    // public async Task<UserInfo> GetUserById(string userId)
    // {
    //     var user = await _context.Users.FindAsync(userId);
    //     if (user == null)
    //     {
    //         return null;
    //     }
    //
    //     return new UserInfo
    //     {
    //         Email = user.Email,
    //         PhoneNumber = user.PhoneNumber,
    //         Avatar = user.Avatar,
    //         DateOfBirth = user.DateOfBirth,
    //         FullName = user.FullName,
    //         Gender = GenderUtil.GetGenderDescription(user.Gender)
    //     };
    // }
    //
    // public async Task<UserInfo> GetMyProfile(string userId)
    // {
    //     var user = await _context.Users.FindAsync(userId);
    //     if (user == null)
    //     {
    //         return null;
    //     }
    //
    //     return new UserInfo
    //     {
    //         Email = user.Email,
    //         PhoneNumber = user.PhoneNumber,
    //         Avatar = user.Avatar,
    //         DateOfBirth = user.DateOfBirth,
    //         FullName = user.FullName,
    //         Gender = GenderUtil.GetGenderDescription(user.Gender)
    //     };
    // }

    public Task<List<Domain.User.Entities.User>> GetAllUsers()
    {
        return _context.Users.ToListAsync();
    }

    public async Task<Domain.User.Entities.User?> GetUserById(string userId)
    {
        var user = await _context.Users.FindAsync(userId);
        return user;
    }


    public async Task<Domain.User.Entities.User?> UpdateUser(string userId, Domain.User.Entities.User userInfo)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return null;
        }
        user.PhoneNumber = userInfo.PhoneNumber;
        user.DateOfBirth = userInfo.DateOfBirth;
        user.FullName = userInfo.FullName;
        user.Gender = userInfo.Gender;
        await _context.SaveChangesAsync();
        return user;
    }

    
    // public async Task<bool> UpdateUser(string userId, UserInfo userInfo)
    // {
    //     var user = await _context.Users.FindAsync(userId);
    //     if (user == null)
    //     {
    //         return false;
    //     }
    //
    //     user.PhoneNumber = userInfo.PhoneNumber;
    //     user.DateOfBirth = userInfo.DateOfBirth;
    //     user.FullName = userInfo.FullName;
    //     user.Gender = GenderUtil.GetGenderNum(userInfo.Gender);
    //
    //     await _context.SaveChangesAsync();
    //     return true;
    // }


    public async Task<bool> ChangeEmail(string userId, string newEmail, string token)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return false;
        }
        // Save the token and new email in the database (you might want to create a separate table for this)
        user.ChangeToken = token;
        user.NewEmail = newEmail;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
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
    

    public async Task<bool> SendForgotPassword(string email, string token)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
        {
            return false;
        }
        // Save the token and new email in the database (you might want to create a separate table for this)
        user.ChangeToken = token;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        
        return true;
    }
    
    public async Task<bool> ForgotPassword(string token, string newPassword, string confirmPassword)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.ChangeToken == token);
        if (user == null)
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
    public async Task<bool> ChangePassword(string userId, string oldPassword, string newPassword)
    {
        var user = await _context.Users.FindAsync(userId);
        await _userManager.ChangePasswordAsync(user!, oldPassword, newPassword);
        return true;
    }
}

