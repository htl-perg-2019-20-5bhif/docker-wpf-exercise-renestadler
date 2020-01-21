using Microsoft.EntityFrameworkCore;

namespace DockerE2EApi.Model
{
    public class CarDataContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<Booking> Booking { get; set; }

        public CarDataContext(DbContextOptions<CarDataContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasMany(c => c.Bookings);
        }
    }
}
