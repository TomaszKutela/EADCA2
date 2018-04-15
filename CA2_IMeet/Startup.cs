using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CA2_IMeet.Startup))]
namespace CA2_IMeet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
