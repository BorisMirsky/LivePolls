using LivePolls.Domain.Modeles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Diagnostics;



namespace LivePolls.DataAccess
{
    public class AppDbContext : DbContext
    {

        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollOption> PollOptions { get; set; }
        public DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) { }
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
