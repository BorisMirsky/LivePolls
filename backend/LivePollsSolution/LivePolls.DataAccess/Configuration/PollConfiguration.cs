using LivePolls.Domain.Modeles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Numerics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;



namespace LivePolls.DataAccess.Configuration
{
    public class PollConfiguration : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .HasMany(p => p.Options)
                .WithOne(o => o.Poll);
        }
    }
}
