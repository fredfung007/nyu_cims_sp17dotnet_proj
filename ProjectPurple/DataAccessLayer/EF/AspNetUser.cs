using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer.EF
{
    public class AspNetUser : IdentityUser
    {
        public AspNetUser()
        {
            Profile = new Profile();
            Reservations = new HashSet<Reservation>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AspNetUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public DateTime? LoyaltyYear { get; set; }
        public int LoyaltyProgress { get; set; }
        public Guid ProfileGuid { get; set; }

        public virtual Profile Profile { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}