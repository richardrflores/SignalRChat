using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SignalRPrivateChat.Startup))]
namespace SignalRPrivateChat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
