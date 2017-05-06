using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(testEF.Startup))]
namespace testEF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
