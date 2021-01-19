using MyChat.Application.Dto.Entities;
using MyChat.Application.Dto.Results;
using MyChat.Application.Service.Contracts;
using MyChat.Application.Service.Contracts.Bots;
using MyChat.Application.Service.Services.Bots;
using MyChat.Domain.Entities;
using MyChat.Infra.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyChat.Application.Service.Services
{
    public class ChatService : IChatService
    {
        private readonly IAuthService authService;
        private readonly IChatMessageRepository chatMessageRepository;

        public ChatService(
            IAuthService authService,
            IChatMessageRepository chatMessageRepository
        )
        {
            this.authService = authService;
            this.chatMessageRepository = chatMessageRepository;
        }

        public async Task<IList<ChatMessageDto>> FindLastMessagesAsync(int limit, string group)
        {
            var entities = await chatMessageRepository.FindLastMessagesAsync(limit, group);

            return entities
                .OrderBy(e => e.CreatedAt)
                .Select(e => new ChatMessageDto
                {
                    Id = e.Id,
                    UserName = e.UserName,
                    CreatedAt = e.CreatedAt,
                    Message = e.Message,
                    ChatGroup = e.ChatGroup
                })
                .ToList();

        }

        public async Task SaveNewMessageAsync(ChatMessageDto chatMessage)
        {
            var user = await authService.GetUserByUserNameAsync(chatMessage.UserName);

            if (string.IsNullOrEmpty(chatMessage.ChatGroup))
                chatMessage.ChatGroup = null;

            var entity = new ChatMessage(
                chatMessage.UserName,
                chatMessage.CreatedAt,
                chatMessage.Message,
                chatMessage.ChatGroup,
                user.Id
            );

            await chatMessageRepository.AddAsync(entity);
        }

        public async Task<ProcessMessageResult> ProcessMessageAsync(ChatMessageDto message)
        {
            var result = new ProcessMessageResult
            {
                OriginalMessage = message
            };

            var checkBotMessageRegex = new Regex(@"^\/\w+(\=.*)?$", RegexOptions.IgnoreCase);

            var matchBotRequest = checkBotMessageRegex.Match(message.Message);

            if (matchBotRequest.Success)
            {
                result.IsForBot = true;
                result.InternalAnswer = await ProcessBotAnswer(message);
            }

            if (!result.IsForBot)
            {
                await SaveNewMessageAsync(message);
            }

            return result;

        }

        private async Task<ChatMessageDto> ProcessBotAnswer(ChatMessageDto message)
        {
            var botCall = Regex.Match(message.Message, @"^\/\w+", RegexOptions.Compiled | RegexOptions.IgnoreCase)
              .Captures
              .FirstOrDefault()
              ?.Value;

            var botParameter = Regex.Match(message.Message, @"\=.*$", RegexOptions.Compiled | RegexOptions.IgnoreCase)
                .Captures
                .FirstOrDefault()
                ?.Value;
            if (!string.IsNullOrEmpty(botParameter))
                botParameter = Regex.Replace(botParameter, @"^\=", "", RegexOptions.IgnoreCase);

            IChatBot chatBot;

            switch (botCall)
            {
                case "/stock":
                    chatBot = new StockBot();
                    break;

                default:
                    throw new InvalidOperationException("Unknow Bot Caller!");
            }

            string botResponse = await chatBot.CallAsync(botParameter);

            return new ChatMessageDto
            {
                UserName = "Bot",
                Message = botResponse
            };

        }
    }
}
