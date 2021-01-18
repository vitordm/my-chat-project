using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyChat.Domain.Entities;

namespace MyChat.Infra.Data.Configuration
{
    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.ToTable("ChatMessage");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.ChatGroup)
                .HasMaxLength(100)
                .IsRequired(false);
        }
    }
}
