using Microsoft.AspNetCore.Identity;
using MyChat.Domain.Entities;
using System.Collections.Generic;

namespace MyChat.Domain.Auth
{
    public class AppUser : IdentityUser<long>
    {
        public virtual ICollection<ChatMessage> Messages { get; set; } = new HashSet<ChatMessage>();

        public AppUser() : base()
        {

        }

        public AppUser(string userName) : base(userName)
        {
        }
    }
}
