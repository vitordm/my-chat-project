using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyChat.Application.Dto.Entities;
using MyChat.Application.Service.Contracts;
using MyChat.Domain.Auth;
using MyChat.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MyChat.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChatService chatService;
        private readonly ILogger<HomeController> logger;

        public HomeController(
            IChatService chatService,
            ILogger<HomeController> logger
        )
        {
            this.chatService = chatService;
            this.logger = logger;
        }

        public async Task<IActionResult> Index([FromServices] IAuthService authService)
        {
            if (!User.Identity.IsAuthenticated)
                return View("SigninWarning");

            IList<ChatMessageDto> chatMessages = await chatService.FindLastMessagesAsync(50, null);

            AppUser user = await authService.GetUserByClaimPrincipalAsync(User);
            ViewBag.UserName = user.UserName;

            return View(chatMessages);
        }

        public async Task<IActionResult> FindLastMessagesAsync(string groups)
            => Json(await chatService.FindLastMessagesAsync(50, null));

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
