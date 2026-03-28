using LivePolls.Domain.Modeles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Numerics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;



namespace LivePolls.DataAccess.Configuration
{
    public class PollOptionConfiguration: IEntityTypeConfiguration<PollOption>
    {
        public void Configure(EntityTypeBuilder<PollOption> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(o => o.Poll)
                .WithMany(p => p.Options)
                .HasForeignKey(o => o.PollId)
                .IsRequired();
            builder.Property(b => b.PollId)
                .IsRequired();
            builder.Property(p => p.Text);
            builder.Property(p => p.Order);
        }
    }
}
