using Microsoft.AspNetCore.SignalR;
using MyChat.Application.Dto.Entities;
using MyChat.Application.Dto.Results;
using MyChat.Application.Service.Contracts;
using System;
using System.Threading.Tasks;

namespace MyChat.Web.Hubs
{
    public class ChatHub : Hub
    {
        public const string DEFAULT_MESSAGE_TAG = "ReceiveMessage";
        private readonly IChatService chatService;

        public ChatHub(IChatService chatService)
        {
            this.chatService = chatService;
        }

        public async Task SendMessage(ChatMessageDto message)
        {
            await Clients.All.SendAsync(DEFAULT_MESSAGE_TAG, message);

            var internalAnswer = await ProcessMessage(message);
            if (internalAnswer != null)
                await Clients.All.SendAsync(DEFAULT_MESSAGE_TAG, internalAnswer);

        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToGroup(ChatMessageDto message)
        {
            await Clients.Group(message.ChatGroup).SendAsync(DEFAULT_MESSAGE_TAG, message);
            var internalAnswer = await ProcessMessage(message);
            if (internalAnswer != null)
                await Clients.Group(message.ChatGroup).SendAsync(DEFAULT_MESSAGE_TAG, internalAnswer);

        }

        private async Task<ChatMessageDto> ProcessMessage(ChatMessageDto message)
        {
            try
            {
                ProcessMessageResult processResult = await chatService.ProcessMessageAsync(message);
                return processResult.InternalAnswer;
            }
            catch (Exception e)
            {
                return new ChatMessageDto
                {
                    UserName = "Internal",
                    Message = $"Exception : {e.Message}",
                    CreatedAt = DateTime.Now
                };
            }
        }
    }
}
