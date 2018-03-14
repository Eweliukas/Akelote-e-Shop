using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Akelote_e_Shop.Startup))]
namespace Akelote_e_Shop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
