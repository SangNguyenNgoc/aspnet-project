using aspdotnet_project.App.User.Dtos;

public interface IUserRepository{
    public Task<List<UserInfo>> GetAllUsers();
    public Task<UserInfo> GetUserById(string userId);
    public Task<UserInfo> GetMyProfile(string userId);
    public Task<bool> UpdateUser(string userId, UserInfo userInfo);

    public Task<bool> ChangeEmail(string userId, string newEmail);

    public Task<bool> ConfirmEmailChange(string token);

    public Task<bool> SendForgotPassword(string email);
    public Task<bool> ForgotPassword(string token, string newPassword, string confirmPassword);

    public Task<bool> UpdateStatus(string userId, int status);

    public Task<bool> ChangePassword(string userId, ChangePasswordRequest changePasswordRequest);

}
