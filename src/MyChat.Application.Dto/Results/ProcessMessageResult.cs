using MyChat.Application.Dto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChat.Application.Dto.Results
{
    public class ProcessMessageResult
    {
        public bool IsForBot { get; set; }
        public ChatMessageDto OriginalMessage { get; set; }
        public ChatMessageDto InternalAnswer { get; set; }
    }
}
