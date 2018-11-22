using System;
using System.Collections.Generic;
using System.Linq;

namespace Task2
{
    class Program
    {
        static int[] GenerateArray(int n)
        {
            Random rnd = new Random();
            int[] a = new int[n]; // The required array of the task
            for (int i = 0; i < n; i++)
            {
                int newNumber = i + 1;
                // Swap place of the new number with a random number already in the array
                int index = rnd.Next(0, i);
                a[i] = a[index];
                a[index] = newNumber;
            }
            return a;
        }


        static Boolean NewIsUnique(List<int> l)
        {
            return (l.Count == new HashSet<int>(l).Count) && (l.Min() > 0) && (l.Max() <= l.Count);
        }

        static Boolean IsUnique(int[] a)
        {
            HashSet<int> numbers = new HashSet<int>();
            int max = a.Length;
            foreach (int i in a)
            {
                // Return false if it's a duplicate
                if (numbers.Contains(i))
                    return false;
                // Return false if it's out of range
                if (i < 1 || i > max)
                    return false;
                numbers.Add(i);
            }
            return true;
        }

        static void Main(string[] args)
        {
            // Test the testing function
            //Console.WriteLine(IsUnique(new int[] { 1, 1, 2 })); // Should return false
            //Console.WriteLine(IsUnique(new int[] { 1, 2, 4 })); // Should return false
            //Console.WriteLine(IsUnique(new int[] { 3, 1, 2 })); // Should return true

            Console.WriteLine(NewIsUnique(new List<int> { 1, 1, 2 })); // Should return false
            Console.WriteLine(NewIsUnique(new List<int> { 1, 2, 4 })); // Should return false
            Console.WriteLine(NewIsUnique(new List<int> { 3, 1, 2 })); // Should return true

            var a = new List<int>(GenerateArray(100000));
            Console.WriteLine(NewIsUnique(a));
        }
    }
}
