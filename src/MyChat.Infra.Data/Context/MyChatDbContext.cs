using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyChat.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyChat.Infra.Data.Context
{
    public class MyChatDbContext : IdentityDbContext<AppUser, AppRole, long>
    {

        public MyChatDbContext(DbContextOptions options) : base(options)
        {
        }

        protected MyChatDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyChatDbContext).Assembly);
        }

    }
}
