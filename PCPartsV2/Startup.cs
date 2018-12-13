using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using PCPartsV2.Models;

[assembly: OwinStartupAttribute(typeof(PCPartsV2.Startup))]
namespace PCPartsV2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }
        
    }
}
