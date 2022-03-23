using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCPresentationWithIdentity.Startup))]
namespace MVCPresentationWithIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
