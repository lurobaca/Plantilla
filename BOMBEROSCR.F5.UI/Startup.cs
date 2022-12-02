using Microsoft.Owin;
using Owin;


[assembly: OwinStartupAttribute(typeof(BOMBEROSCR.F5.UI.Startup))]

namespace BOMBEROSCR.F5.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
