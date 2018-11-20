using System;
using System.Data.SQLite;

namespace Task3
{
    // A singleton class representing the global database
    public class Database
    {
        private static Database _instance;
        private SQLiteConnection dbConnection;

        public static Database Instance
        {
            get {
                if (_instance == null) {
                    _instance = new Database();
                }
                return _instance;
            }
        }

        protected Database()
        {
            dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            dbConnection.Open();

            CleanDatabase();
            CreateTables();
            PopulateData();
        }

        private void CleanDatabase()
        {
            Execute("DROP TABLE IF EXISTS movies;");
        }


        private void CreateTables()
        {         
            Execute(@"
                CREATE TABLE IF NOT EXISTS movies (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name VARCHAR(100),
                    release_year INTEGER,
                    genre VARCHAR(20)
                );");
        }

        private void PopulateData() 
        {
            Execute(@"
                INSERT INTO movies(name, release_year, genre)
                VALUES
                    ('The Avengers', 2012, 'Superhero'),
                    ('La La Land', 2016, 'Romance'),
                    ('Kimi no Na wa', 2016, 'Anime')
                ");
        }

        public void Execute(string sql) 
        {
            var command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
        }

        public SQLiteDataReader Query(string sql) 
        {
            var command = new SQLiteCommand(sql, dbConnection);
            return command.ExecuteReader();
        }

        public void Close() 
        {
            dbConnection.Close();
        }
    }
}
