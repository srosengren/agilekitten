using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AgileKitten.Web.Startup))]
namespace AgileKitten.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
