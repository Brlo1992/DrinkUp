using System.Collections.Generic;
using System.Linq;

namespace DrinkUp.WebApi.Extensions {
    public static class CollectionExtension {

        public static bool IsEmpty<T>(this IEnumerable<T> collection) => collection.Any() == false;

        public static bool IsNotEmpty<T>(this IEnumerable<T> collection) => collection.Any();

        public static bool IsOneSelected<T>(this IEnumerable<T> collection) {
            var enumerable = collection as IList<T> ?? collection.ToList();
            return enumerable.IsNotEmpty() &&
                   enumerable.Count.IsEqualTo((int)IntValues.One);
        }

        public static bool IsManySelected<T>(this IEnumerable<T> collection) {
            var enumerable = collection as IList<T> ?? collection.ToList();
            return enumerable.IsNotEmpty() &&
                   enumerable.Count.IsGreaterThan((int)IntValues.One);
        }
    }
}
