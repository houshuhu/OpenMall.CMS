using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CMS.Web.Startup))]
namespace CMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
