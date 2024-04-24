using exDate.Models;

namespace exDate.Core
{
    public interface ILoginRepository
    {
        Task<object> GetUserDetails();
        Task<object> CreateProductProcess(Login Details);
        Task<object> GetUserEmailValidation(string User_Email);
        Task<Login> AuthenticateAsync(string username, string password);
    }

}
