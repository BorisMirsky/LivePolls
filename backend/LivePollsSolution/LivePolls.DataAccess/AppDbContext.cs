using LivePolls.DataAccess.Configuration;
using LivePolls.Domain.Modeles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;



namespace LivePolls.DataAccess
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) { }

        public DbSet<Poll> Polls { get; set; } = null!;
        public DbSet<PollOption> PollOptions { get; set; } = null!;
        public DbSet<UserConnection> UserConnections { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Уникальность голоса: один пользователь может голосовать в опросе только один раз
            //modelBuilder.Entity<Vote>()
            //    .HasIndex(v => new { v.PollId, v.UserName })
            //    .IsUnique();
            modelBuilder.ApplyConfiguration(new PollConfiguration());
            modelBuilder.ApplyConfiguration(new PollOptionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserConnectionConfiguration());
            modelBuilder.ApplyConfiguration(new VoteConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
