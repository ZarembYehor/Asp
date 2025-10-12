using AbsoluteCinema.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using static AbsoluteCinema.Models.CinemaDbContext;


namespace AbsoluteCinema
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<CinemaDbContext>(opts => {
                opts.UseSqlServer(builder.Configuration["ConnectionStrings:AbsoluteCinemaConnection"]);
            });

            builder.Services.AddScoped<ICinemaRepository, EFCinemaRepository>();

            var app = builder.Build();

            app.UseStaticFiles();

            app.MapDefaultControllerRoute();

            SeedData.EnsurePopulated(app);

            app.Run();

        }
    }
}
