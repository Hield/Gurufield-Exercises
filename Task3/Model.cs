using System;
using System.Collections.Generic;

namespace Task3
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ReleaseYear { get; set; }
        public string Genre { get; set; }

        public Model(string name, int releaseYear, string genre)
            : this(0, name, releaseYear, genre) {}

        public Model(int id, string name, int releaseYear, string genre)
        {
            this.Id = id;
            this.Name = name;
            this.ReleaseYear = releaseYear;
            this.Genre = genre;
        }

        public static List<Model> GetAll() 
        {
            var movies = new List<Model>();
            var reader = Database.Instance.Query("SELECT * FROM movies;");
            while (reader.Read())
            {
                movies.Add(new Model(
                    Convert.ToInt32(reader["id"]),
                    Convert.ToString(reader["name"]),
                    Convert.ToInt32(reader["release_year"]),
                    Convert.ToString(reader["genre"])));
            }
            return movies;
        }

        public static void AddNew(Model newMovie)
        {
            Database.Instance.Query($@"
                INSERT INTO movies(name, release_year, genre)            
                VALUES('{newMovie.Name}', {newMovie.ReleaseYear}, '{newMovie.Genre}');
                ");
        }

        public static void RemoveById(int id)
        {
            Database.Instance.Execute($@"
                DELETE FROM movies
                WHERE id = {id};
                ");
        }
    }
}
