using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMissingProblem
{
    public class ExampleGenerator
    {
        public int[] FullSet { get; set; }
        public int[] MissingValueSet { get; set; }

        public Random random { get; set; }

        public ExampleGenerator()
        {
            random = new Random();
        }

        public void CreateExampleSets()
        {
            
            var arraySize = random.Next(4, 20);

            FullSet = new int[arraySize];
            MissingValueSet = new int[arraySize - 1];

            for (int i = 0; i < FullSet.Length; i++)
            {
                FullSet[i] = GetUniqueValue(random);
            }

            var indexToRemove = random.Next(0, FullSet.Length);

            var fullList = FullSet.ToList<int>();
            fullList.RemoveAt(indexToRemove);
            MissingValueSet = fullList.ToArray();

            //int missingSetIndex = 0;
            //for (int k = 0; k < FullSet.Length; k++)
            //{
            //    if (k == indexToRemove)
            //        continue;

            //    MissingValueSet[missingSetIndex] = FullSet[k];
            //    missingSetIndex += 1;
            //}

            ScramblePartialArray(random);
        }

        private int GetUniqueValue(Random rand)
        {
            var val = rand.Next(0, 100);
            while (FullSet.Contains(val))
            {
                val = rand.Next(0, 100);
            }

            return val;
        }

        private void ScramblePartialArray(Random r)
        {
            var array = MissingValueSet;
            var iterations = r.Next(1000, 2500);
            
            for (int i = 0; i < iterations; i++)
            {
                var index = r.Next(0, array.Length - 1);
                var temp = array[index];
                try
                {
                    array[index] = array[index + 1];
                    array[index + 1] = temp;
                }
                catch (IndexOutOfRangeException)
                {
                    array[index] = array[index - 1];
                    array[index - 1] = temp;
                }         
            }

            MissingValueSet = array;
        }


    }
}
