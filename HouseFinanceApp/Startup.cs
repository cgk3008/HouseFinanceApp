using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HouseFinanceApp.Startup))]
namespace HouseFinanceApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
