using MyChat.Application.Dto.Entities;
using MyChat.Application.Service.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyChat.Test.Services
{
    public class ChatServiceTest
    {
        [Fact]
        public void FalseBot()
        {

            Func<Task> act = async () =>
            {
                var chatService = new ChatService(null, null);
                var chatMessage = new ChatMessageDto
                {
                    ChatGroup = null,
                    Message = "/unknowBot=paramIrrelevant",
                    UserName = "ignoredUser"
                };

                await chatService.ProcessMessageAsync(chatMessage);
            };


            Assert.ThrowsAsync<InvalidOperationException>(act);
        }
    }
}
