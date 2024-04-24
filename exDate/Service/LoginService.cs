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
        public async Task<object> GetUserEmailValidation(string User_Email)
        {
            return await _repository.GetUserEmailValidation(User_Email);
        }
        public async Task<Login> AuthenticateAsync(string username, string password)
        {
            return await _repository.AuthenticateAsync(username, password);
        }
    }
}
