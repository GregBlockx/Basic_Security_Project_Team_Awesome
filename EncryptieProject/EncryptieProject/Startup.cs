using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EncryptieProject.Startup))]
namespace EncryptieProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
