namespace SignalRChat.Hubs
{
    public class ChatHub : Microsoft.AspNet.SignalR.Hub
    {
        public void BroadcastMessage(string message)
        {
            Clients.All.postMessage(message);
        }
    }
}