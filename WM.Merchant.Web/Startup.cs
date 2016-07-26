using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WM.Merchant.Web.Startup))]
namespace WM.Merchant.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
