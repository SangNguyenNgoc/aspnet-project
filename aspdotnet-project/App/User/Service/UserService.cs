
using aspdotnet_project.App.User.Dtos;
using aspdotnet_project.App.User.Repository;
using aspdotnet_project.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace aspdotnet_project.App.User.Service;

public class UserService{
    private readonly MyDbContext _context;

    public UserService(MyDbContext context){
        _context = context;
    }

    //get all users
    public async Task<List<UserInfo>> GetAllUsers(){
        var users = await _context.Users.ToListAsync();
        return users.Select(user => new UserInfo
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Avatar = user.Avatar,
                DateOfBirth = user.DateOfBirth,
                FullName = user.FullName,
                Gender = user.Gender
            }).ToList();
    }

    //get my profile
    public async Task<UserInfo> GetMyProfile(string userId){
        var user = await _context.Users.FindAsync(userId);
        if (user == null){
            return null;
        }

        return new UserInfo{
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Avatar = user.Avatar,
            DateOfBirth = user.DateOfBirth,
            FullName = user.FullName,
            Gender = user.Gender
        };
    }

    //update user
    public async Task<bool> UpdateUser(string userId, UserInfo userInfo){
        var user = await _context.Users.FindAsync(userId);
        if (user == null){
            return false;
        }

        user.Email = userInfo.Email;
        user.PhoneNumber = userInfo.PhoneNumber;
        user.Avatar = userInfo.Avatar;
        user.DateOfBirth = userInfo.DateOfBirth;
        user.FullName = userInfo.FullName;
        user.Gender = userInfo.Gender;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
