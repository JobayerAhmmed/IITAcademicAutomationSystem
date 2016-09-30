using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IITAcademicAutomationSystem.Startup))]
namespace IITAcademicAutomationSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
