﻿using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i < 101; i++)
            {
                if (i % 15 == 0)
                    Console.WriteLine("FooBar");
                else if (i % 3 == 0)
                    Console.WriteLine("Foo");
                else if (i % 5 == 0)
                    Console.WriteLine("Bar");
                else
                    Console.WriteLine(i);
            }
        }
    }
}
