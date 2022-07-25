using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MySchoolProj.Startup))]
namespace MySchoolProj
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
