using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Task4.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MovieContext(
                serviceProvider.GetRequiredService<DbContextOptions<MovieContext>>()))
            {
                // Look for any movies
                if (context.Movies.Any())
                {
                    return; // DB has been seeded
                }

                context.Movies.AddRange(
                    new Movie
                    {
                        Name = "La La Land",
                        ReleaseYear = 2016,
                        Genre = "Romance"
                    },

                    new Movie
                    {
                        Name = "The Avengers",
                        ReleaseYear = 2012,
                        Genre = "Superhero"
                    },

                    new Movie
                    {
                        Name = "Avengers: Infinity War",
                        ReleaseYear = 2018,
                        Genre = "Superhero"
                    },

                    new Movie
                    {
                        Name = "Avengers: Age of Ultron",
                        ReleaseYear = 2015,
                        Genre = "Superhero"
                    },

                    new Movie
                    {
                        Name = "Kimi no Na wa",
                        ReleaseYear = 2016,
                        Genre = "Anime"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
