using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextSearchApi
{
    public class StringHelper
    {
        public static int[] FindOccurrences(string main, string subtext)
        {
            List<int> matchIndexes = new List<int>();

            if (string.IsNullOrEmpty(main) || string.IsNullOrEmpty(subtext))
                throw new ArgumentException("Either main or subtext argument is invalie");

            //scan main string
            for (int i = 0; i < main.Length; i++)
            {
                bool isMatch = true;
                //compare all chars of substring
                for (int j = 0; j < subtext.Length; j++)
                {
                    if ((i + j) < main.Length)
                    {
                        if (char.ToUpperInvariant(main[i + j]) != char.ToUpperInvariant(subtext[j]))
                            isMatch = false;
                    }
                    else
                    {
                        isMatch = false;
                    }
                }

                if (isMatch)
                {
                    //Console.WriteLine($"match found at {i}");
                    matchIndexes.Add(i + 1);//output requires index+1
                    //once a match is found, move index forward for main string to keep searching
                    i += subtext.Length;

                    if (i > (main.Length - 1))
                        i = main.Length - 1;
                }
            }

            return matchIndexes.ToArray();
        }
    }

}
