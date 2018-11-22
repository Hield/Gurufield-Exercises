using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i < 101; i++)
            {
                Console.WriteLine((i % 15 == 0) ? "FooBar" : (i % 3 == 0) ? "Foo" : (i % 5 == 0) ? "Bar" : i.ToString());
            }
        }
    }
}
