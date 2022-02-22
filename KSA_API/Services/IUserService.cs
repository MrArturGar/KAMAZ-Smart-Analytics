using KSA_API.Models;

namespace KSA_API.Services
{
    public interface IUserService
    {
        bool IsValidUserInformation(LoginModel model);
    }
}
