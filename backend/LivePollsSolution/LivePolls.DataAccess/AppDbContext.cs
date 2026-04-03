using LivePolls.Domain.Modeles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Diagnostics;



namespace LivePolls.DataAccess
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) { }

        public DbSet<Poll> Polls { get; set; } = null!;
        public DbSet<PollOption> PollOptions { get; set; } = null!;
        //public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }

        // Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Уникальность голоса: один пользователь может голосовать в опросе только один раз
            //modelBuilder.Entity<Vote>()
            //    .HasIndex(v => new { v.PollId, v.UserName })
            //    .IsUnique();
            modelBuilder.ApplyConfiguration(new Configuration.PollConfiguration());
            modelBuilder.ApplyConfiguration(new Configuration.PollOptionConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
