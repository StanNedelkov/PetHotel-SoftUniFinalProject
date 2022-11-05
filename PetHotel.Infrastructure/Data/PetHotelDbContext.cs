using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetHotel.Common;
using PetHotel.Infrastructure.Data.Entities;
using System.Runtime.CompilerServices;

namespace PetHotel.Infrastructure.Data
{

    public class PetHotelDbContext : IdentityDbContext<User>
    {
        private const string adminId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e";
        private const string userId = "dea12856-c198-4129-b3f3-b893d8395082";
        private const string adminRoleId = "6d5800ce-d123-4fc8-83d9-d6b3ac1f591e";
        private const string userRoleId = "dea12856-c321-4129-b3f3-b893d8395082";
        public PetHotelDbContext(DbContextOptions<PetHotelDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Capacity> Capacities { get; set; } = null!;
        public DbSet<Hotel> Hotels { get; set; } = null!;
        public DbSet<Pet> Pets { get; set; } = null!;
        public DbSet<PetType> PetTypes { get; set; } = null!;
        public DbSet<Schedule> Schedules { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder builder)
        {

            this.SeedAdminRole();
            builder.Entity<IdentityRole>()
                .HasData(this.AdminRole);

            this.SeedAdminUsers();
            builder.Entity<User>()
                .HasData(this.AdminUser);

            builder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminId,
                });
            
            base.OnModelCreating(builder);
        }
        

        //Properties to seed the DB.

        private User AdminUser { get; set; } = null!;

        private IdentityRole AdminRole { get; set; } = null!;

        //Seed Information
        private void SeedAdminRole()
        {
            AdminRole = new IdentityRole()
            {
                Id = adminRoleId,
                Name = GlobalConstants.AdministratorRoleName,
                NormalizedName = GlobalConstants.AdministratorRoleName.ToUpper()
            };
        }
        private void SeedAdminUsers()
        {
            var hasher = new PasswordHasher<User>();
            this.AdminUser = new User()
            {
                Id = adminId,
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                FirstName = "Admin",
                LastName = "User"
            };
            this.AdminUser.PasswordHash = hasher.HashPassword(this.AdminUser, "guest");
        }
    }
}