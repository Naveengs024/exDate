using exDate.Models;

namespace exDate.Core
{
    public interface  ILoginService
    {
        Task<object> GetUserDetails();
        Task<object> CreateProductProcess(Login Details);
    }
}
