using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMissingProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleGenerator eg = new ExampleGenerator();

            do
            {
                Console.Clear();
                eg.CreateExampleSets();
                
                Console.WriteLine($"The full set is:");
                PrintArray(eg.FullSet);

                Console.WriteLine();

                Console.WriteLine($"The set with missing value is:");
                PrintArray(eg.MissingValueSet);

                Console.WriteLine();

                Console.WriteLine($"The missing value is: {FindMissingHash(eg.FullSet, eg.MissingValueSet)}");

                Console.WriteLine();

            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);

            Console.Write("Application closing.");
            System.Threading.Thread.Sleep(700);
            Console.Write(".");
            System.Threading.Thread.Sleep(700);
            Console.Write(".");
            System.Threading.Thread.Sleep(700);
            
            Environment.Exit(0);
        }

        static void PrintArray(int[] array)
        {
            var output = "[ ";

            foreach (int i in array)
                output += $"{i} ";

            Console.WriteLine( output += ']' );
        }


        // Finds missing when arrays are in the same order
        static int FindMissing(int[] full, int[] partial)
        {
            int missingValue = -1;

            //var partialIndex = 0;
            for (int i = 0; i < full.Length; i++)
            {
                try
                {
                    if (full[i] == partial[i])
                        continue;

                    return full[i];
                }
                catch (IndexOutOfRangeException)
                {
                    missingValue = full[i];
                }
            }

            return missingValue;
        }

        static int FindMissingHash(int[] full, int[] partial)
        {
            int missingValue = -1;

            var fullDict = full
                .Select((value, index) => new { value, index })
                .ToDictionary(i => i.index, i => i.value);

            var partialDict = partial
                .Select((value, index) => new { value, index })
                .ToDictionary(i => i.index, i => i.value);

            foreach (int val in fullDict.Values)
            {
                if (!partialDict.ContainsValue(val))
                    missingValue = val;

                else continue;
            }

            return missingValue;
        }

        static void PrintDictionary(Dictionary<int, int> dict)
        {
            if (dict.Count == 0 || dict == null)
                return;

            var output = "[ ";

            for (int i = 0; i < dict.Count; i++)
            {
                if (i == (dict.Count - 1))
                {
                    output += $"{i} : {dict[i]} ]";
                    continue;
                }
                output += $"{i} : {dict[i]}, ";
            }

            Console.WriteLine(output);
        }
    }
}
