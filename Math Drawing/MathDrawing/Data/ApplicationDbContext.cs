using MathDrawing.Classes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MathDrawing.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Equation> Equations { get; set; }
        public DbSet<CurrentState> MyState { get; set; }
        public DbSet<Player> Players { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().HasIndex(a => a.Name).IsUnique();
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
