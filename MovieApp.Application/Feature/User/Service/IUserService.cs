using MovieApp.Application.Feature.User.Dtos;

namespace MovieApp.Application.Feature.User.Service;

public interface IUserService
{
    public Task<List<UserInfo>> GetAllUsers();
    public Task<UserInfo> GetUserById(string userId);
    public Task<UserInfo> GetMyProfile(string token);
    public Task<UserInfo> UpdateUser(string userId, UserUpdateRequest userUpdate);

    public Task<bool> ChangeEmail(string userId, string newEmail);

    public Task<bool> ConfirmEmailChange(string token);

    public Task<bool> SendForgotPassword(string email);

    public Task<bool> ForgotPassword(string token, string newPassword, string confirmPassword);

    public Task<bool> UpdateStatus(string userId, int status);

    public Task<bool> ChangePassword(string userId, ChangePasswordRequest request);
}