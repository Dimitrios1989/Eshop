using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Eshop.Administration.Startup))]
namespace Eshop.Administration
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
