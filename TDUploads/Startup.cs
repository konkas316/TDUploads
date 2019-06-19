using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TDUploads.Startup))]
namespace TDUploads
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
