using LoginAPI.Models;

namespace LoginAPI.Repository
{
    public interface IAuthRepository
    {
        string GetSaltByEmail(string email);
        Task<bool> Login(LoginModel request);
       // Task<bool> Register(RegisterModel request, string salt);
        Task<bool> Register(RegisterModel request);
    }
}
