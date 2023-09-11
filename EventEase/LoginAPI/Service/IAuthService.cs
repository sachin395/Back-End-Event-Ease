using LoginAPI.Models;

namespace LoginAPI.Service
{
    public interface IAuthService
    {
        Task<bool> IsCredentialValid(LoginModel request);
        Task<bool> Register(RegisterModel request);
    }
}
