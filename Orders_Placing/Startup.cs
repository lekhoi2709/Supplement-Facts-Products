using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Orders_Placing.Startup))]
namespace Orders_Placing
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
