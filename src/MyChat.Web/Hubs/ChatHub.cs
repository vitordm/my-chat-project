using Microsoft.AspNetCore.SignalR;
using MyChat.Application.Dto.Entities;
using System.Threading.Tasks;

namespace MyChat.Web.Hubs
{
    public class ChatHub : Hub
    {
        public const string GENERAL_ROOM_TAG = "ReceiveMessage";

        public async Task SendMessage(ChatMessageDto message)
        {
            await Clients.All.SendAsync(GENERAL_ROOM_TAG, message);   
        }
    }
}
