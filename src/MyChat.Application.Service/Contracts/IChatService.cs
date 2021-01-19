using MyChat.Application.Dto.Entities;
using MyChat.Application.Dto.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyChat.Application.Service.Contracts
{
    public interface IChatService
    {
        Task<IList<ChatMessageDto>> FindLastMessagesAsync(int limit, string group);
        Task SaveNewMessageAsync(ChatMessageDto chatMessage);
        Task<ProcessMessageResult> ProcessMessageAsync(ChatMessageDto message);
    }
}
