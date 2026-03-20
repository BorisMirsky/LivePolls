using Microsoft.EntityFrameworkCore;
using LivePolls.Domain.Models;



namespace LivePolls.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollOption> PollOptions { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Уникальность голоса: один пользователь может голосовать в опросе только один раз
            modelBuilder.Entity<Vote>()
                .HasIndex(v => new { v.PollId, v.UserName })
                .IsUnique();
        }
    }
}
