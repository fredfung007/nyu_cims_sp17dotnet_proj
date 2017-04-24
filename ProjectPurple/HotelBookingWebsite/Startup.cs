using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HotelBookingWebsite.Startup))]
namespace HotelBookingWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
