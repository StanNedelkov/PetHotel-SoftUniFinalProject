﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PetHotel.Infrastructure.Data;

#nullable disable

namespace PetHotel.Infrastructure.Migrations
{
    [DbContext(typeof(PetHotelDbContext))]
    partial class PetHotelDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "6d5800ce-d123-4fc8-83d9-d6b3ac1f591e",
                            ConcurrencyStamp = "09b833b0-b7c5-4797-826e-87cdadfb19a4",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "dea12856-c321-4129-b3f3-b893d8395082",
                            ConcurrencyStamp = "9e2ba927-c08f-4d82-93ba-1a2b24ab6acf",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "df93e740-41a6-474c-ba20-5505df682ae4",
                            ConcurrencyStamp = "6feb93e6-cab6-42c2-a499-3cefbbb95465",
                            Name = "Employee",
                            NormalizedName = "EMPLOYEE"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                            RoleId = "6d5800ce-d123-4fc8-83d9-d6b3ac1f591e"
                        },
                        new
                        {
                            UserId = "dea12856-c198-4129-b3f3-b893d8395082",
                            RoleId = "dea12856-c321-4129-b3f3-b893d8395082"
                        },
                        new
                        {
                            UserId = "a841631e-8b01-4884-a23e-c0232e24aa29",
                            RoleId = "df93e740-41a6-474c-ba20-5505df682ae4"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PetHotel.Infrastructure.Data.Entities.Capacity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CurCapacityCats")
                        .HasColumnType("int");

                    b.Property<int>("CurCapacityDogs")
                        .HasColumnType("int");

                    b.Property<int>("CurCapacityGator")
                        .HasColumnType("int");

                    b.Property<int>("HotelID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Capacities");
                });

            modelBuilder.Entity("PetHotel.Infrastructure.Data.Entities.GalleryImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("HotelID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HotelID");

                    b.ToTable("GalleryImages");
                });

            modelBuilder.Entity("PetHotel.Infrastructure.Data.Entities.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CapacityID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CapacityID");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("PetHotel.Infrastructure.Data.Entities.ImageTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GalleryImageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GalleryImageId");

                    b.ToTable("ImageTags");
                });

            modelBuilder.Entity("PetHotel.Infrastructure.Data.Entities.Pet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Alergies")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HotelID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PetTypeID")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("HotelID");

                    b.HasIndex("PetTypeID");

                    b.HasIndex("UserID");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("PetHotel.Infrastructure.Data.Entities.PetType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("CostPerDay")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PetTypes");
                });

            modelBuilder.Entity("PetHotel.Infrastructure.Data.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("AdmissionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("HotelID")
                        .HasColumnType("int");

                    b.Property<int>("PetID")
                        .HasColumnType("int");

                    b.Property<string>("PetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HotelID");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("PetHotel.Infrastructure.Data.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "719cf91f-e5ca-4ea0-8522-1c88dd6cfb38",
                            Email = "admin@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Admin",
                            LastName = "User",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEJfzxrjAG1hoxUWhUeU/776NXKWNUEpOesw2I4FnTW7lin4sey/Km3OXe2aQYrEAvA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "47c61879-719e-4064-bb27-4f9eb77948e1",
                            TwoFactorEnabled = false,
                            UserName = "Admin"
                        },
                        new
                        {
                            Id = "dea12856-c198-4129-b3f3-b893d8395082",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d9425841-dcda-481a-8c48-47da0906eb91",
                            Email = "stenly.nedelkov@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Stanislav",
                            LastName = "Nedelkov",
                            LockoutEnabled = false,
                            NormalizedEmail = "STENLY.NEDELKOV@GMAIL.COM",
                            NormalizedUserName = "STAN",
                            PasswordHash = "AQAAAAEAACcQAAAAEMxcfKgWpg0dXC28M8BnmmW6mhJ9+coiLK5OdG08CFb4h03pXQJYQ9g4quhQHwDLPQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "daf10701-df62-4ad8-9f42-ba104b0774be",
                            TwoFactorEnabled = false,
                            UserName = "Stan"
                        },
                        new
                        {
                            Id = "a841631e-8b01-4884-a23e-c0232e24aa29",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "904643dd-536c-4b54-9fe9-77f1703002d9",
                            Email = "stenly.nedelkov@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Mitko",
                            LastName = "Mitkov",
                            LockoutEnabled = false,
                            NormalizedEmail = "STENLY.NEDELKOV@GMAIL.COM",
                            NormalizedUserName = "BOSS",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "22f5fc9b-bb0f-4bf7-b25f-3c43683d5f56",
                            TwoFactorEnabled = false,
                            UserName = "Boss"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PetHotel.Infrastructure.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PetHotel.Infrastructure.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHotel.Infrastructure.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PetHotel.Infrastructure.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PetHotel.Infrastructure.Data.Entities.GalleryImage", b =>
                {
                    b.HasOne("PetHotel.Infrastructure.Data.Entities.Hotel", "Hotel")
                        .WithMany("GalleryImages")
                        .HasForeignKey("HotelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("PetHotel.Infrastructure.Data.Entities.Hotel", b =>
                {
                    b.HasOne("PetHotel.Infrastructure.Data.Entities.Capacity", "Capacity")
                        .WithMany()
                        .HasForeignKey("CapacityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Capacity");
                });

            modelBuilder.Entity("PetHotel.Infrastructure.Data.Entities.ImageTag", b =>
                {
                    b.HasOne("PetHotel.Infrastructure.Data.Entities.GalleryImage", null)
                        .WithMany("Tags")
                        .HasForeignKey("GalleryImageId");
                });

            modelBuilder.Entity("PetHotel.Infrastructure.Data.Entities.Pet", b =>
                {
                    b.HasOne("PetHotel.Infrastructure.Data.Entities.Hotel", "Hotel")
                        .WithMany("Pets")
                        .HasForeignKey("HotelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHotel.Infrastructure.Data.Entities.PetType", "PetType")
                        .WithMany("Pets")
                        .HasForeignKey("PetTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHotel.Infrastructure.Data.Entities.User", "User")
                        .WithMany("Pets")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("PetType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetHotel.Infrastructure.Data.Entities.Reservation", b =>
                {
                    b.HasOne("PetHotel.Infrastructure.Data.Entities.Hotel", "Hotel")
                        .WithMany("Schedules")
                        .HasForeignKey("HotelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("PetHotel.Infrastructure.Data.Entities.GalleryImage", b =>
                {
                    b.Navigation("Tags");
                });

            modelBuilder.Entity("PetHotel.Infrastructure.Data.Entities.Hotel", b =>
                {
                    b.Navigation("GalleryImages");

                    b.Navigation("Pets");

                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("PetHotel.Infrastructure.Data.Entities.PetType", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("PetHotel.Infrastructure.Data.Entities.User", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
