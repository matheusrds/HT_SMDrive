using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SMDriveV2.Startup))]
namespace SMDriveV2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
