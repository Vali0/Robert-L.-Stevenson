using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JobHunters.WebFormsClient.Startup))]
namespace JobHunters.WebFormsClient
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
