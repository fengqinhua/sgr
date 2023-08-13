using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Collections.Generic
{
    public static class CollectionExtensions
    {
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
