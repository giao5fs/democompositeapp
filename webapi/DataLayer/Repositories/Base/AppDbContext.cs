using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Models.BasicModel;

namespace webapi.DataLayer.Repositories.Base
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.SeedDataForDb();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
    }

    public static class SeedData
    {
        public static void SeedDataForDb(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    UserName = "admin",
                    Password = "123",
                    Address = "ABC"
                });
        }

    }
}
