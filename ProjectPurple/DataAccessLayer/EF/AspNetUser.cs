using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer.EF
{
    public class AspNetUser : IdentityUser
    {
        public DateTime? LoyaltyYear { get; set; }
        public int LoyaltyProgress { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AspNetUser> manager)
        {
            ClaimsIdentity userIdentity =
                await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}