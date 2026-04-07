//using LivePolls.Domain.Entities;
using LivePolls.Domain.Modeles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LivePolls.DataAccess.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("NEWID()");

            builder.Property(u => u.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.Login)
                .HasColumnName("login")
                .HasMaxLength(100);

            builder.Property(u => u.Password)
                .HasColumnName("password")
                .HasMaxLength(255);

            // Связь с UserConnection
            builder.HasMany(u => u.Connections)
                .WithOne(uc => uc.User)
                .HasForeignKey(uc => uc.UserId);
        }
    }
}