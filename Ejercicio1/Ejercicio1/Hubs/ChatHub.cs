using Microsoft.AspNetCore.SignalR;
using ENT;

namespace Ejercicio1.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Msg msg)
        {
            await Clients.All.SendAsync("ReceiveMessage", msg);
        }
    }
}
