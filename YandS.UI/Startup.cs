using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YandS.UI.Startup))]
namespace YandS.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
