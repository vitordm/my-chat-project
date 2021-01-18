using System;
using System.Collections.Generic;
using System.Text;

namespace MyChat.Application.Dto.Entities
{
    public class ChatMessageDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Message { get; set; }

    }
}
