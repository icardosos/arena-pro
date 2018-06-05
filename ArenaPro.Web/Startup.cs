using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArenaPro.Web.Startup))]
namespace ArenaPro.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
