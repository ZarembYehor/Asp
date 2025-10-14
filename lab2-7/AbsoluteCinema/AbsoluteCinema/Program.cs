using AbsoluteCinema.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System;

namespace AbsoluteCinema
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<CinemaDbContext>(opts => {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("AbsoluteCinemaConnection"));
            });

            builder.Services.AddScoped<ICinemaRepository, EFCinemaRepository>();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"))
            );

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 8;
                opts.Password.RequireDigit = true;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireNonAlphanumeric = false;
                opts.User.RequireUniqueEmail = true;

            })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            var app = builder.Build();

            app.UseStaticFiles();

            app.UseSession();
            app.UseAuthentication(); 
            app.UseAuthorization(); 

            app.MapDefaultControllerRoute();

            SeedData.EnsurePopulated(app);

            app.Run();
        }
    }
}