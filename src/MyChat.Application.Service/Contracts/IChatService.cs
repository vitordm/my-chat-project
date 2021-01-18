using MyChat.Application.Dto.Entities;
using System.Threading.Tasks;

namespace MyChat.Application.Service.Contracts
{
    public interface IChatService
    {
        public Task NewMessageAsync(ChatMessageDto chatMessage);
    }
}
