using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AcmeCorporationWebApp.Startup))]
namespace AcmeCorporationWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
