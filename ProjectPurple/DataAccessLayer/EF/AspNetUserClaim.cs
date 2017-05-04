using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer.EF
{
    public class AspNetUserClaim : IdentityUserClaim
    {
        public virtual AspNetUser AspNetUser { get; set; }
    }
}