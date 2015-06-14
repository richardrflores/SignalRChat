using System.Linq;
using System.Threading.Tasks;
using WebGrease.Css.Extensions;

namespace SignalRPrivateChat.Hubs
{
    public class ChatHub : Microsoft.AspNet.SignalR.Hub
    {
        private readonly static ConnectionMapping<string> _connections =
            new ConnectionMapping<string>();

        public void BroadcastMessage(string message)
        {
            var userName = Context.User.Identity.Name;
            Clients.All.postMessage(userName, message);
        }

        public void PointToPointMessage(string groupName, string message)
        {
            var userName = Context.User.Identity.Name;
            Clients.Group(groupName).postMessage(userName, message);
        }

        public async Task JoinGroup(string userToChat)
        {
            var groupName = Context.User.Identity.Name;
            var userToChatConnections = _connections.GetConnections(userToChat);

            userToChatConnections.ForEach(x => Groups.Add(x, groupName));

            await Groups.Add(Context.ConnectionId, groupName);
            Clients.Group(groupName).setGroupName(groupName);
        }

        public Task LeaveGroup(string groupName)
        {
            return Groups.Remove(Context.ConnectionId, groupName);
        }

        public override Task OnConnected()
        {
            var name = Context.User.Identity.Name;

            _connections.Add(name, Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var name = Context.User.Identity.Name;

            _connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            var name = Context.User.Identity.Name;

            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connections.Add(name, Context.ConnectionId);
            }

            return base.OnReconnected();
        }
    }
}