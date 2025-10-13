using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Microsoft.AspNetCore.Builder; 

namespace AbsoluteCinema.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CinemaDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Films.Any())
            {
                context.Films.AddRange(
                    new Film
                    {
                        Title = "Inception",
                        Description = "A mind-bending thriller where dreams are real.",
                        Genre = "Sci-Fi",
                        Director = "Christopher Nolan",
                        ReleaseYear = 2010,
                        DurationMinutes = 148,
                        PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_.jpg",
                        TicketPrice = 150,
                        StartDate = new DateTime(2025, 10, 15),
                        EndDate = new DateTime(2025, 11, 15),
                        IsActive = true
                    },
                    new Film
                    {
                        Title = "Interstellar",
                        Description = "A space adventure that explores love, time, and survival.",
                        Genre = "Sci-Fi",
                        Director = "Christopher Nolan",
                        ReleaseYear = 2014,
                        DurationMinutes = 169,
                        PosterUrl = "https://upload.wikimedia.org/wikipedia/uk/2/29/Interstellar_film_poster2.jpg",
                        TicketPrice = 180,
                        StartDate = new DateTime(2025, 10, 20),
                        EndDate = new DateTime(2025, 11, 20),
                        IsActive = true
                    },
                    new Film
                    {
                        Title = "The Dark Knight",
                        Description = "The battle between Batman and the Joker in Gotham City.",
                        Genre = "Action",
                        Director = "Christopher Nolan",
                        ReleaseYear = 2008,
                        DurationMinutes = 152,
                        PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_FMjpg_UX1000_.jpg",
                        TicketPrice = 140,
                        StartDate = new DateTime(2025, 10, 10),
                        EndDate = new DateTime(2025, 11, 10),
                        IsActive = true
                    },
                    new Film
                    {
                        Title = "La La Land",
                        Description = "A romantic musical about dreams, love, and heartbreak.",
                        Genre = "Musical",
                        Director = "Damien Chazelle",
                        ReleaseYear = 2016,
                        DurationMinutes = 128,
                        PosterUrl = "https://m.media-amazon.com/images/M/MV5BMzUzNDM2NzM2MV5BMl5BanBnXkFtZTgwNTM3NTg4OTE@._V1_FMjpg_UX1000_.jpg",
                        TicketPrice = 130,
                        StartDate = new DateTime(2025, 10, 25),
                        EndDate = new DateTime(2025, 11, 25),
                        IsActive = true
                    },
                    new Film
                    {
                        Title = "Oppenheimer",
                        Description = "The story of the man who created the atomic bomb.",
                        Genre = "Drama",
                        Director = "Christopher Nolan",
                        ReleaseYear = 2023,
                        DurationMinutes = 180,
                        PosterUrl = "https://upload.wikimedia.org/wikipedia/en/4/4a/Oppenheimer_%28film%29.jpg",
                        TicketPrice = 200,
                        StartDate = new DateTime(2025, 10, 30),
                        EndDate = new DateTime(2025, 11, 30),
                        IsActive = true
                    }
                );

                context.SaveChanges();
            }

            if (!context.Screenings.Any())
            {
                var inception = context.Films.First(f => f.Title == "Inception");
                var interstellar = context.Films.First(f => f.Title == "Interstellar");

                context.Screenings.AddRange(
                    new Screening
                    {
                        FilmID = inception.FilmID,
                        StartTime = new DateTime(2025, 11, 1, 18, 0, 0),
                        CinemaHall = 1
                    },
                    new Screening
                    {
                        FilmID = inception.FilmID,
                        StartTime = new DateTime(2025, 11, 2, 21, 30, 0),
                        CinemaHall = 3
                    },
                    new Screening
                    {
                        FilmID = interstellar.FilmID,
                        StartTime = new DateTime(2025, 11, 1, 15, 0, 0),
                        CinemaHall = 2
                    },
                    new Screening
                    {
                        FilmID = interstellar.FilmID,
                        StartTime = new DateTime(2025, 11, 3, 19, 0, 0),
                        CinemaHall = 2
                    }
                );
                context.SaveChanges();
            }
        }
    }
}