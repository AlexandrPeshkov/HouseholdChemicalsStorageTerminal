using System;
using UnityEngine;
using Random = System.Random;

namespace HC.Core.Extensions
{
    public static class RandomExtensions
    {
        public static float Range(this Random random, float minimum, float maximum)
        {
            if (Mathf.Approximately(minimum, maximum))
            {
                return minimum;
            }

            return ((float)random.NextDouble()) * (maximum - minimum) + minimum;
        }

        public static int Range(this Random random, int minimum, int maximum)
        {
            if (maximum == minimum)
            {
                return minimum;
            }

            return random.Next(minimum, maximum);
        }

        public static DateTime NearDateTime(this Random random)
        {
            var start = new DateTime(2021, 1, 1);
            var range = (DateTime.Today - start).Days;
            var date = start.AddDays(random.Next(range));
            date = date.AddHours(random.Next(20));
            date = date.AddMinutes(random.Next(60));
            date = date.AddSeconds(random.Next(60));
            return date;
        }

        public static string PhoneNumber(this Random random)
        {
            return $"+7({random.Range(234, 999)})-{random.Range(111, 999)}-{random.Range(10, 99)}-{random.Range(10, 99)}";
        }
        
        /// <summary>
        /// Подбросить монетку
        /// </summary>
        /// <param name="random">System.Random</param>
        /// <returns>True/False</returns>
        public static bool FlipCoin(this Random random)
        {
            return random.Next(0, 2) == 0;
        }
    }
}