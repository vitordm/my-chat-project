using MyChat.Infra.Crosscutting.Entities;
using System;

namespace MyChat.Domain.Entities
{
    public class ChatMessage : IEntity
    {
        public long Id { get; private set; }
        public string UserName { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public string Message { get; private set; }
        public string ChatGroup { get; set; }

        public long AppUserId { get; private set; }

        public ChatMessage()
        {

        }

        public ChatMessage(string userName, DateTime createdAt, string message, string chatGroup, long appUserId)
        {
            UserName = userName;
            CreatedAt = createdAt;
            Message = message;
            ChatGroup = chatGroup;
            AppUserId = appUserId;
        }
    }
}
