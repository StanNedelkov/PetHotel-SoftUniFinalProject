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

        private const string employeeId = "a841631e-8b01-4884-a23e-c0232e24aa29";
        private const string employeeRoleId = "df93e740-41a6-474c-ba20-5505df682ae4";
        public PetHotelDbContext(DbContextOptions<PetHotelDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Capacity> Capacities { get; set; } = null!;
        public DbSet<Hotel> Hotels { get; set; } = null!;
        public DbSet<Pet> Pets { get; set; } = null!;
        public DbSet<PetType> PetTypes { get; set; } = null!;
        public DbSet<Reservation> Schedules { get; set; } = null!;
        public DbSet<GalleryImage> GalleryImages { get; set; } = null!;
        public DbSet<ImageTag> ImageTags { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //admin
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


            //client
            this.SeedClientRole();
            builder.Entity<IdentityRole>()
                .HasData(this.ClientRole);

            this.SeedClientUsers();
            builder.Entity<User>()
                .HasData(this.ClientUser);

            builder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = userId,
                });

            //employee
            this.SeedEmployeeRole();
            builder.Entity<IdentityRole>()
                .HasData(this.EmployeeRole);

            this.SeedEmployeeUsers();
            builder.Entity<User>()
                .HasData(this.EmployeeUser);

            builder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>
                {
                    RoleId = employeeRoleId,
                    UserId = employeeId,
                });


            base.OnModelCreating(builder);
        }
        

        //Properties to seed the DB.
        //Users and Roles
        private User AdminUser { get; set; } = null!;

        private IdentityRole AdminRole { get; set; } = null!;

        private User ClientUser { get; set; } = null!;

        private IdentityRole ClientRole { get; set; } = null!;

        private User EmployeeUser { get; set; } = null!;

        private IdentityRole EmployeeRole { get; set; } = null!;

        /*//Capacity
        private Capacity HotelCapacity { get; set; } = null!;

        //Hotel
        public Hotel CatsDogsCrocsHotel { get; set; } = null!;*/


        //Seed Information
        //Admin User and Role
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

        //Client User and Role
        private void SeedClientRole()
        {
            ClientRole = new IdentityRole()
            {
                Id = userRoleId,
                Name = "User",
                NormalizedName = "USER"
            };
        }
        private void SeedClientUsers()
        {
            var hasher = new PasswordHasher<User>();
            this.ClientUser = new User()
            {
                Id = userId,
                UserName = "Stan",
                NormalizedUserName = "STAN",
                Email = "stenly.nedelkov@gmail.com",
                NormalizedEmail = "STENLY.NEDELKOV@GMAIL.COM",
                FirstName = "Stanislav",
                LastName = "Nedelkov"
            };
            this.ClientUser.PasswordHash = hasher.HashPassword(this.ClientUser, "Parola1!");
        }

        //Employee User and Role
        private void SeedEmployeeRole()
        {
            EmployeeRole = new IdentityRole()
            {
                Id = employeeRoleId,
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            };
        }
        private void SeedEmployeeUsers()
        {
            var hasher = new PasswordHasher<User>();
            this.EmployeeUser = new User()
            {
                Id = employeeId,
                UserName = "Boss",
                NormalizedUserName = "BOSS",
                Email = "stenly.nedelkov@gmail.com",
                NormalizedEmail = "STENLY.NEDELKOV@GMAIL.COM",
                FirstName = "Mitko",
                LastName = "Mitkov"
            };
            this.ClientUser.PasswordHash = hasher.HashPassword(this.ClientUser, "Parola1!");
        }

        
    }
}