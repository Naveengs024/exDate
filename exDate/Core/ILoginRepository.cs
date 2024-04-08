using exDate.Models;

namespace exDate.Core
{
    public interface ILoginRepository
    {
        Task<object> GetUserDetails();
        Task<object> CreateProductProcess(Login Details);
    }

}
