using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetHotel.Infrastructure.Data;
using PetHotel.Core.Models;
using PetHotel.Infrastructure.Data.Entities;
using PetHotel.Core.Contracts;
using PetHotel.Core.Services;

namespace PetHotel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<PetHotelDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //todo add real requirments and move them to js file. Stamo said so.
            builder.Services.AddDefaultIdentity<User>(options => 
            { 
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<PetHotelDbContext>();
            builder.Services.AddControllersWithViews();

            //caching to save data 
            builder.Services.AddMemoryCache();

            //Set login/register/access denied path here
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath= "/Logout";
            });

            builder.Services.AddMvcCore()
                            .AddDataAnnotations();

            //Add services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IPetService, PetService>();
            builder.Services.AddScoped<IHotelService, HotelService>();
            builder.Services.AddScoped<IGalleryService, GalleryService>();
            builder.Services.AddScoped<ICloudinaryImageUpload, CloudinaryImageUpload>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IHomeService, HomeService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
               // app.UseExceptionHandler("/Home/Error");
               
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //Map for the Areas folder configuration
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
              name: "default",
              pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(
            name: "MyAreaAdmin",
            areaName: "Admin",
            pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapAreaControllerRoute(
           name: "MyAreaClient",
           areaName: "Client",
           pattern: "Client/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapAreaControllerRoute(
           name: "MyAreaEmployee",
           areaName: "Employee",
           pattern: "Employee/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

            });

            app.Run();
        }

        /*private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();
            string[] roleNames = { "Admin", "Store-Manager", "Member" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                // ensure that the role does not exist
                if (!roleExist)
                {
                    //create the roles and seed them to the database: 
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // find the user with the admin email 
            var _user = await UserManager.FindByEmailAsync("admin@email.com");

            // check if the user exists
            if (_user == null)
            {
                //Here you could create the super admin who will maintain the web app
                var poweruser = new User
                {
                    UserName = "Admin",
                    Email = "admin@email.com",
                };
                string adminPassword = "p@$$w0rd";

                var createPowerUser = await UserManager.CreateAsync(poweruser, adminPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(poweruser, "Admin");

                }
            }
        }*/
    }
}