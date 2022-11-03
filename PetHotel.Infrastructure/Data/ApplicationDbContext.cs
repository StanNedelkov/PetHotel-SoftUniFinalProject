using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetHotel.Infrastructure.Data.Entities;

namespace PetHotel.Infrastructure.Data
{
    public class PetHotelDbContext : IdentityDbContext<User>
    {
        public PetHotelDbContext(DbContextOptions<PetHotelDbContext> options)
            : base(options)
        {
        }
        public DbSet<Capacity> Capacities { get; set; } = null!;
        public DbSet<Hotel> Hotels { get; set; } = null!;
        public DbSet<Pet> Pets { get; set; } = null!;
        public DbSet<PetType> PetTypes { get; set; } = null!;
        public DbSet<Schedule> Schedules { get; set; } = null!;
    }
}