using exDate.Core;
using exDate.Models;

namespace exDate.Service
{
    public class LoginService:ILoginService
    {
        
        private readonly ILoginRepository _repository;

        public LoginService(ILoginRepository Login)
        {
            _repository = Login;
        }
       public async  Task<object> GetUserDetails()
        {
            return await _repository.GetUserDetails();  
        }
        public async Task<object> CreateProductProcess(Login Details)
        {
            return await _repository.CreateProductProcess(Details); 
        }
    }
}
