using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(rsplsdeathmatchService.Startup))]

namespace rsplsdeathmatchService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}