using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UserRolesAddedApp.Startup))]
namespace UserRolesAddedApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
