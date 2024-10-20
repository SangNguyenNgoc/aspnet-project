namespace MovieApp.Domain.User.Repositories;

public interface IUserRepository{
    public Task<List<Entities.User>> GetAllUsers();
    public Task<Entities.User?> GetUserById(string userId);
    public Task<Entities.User> GetMyProfile(string userId);
    public Task<bool> UpdateUser(string userId, Entities.User userInfo);

    public Task<bool> ChangeEmail(string userId, string newEmail);

    public Task<bool> ConfirmEmailChange(string token);

    public Task<bool> SendForgotPassword(string email);
    public Task<bool> ForgotPassword(string token, string newPassword, string confirmPassword);

    public Task<bool> UpdateStatus(string userId, int status);

    public Task<bool> ChangePassword(string userId, string changePasswordRequest);

}