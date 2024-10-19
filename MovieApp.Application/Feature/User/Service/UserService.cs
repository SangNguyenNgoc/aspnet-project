using MovieApp.Application.Feature.User.Dtos;
using MovieApp.Domain.User.Repositories;

namespace MovieApp.Application.Feature.User.Service;

public class UserService : IUserService{
    private readonly IUserRepository _userRepository;


    public UserService(IUserRepository userRepository){
        _userRepository = userRepository;
    }

    //get all users
    public async Task<List<UserInfo>> GetAllUsers(){
        var users = await _userRepository.GetAllUsers();
        // return users;
        return null;
    }

    //get user by id
    public async Task<UserInfo> GetUserById(string userId){
        var user = await _userRepository.GetUserById(userId);
        // return user;
        return null;
    }

    //get my profile
    public async Task<UserInfo> GetMyProfile(string userId){
        var user = await _userRepository.GetMyProfile(userId);
        // return user;
        return null;
    }

    //update user
    public async Task<bool> UpdateUser(string userId, UserInfo userInfo){
        if (string.IsNullOrEmpty(userId)){
            return false;
        }
        // var user = await _userRepository.UpdateUser(userId, userInfo);
        return true;
    }

    //change email
    public async Task<bool> ChangeEmail(string userId, string newEmail){
        if (string.IsNullOrEmpty(userId)){
            return false;
        }
        var user = await _userRepository.ChangeEmail(userId, newEmail);
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
    public async Task<bool> SendForgotPassword(string email){
        if (string.IsNullOrEmpty(email)){
            return false;
        }
        var user = await _userRepository.SendForgotPassword(email);
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
    public async Task<bool> ChangePassword(string userId, ChangePasswordRequest changePasswordRequest){
        if (string.IsNullOrEmpty(userId)){
            return false;
        }
        // var user = await _userRepository.ChangePassword(userId, changePasswordRequest);
        return true;
    }
}
