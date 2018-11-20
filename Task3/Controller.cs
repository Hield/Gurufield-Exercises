using System;
using System.Collections.Generic;

namespace Task3
{
    public class Controller
    {
        private List<Model> movies;

        public void Start() {
            GetMoviesFromDatabase();
            ListAllMovies();
            var nextCommand = GetNextCommand();
            while (nextCommand != 3)
            {   
                switch (nextCommand)
                {
                    case 1:
                        AddNewMovieToTheDatabase();
                        Console.Clear();
                        GetMoviesFromDatabase();
                        ListAllMovies();
                        nextCommand = GetNextCommand();
                        break;
                    case 2:
                        RemoveAMovie();
                        Console.Clear();
                        GetMoviesFromDatabase();
                        ListAllMovies();
                        nextCommand = GetNextCommand();
                        break;
                    default:
                        Console.Clear();
                        ListAllMovies();
                        Console.WriteLine("Error: Invalid choice\n");
                        nextCommand = GetNextCommand();
                        break;
                }
            }
        }

        public void GetMoviesFromDatabase() 
        {
            movies = Model.GetAll();
        }

        public void AddNewMovieToTheDatabase()
        {
            string name, genre;
            int releaseYear;
            Console.Clear();
            // Get movie name
            Console.Write("Enter movie name: ");
            var input = Console.ReadLine();
            while (input == "")
            {
                Console.WriteLine("Error: Movie name cannot be empty");
                Console.Write("Enter movie name: ");
                input = Console.ReadLine();
            }
            name = input;
            // Get release year
            Console.Write("Enter release year: ");
            input = Console.ReadLine();
            while (!Int32.TryParse(input, out releaseYear))
            {
                Console.WriteLine("Error: Invalid release year");
                Console.Write("Enter release year: ");
                input = Console.ReadLine();
            }
            // Get genre
            Console.Write("Enter genre: ");
            genre = Console.ReadLine();

            // Call to the model function
            Model.AddNew(new Model(name, releaseYear, genre));
        }

        public void RemoveAMovie()
        {
            Console.Clear();
            ListAllMovies();
            Console.Write("Enter the ID of the movie you want to remove: ");
            var input = Console.ReadLine();
            int id;
            while (!Int32.TryParse(input, out id))
            {
                Console.Clear();
                ListAllMovies();
                Console.WriteLine("Error: Invalid ID\n");
                Console.Write("Enter the ID of the movie you want to remove: ");
                input = Console.ReadLine();
            }
            // Call to the model function
            Model.RemoveById(id);
        }

        public void ListAllMovies() 
        {
            Console.WriteLine("All movies in your watchlist");
            Console.WriteLine($"|{"Id", 3}|{"Name", 18}|{"Release Year", 15}|{"Genre", 10}|");
            foreach (var movie in movies)
            {
                Console.WriteLine($"|{movie.Id, 3}|{movie.Name, 18}|{movie.ReleaseYear, 15}|{movie.Genre, 10}|");
            }
            Console.WriteLine();
        }

        public int GetNextCommand() 
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("1. Add to the watchlist");
            Console.WriteLine("2. Remove a movie");
            Console.WriteLine("3. Exit\n");
            Console.Write("Your next command(1, 2 or 3): ");
            var input = Console.ReadLine();
            int choice;
            while (!Int32.TryParse(input, out choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("Error: Invalid choice");
                Console.Write("Your next command(1, 2 or 3): ");
                input = Console.ReadLine();
            }
            return choice;
        }
    }
}
