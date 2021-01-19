using MyChat.Domain.Entities;
using MyChat.Infra.Data.Context;
using MyChat.Infra.Data.Contracts;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyChat.Infra.Data.Repositories
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        protected readonly MyChatDbContext dbContext;

        public ChatMessageRepository(MyChatDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ChatMessage> AddAsync(ChatMessage chatMessage)
        {
            await dbContext.ChatMessages.AddAsync(chatMessage);
            await dbContext.SaveChangesAsync();

            return chatMessage;
        }

        public async Task<IList<ChatMessage>> FindLastMessagesAsync(int limit, string group)
            => await dbContext.ChatMessages
                .AsQueryable()
                .Where(cm => cm.ChatGroup == group)
                .OrderByDescending(cm => cm.CreatedAt)
                .Take(limit)
                .ToListAsync();
    }
}
