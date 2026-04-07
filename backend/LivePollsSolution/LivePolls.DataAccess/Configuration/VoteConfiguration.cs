
using LivePolls.Domain.Modeles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LivePolls.DataAccess.Configuration
{
    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.ToTable("votes");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("NEWID()");

            builder.Property(v => v.PollId)
                .HasColumnName("pollid")
                .IsRequired();

            builder.Property(v => v.OptionId)
                .HasColumnName("optionid")
                .IsRequired();

            builder.Property(v => v.UserId)
                .HasColumnName("userid")
                .IsRequired();

            builder.Property(v => v.VotedAt)
                .HasColumnName("votedat")
                .HasDefaultValueSql("GETUTCDATE()");

            // Связи
            builder.HasOne(v => v.Poll)
                .WithMany()
                .HasForeignKey(v => v.PollId);

            builder.HasOne(v => v.Option)
                .WithMany(o => o.Votes)
                .HasForeignKey(v => v.OptionId);

            builder.HasOne(v => v.User)
                .WithMany()
                .HasForeignKey(v => v.UserId);

            // Уникальность: один пользователь может голосовать только один раз в опросе
            builder.HasIndex(v => new { v.PollId, v.UserId })
                .IsUnique()
                .HasDatabaseName("IX_Votes_PollId_UserId");
        }
    }
}