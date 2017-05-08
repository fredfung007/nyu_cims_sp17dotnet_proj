using DataAccessLayer.EF;
using HotelBookingWebsite;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

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
            var context = new HotelModelContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists("staff"))
            {
                var role = new IdentityRole
                {
                    Name = "staff"
                };
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("customer"))
            {
                var role = new IdentityRole
                {
                    Name = "customer"
                };
                roleManager.Create(role);
            }
        }
    }
}