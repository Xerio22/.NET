using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SzkolaWebApp.Startup))]
namespace SzkolaWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
