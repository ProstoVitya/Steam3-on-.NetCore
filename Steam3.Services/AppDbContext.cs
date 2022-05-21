
using Microsoft.EntityFrameworkCore;
using Steam3.Models;

namespace Steam3.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<AvalibleGame> AvalibleGames { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Game> Games { get; set; }
    }
}
