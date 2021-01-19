using Microsoft.AspNetCore.Identity;
using MyChat.Application.Dto.Requests;
using MyChat.Application.Service.Contracts;
using MyChat.Domain.Auth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyChat.Application.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> userManager;

        public AuthService(
            UserManager<AppUser> userManager
        )
        {
            this.userManager = userManager;
        }

        public async Task<AppUser> GetUserByUserNameAsync(string userName)
            => await userManager.FindByNameAsync(userName);

        public async Task<AppUser> GetUserByClaimPrincipalAsync(ClaimsPrincipal user)
            => await userManager.GetUserAsync(user);

        public async Task<IdentityResult> RegisterNewUserAsync(RegisterNewUserRequest request)
        {
            var appUser = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email
            };

            return await userManager.CreateAsync(appUser, request.Password);
        }
    }
}
