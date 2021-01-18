using MyChat.Application.Dto.Entities;
using MyChat.Application.Service.Contracts;
using System.Threading.Tasks;

namespace MyChat.Application.Service.Services
{
    public class ChatService : IChatService
    {
        public async Task NewMessageAsync(ChatMessageDto chatMessage)
        {
            await Task.Delay(1);
        }
    }
}
