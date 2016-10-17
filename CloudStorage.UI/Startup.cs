using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CloudStorage.UI.Startup))]
namespace CloudStorage.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
