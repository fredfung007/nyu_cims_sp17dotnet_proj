using DataAccessLayer.EF;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HotelBookingWebsite.Models
{

    public class ApplicationDbContext : IdentityDbContext<AspNetUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}