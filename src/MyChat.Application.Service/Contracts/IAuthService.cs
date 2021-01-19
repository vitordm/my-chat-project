using Microsoft.AspNetCore.Identity;
using MyChat.Application.Dto.Requests;
using MyChat.Domain.Auth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyChat.Application.Service.Contracts
{
    public interface IAuthService
    {
        /// <summary>
        /// Register new user and returns the result of Identity
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IdentityResult> RegisterNewUserAsync(RegisterNewUserRequest request);

        Task<AppUser> GetUserByUserNameAsync(string userName);
        Task<AppUser> GetUserByClaimPrincipalAsync(ClaimsPrincipal user);
    }
}
