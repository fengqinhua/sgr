using Sgr;
using System;
using System.Linq;
using System.Text;

namespace System.Collections.Generic
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        public static bool TryAddItem<T>(this ICollection<T> source, T item)
        {
            Check.NotNull(source, nameof(source));

            if (source.Contains(item))
                return false;

            source.Add(item);
            return true;
        }

        public static void TryAddItems<T>(this ICollection<T> source, IEnumerable<T> items)
        {
            Check.NotNull(source, nameof(source));

            if (items != null)
            {
                foreach (var item in items)
                {
                    if (source.Contains(item))
                        continue;

                    source.Add(item);
                }
            }
        }

        public static void RemoveAll<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            if (collection is List<T> list)
            {
                list.RemoveAll(new Predicate<T>(predicate));
            }
            else
            {
                var itemsToDelete = collection.Where(predicate).ToArray();
                foreach (var item in itemsToDelete)
                {
                    collection.Remove(item);
                }
            }
        }
    }
}
