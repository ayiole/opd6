using Microsoft.EntityFrameworkCore;

namespace sha7p.Models
{
    public class CitiesDBContext : DbContext
    {

        public CitiesDBContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CitiesDB;Trusted_connection=TRUE");
        }
        // Коллекции сущностей
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasOne(c => c.Country)
                .WithMany(p => p.Cities)
                .HasForeignKey(p => p.CountryId);
        }
    }
}
