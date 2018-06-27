using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(myoung_Portfolio2222.Startup))]
namespace myoung_Portfolio2222
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
