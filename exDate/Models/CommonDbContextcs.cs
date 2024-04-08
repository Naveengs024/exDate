using Microsoft.EntityFrameworkCore;
namespace exDate.Models
{
    public class CommonDbContext:DbContext
    {
        public CommonDbContext(DbContextOptions<CommonDbContext>options):base(options) 
        
        {

        }
        
            public DbSet<Login> UserDetails { get; set; }
        }
    }

