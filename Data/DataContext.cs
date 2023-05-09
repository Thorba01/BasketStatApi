using BasketStatsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketStatsApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Skill>().HasData(
        //        new Skill { Id = 1, Name = "Fireball", Damage = 30 },
        //        new Skill { Id = 2, Name = "Frenzy", Damage = 20 },
        //        new Skill { Id = 3, Name = "Blizzard", Damage = 50 }
        //        );
        //}

        public DbSet<Player> Players => Set<Player>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Team> Teams => Set<Team>();
        public DbSet<Match> Matchs => Set<Match>();
        public DbSet<Stat> Stats => Set<Stat>();
    }
}
