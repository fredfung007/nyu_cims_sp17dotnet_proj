using DataAccessLayer.EF;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(HotelBookingWebsite.Startup))]
namespace HotelBookingWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        private void CreateRolesandUsers()
        {
            HotelModelContext context = new HotelModelContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists("staff"))
            {
                var role = new IdentityRole();
                role.Name = "staff";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("customer"))
            {
                var role = new IdentityRole();
                role.Name = "customer";
                roleManager.Create(role);

            }
        }
    }
}
