using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(databaseConnectionTest.Startup))]
namespace databaseConnectionTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
