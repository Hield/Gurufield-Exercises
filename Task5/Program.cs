using System;
using System.Collections.Generic;
using System.Linq;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new List<string> {"red", "green", "yellow", "blue", "purple"};
            var newArray = ChangeStartingPoint(array, "blue");
            foreach(var color in newArray)
            {
                Console.Write(color + ", ");
            }
        }

        static List<string> ChangeStartingPoint(List<string> array, string startingPoint)
        {
            int index = array.IndexOf(startingPoint);
            if (index == -1)
                throw new System.ArgumentException("The new starting point doesn't exist in the array", nameof(startingPoint));
            var newArray = new List<string>();
            // Add the later half of the array first
            newArray.AddRange(array.GetRange(index, array.Count - index));
            // Add the first half after
            newArray.AddRange(array.GetRange(0, index));
            return newArray;
        }
    }
}
