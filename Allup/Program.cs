using Allup.Areas.Admin.Models;
using Allup.DAL;
using Allup.Middlewares;
using Allup.Services.Implementations;
using Allup.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Management;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllersWithViews();
        string GetSerialNumber()
        {
            string serialNumber = string.Empty;
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
            foreach (ManagementObject obj in searcher.Get())
            {
                serialNumber = obj["SerialNumber"]?.ToString();
                break;
            }
            return serialNumber;
        }

        string serialNumber = GetSerialNumber();
        Console.WriteLine(serialNumber);

        if (serialNumber == "4CE11718F6")
        {
            //univer
            builder.Services.AddDbContext<AppDbContext>(opt =>
       opt.UseSqlServer(builder.Configuration.GetConnectionString("Univer")));
        }
        else
        {
            //home
            builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Home")));

        }
        builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
        {
            opt.Password.RequiredLength = 8;
            opt.Password.RequireNonAlphanumeric = false;
            opt.User.RequireUniqueEmail = true;
            opt.Lockout.AllowedForNewUsers = true;
            opt.Lockout.MaxFailedAccessAttempts = 4;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(100);
        }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        builder.Services.AddScoped<IBasketService, BasketService>();

        var app = builder.Build();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseStaticFiles();
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        app.MapControllerRoute(
           name: "admin",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
       );
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );

        app.Run();
    }
}
