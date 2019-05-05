using Microsoft.EntityFrameworkCore;
using Limo.Models;

namespace Limo.DataBase
{
    class DataBaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Request> Requests { get; set; }

        private readonly string _databasePath;
        public DataBaseContext(string databasepath)
        {
            _databasePath = databasepath;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}
