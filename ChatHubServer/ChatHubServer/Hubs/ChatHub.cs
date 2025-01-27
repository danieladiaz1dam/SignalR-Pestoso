using ENT;
using Microsoft.AspNetCore.SignalR;

namespace ChatHubServer.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Msg m)
        {
            //await Clients.All.SendAsync("ReceiveMessage", m);
            await Clients.Others.SendAsync("ReceiveMessage", m);
        }
    }
}
