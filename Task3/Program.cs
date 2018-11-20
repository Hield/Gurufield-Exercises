using System;
using System.Configuration;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            new Controller().Start();

            // Close the database before exiting
            Database.Instance.Close();
        }
    }
}
