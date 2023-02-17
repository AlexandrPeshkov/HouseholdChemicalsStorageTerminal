using System;
using DataAccess;
using UnityEngine;
using Random = System.Random;

namespace Core.Extensions
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

        public static string MobilePhoneNumber(this Random random)
        {
            return $"+7({random.Range(234, 999)})-{random.Range(111, 999)}-{random.Range(10, 99)}-{random.Range(10, 99)}";
        }
        
        public static string LocalPhoneNumber(this Random random)
        {
            return $"+7(863) {random.Range(234, 999)}{random.Range(11, 99)}-{random.Range(11, 99)}";
        }

        public static string ServicePhoneNumber(this Random random)
        {
            return $"0{random.Range(1, 4)}";
        }

        public static string RandomNumberFor(this Random random, AccountTypeEnum accountTypeEnum)
        {
            switch (accountTypeEnum)
            {
                case AccountTypeEnum.Home: return LocalPhoneNumber(random);
                case AccountTypeEnum.Mobile: return MobilePhoneNumber(random);
                case AccountTypeEnum.Service: return ServicePhoneNumber(random);
                default: return null;
            }
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