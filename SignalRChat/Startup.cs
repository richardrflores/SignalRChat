using Owin;

namespace SignalRChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // This is an interface to OWIN, a new standard interface between
            // web servers and web applications. Eliminates dependency on IIS.
            // SignalR uses this; allows for self hosting and other cool stuff.
            appBuilder.MapSignalR();
        } 
    }
}