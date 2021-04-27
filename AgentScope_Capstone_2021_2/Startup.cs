using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AgentScope_Capstone_2021_2.Startup))]
namespace AgentScope_Capstone_2021_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
