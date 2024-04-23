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

        public async Task<object> CreateProductProcess(Login Details)
        {
           
            var Username = await _context.UserDetails.FirstOrDefaultAsync(e => e.User_Name == Details.User_Name);
            var UserEmail = await _context.UserDetails.FirstOrDefaultAsync(e => e.User_Email == Details.User_Email);

            if (Username != null)
            {
                return new { StatusCode = 2, Message = $" {Details.User_Name} Already Exists! Please Try With Other Name" };
            }
            else if(UserEmail!= null)
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


    }
}
