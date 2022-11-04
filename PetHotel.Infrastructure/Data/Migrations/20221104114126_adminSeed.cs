using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHotel.Infrastructure.Data.Migrations
{
    public partial class adminSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6d5800ce-d123-4fc8-83d9-d6b3ac1f591e", "91985d82-2aee-4df7-8517-ed46e5f44c3d", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", 0, "b94fc704-5098-44b3-9f7c-85d57f59b098", "admin@gmail.com", false, "Admin", "User", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEF5U9St+JUoeS6G/vqT0Fih+0JQf8NB1c7q2Rsek+BdbSxgot3rRQ9EVJPIerXf8Xw==", null, false, "7b782a6e-78dc-4b56-8488-e418f6578ca4", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6d5800ce-d123-4fc8-83d9-d6b3ac1f591e", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6d5800ce-d123-4fc8-83d9-d6b3ac1f591e", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6d5800ce-d123-4fc8-83d9-d6b3ac1f591e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e");
        }
    }
}
