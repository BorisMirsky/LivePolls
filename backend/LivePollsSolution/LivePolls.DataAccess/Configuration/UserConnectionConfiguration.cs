
//using LivePolls.Domain.Entities;
using LivePolls.Domain.Modeles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LivePolls.DataAccess.Configuration
{
    public class UserConnectionConfiguration : IEntityTypeConfiguration<UserConnection>
    {
        public void Configure(EntityTypeBuilder<UserConnection> builder)
        {
            builder.ToTable("userconnections");
            builder.HasKey(uc => uc.Id);

            builder.Property(uc => uc.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("NEWID()");

            builder.Property(uc => uc.UserId)
                .HasColumnName("userid")
                .IsRequired();

            builder.Property(uc => uc.ConnectionId)
                .HasColumnName("connectionid")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(uc => uc.PollId)
                .HasColumnName("pollid");

            builder.Property(uc => uc.ConnectedAt)
                .HasColumnName("connectedat")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(uc => uc.LastActivity)
                .HasColumnName("lastactivity")
                .HasDefaultValueSql("GETUTCDATE()");

            // Связь с User
            //builder.HasOne(uc => uc.User)
            //    .WithMany()
            //    .HasForeignKey(uc => uc.UserId);
        }
    }
}