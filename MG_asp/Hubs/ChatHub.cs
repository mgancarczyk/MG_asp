using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message) // w momencie gdy po stronie klienta ktos napisze wiadomosc wywolywanme jest send message
        {

            await Clients.All.SendAsync("ReceiveMessage", user, message); // rozsyła wiadomość
        }
    }
}