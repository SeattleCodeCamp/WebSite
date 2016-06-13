using System;
using System.Collections.Generic;

namespace CC.UI.Webhost.Infrastructure
{
    public static class ListExtensions
    {
        //Got this from http://stackoverflow.com/questions/273313/randomize-a-listt-in-c-sharp
        public static void Shuffle<T>(this IList<T> list)
        {
            var randomGenerator = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = randomGenerator.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}