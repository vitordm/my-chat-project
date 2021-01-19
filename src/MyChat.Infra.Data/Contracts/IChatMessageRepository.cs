using MyChat.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyChat.Infra.Data.Contracts
{
    public interface IChatMessageRepository
    {
        Task<ChatMessage> AddAsync(ChatMessage chatMessage);
        Task<IList<ChatMessage>> FindLastMessagesAsync(int limit, string group);
    }
}
