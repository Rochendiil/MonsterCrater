using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Monster_Crater.Startup))]
namespace Monster_Crater
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
