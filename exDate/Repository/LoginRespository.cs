using exDate.Core;
using exDate.Models;
using exDate.Service;
using Microsoft.EntityFrameworkCore;

namespace exDate.Repository
{
    public class LoginRespository: ILoginRepository
    {
        public readonly CommonDbContext _context;


        public LoginRespository(CommonDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetUserDetails()
        {
            try
            {
                var result = await _context.UserDetails.Where(c => c.IsDeleted == false).ToListAsync();
                return result;
            }
            catch (Exception ex) {
                return ex;
            }
        }
        public async Task<object> GetUserEmailValidation(string User_Email)  
        {
            try
            {
                var result = await _context.UserDetails.Where(c => c.IsDeleted == false && c.User_Email == User_Email).FirstOrDefaultAsync();
                if (result != null)
                {
                    return new { StatusCode = 2, Message = $" {result.User_Email} Already Exists! Please Try With Other EmailId" };
                } else {
                    return new { StatusCode = 1, Message = $" {User_Email} You can Create Account" };
            } }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<object> CreateProductProcess(Login Details)
        {
           
            
            var UserEmail = await _context.UserDetails.FirstOrDefaultAsync(e => e.User_Email == Details.User_Email);

            if(UserEmail!= null)
            {
                return new { StatusCode = 2, Message = $" {Details.User_Email} Already Exists! Please Try With Other EmailId" };
            }
            else
            {
                try
                {
                    var AddDetails = new Login()
                    {
                       Address = Details.Address,   
                       State_Id = Details.State_Id, 
                       Country_Id = Details.Country_Id,
                       CreatedDate = DateTime.Now,
                       ModifiedDate= DateTime.Now,
                       District_Id = Details.District_Id,
                       IsDeleted= Details.IsDeleted,
                       Mobile_No = Details.Mobile_No,
                       Password= Details.Password,
                       RememberMe= Details.RememberMe,
                       User_Email= Details.User_Email,
                       User_Name= Details.User_Name,
                    };

                    var result = _context.UserDetails.Add(AddDetails);
                    await _context.SaveChangesAsync();
                    return new { StatusCode = 1, Message = "Account Created successfully" };
                }
                catch (Exception ex)
                {
                    return ex;

                }
            }
           
        }
        public async Task<Login> AuthenticateAsync(string username, string password)
        {
            var user = _context.UserDetails.FirstOrDefault(c => c.User_Name == username);
            if (user != null && user.Password == password)
            {
                return user;
            }

            // Return null if authentication fails
            return null;
        }



    }
}
