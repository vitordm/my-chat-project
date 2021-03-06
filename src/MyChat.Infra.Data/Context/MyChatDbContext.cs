﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyChat.Domain.Auth;
using MyChat.Domain.Entities;

namespace MyChat.Infra.Data.Context
{
    public class MyChatDbContext : IdentityDbContext<AppUser, AppRole, long>
    {
        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

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
