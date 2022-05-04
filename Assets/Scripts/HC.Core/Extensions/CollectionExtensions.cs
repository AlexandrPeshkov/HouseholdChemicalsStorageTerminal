using System.Collections.Generic;
using HC.Core.Services;

namespace HC.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static T GetRandom<T>(this IReadOnlyCollection<T> collection)
        {
            var collectionSize = collection.Count;

            if (collectionSize == 0)
            {
                return default;
            }

            if (collectionSize == 1)
            {
                return collection.GetByIndex(0);
            }

            var randomIndex = ThreadSafeRandom.Random.Range(0, collection.Count - 1);
            return collection.GetByIndex(randomIndex);
        }

        public static T GetByIndex<T>(this IEnumerable<T> collection, int index)
        {
            if (collection == null)
            {
                return default;
            }

            var currentIndex = 0;

            foreach (var element in collection)
            {
                if (currentIndex == index)
                {
                    return element;
                }

                currentIndex++;
            }

            return default;
        }
    }
}