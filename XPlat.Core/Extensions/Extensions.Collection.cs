namespace XPlat
{
    using System.Collections.Generic;

    public static partial class Extensions
    {
        /// <summary>
        /// Takes a number of elements from the specified collection from the specified starting index.
        /// </summary>
        /// <param name="list">
        /// The <see cref="List{T}"/> to take items from.
        /// </param>
        /// <param name="startingIndex">
        /// The index to start at in the <see cref="List{T}"/>.
        /// </param>
        /// <param name="takeCount">
        /// The number of items to take from the starting index of the <see cref="List{T}"/>.
        /// </param>
        /// <typeparam name="T">
        /// The type of elements in the collection.
        /// </typeparam>
        /// <returns>
        /// Returns a collection of <see cref="T"/> items.
        /// </returns>
        public static IEnumerable<T> Take<T>(this List<T> list, int startingIndex, int takeCount)
        {
            var results = new List<T>();

            var itemsToTake = takeCount;

            if (list.Count - 1 - startingIndex > itemsToTake)
            {
                var items = list.GetRange(startingIndex, itemsToTake);
                results.AddRange(items);
            }
            else
            {
                itemsToTake = list.Count - startingIndex;
                if (itemsToTake > 0)
                {
                    var items = list.GetRange(startingIndex, itemsToTake);
                    results.AddRange(items);
                }
            }

            return results;
        }

        /// <summary>
        /// Takes a number of elements from the specified collection from the specified starting index.
        /// </summary>
        /// <param name="list">
        /// The <see cref="List{T}"/> to take items from.
        /// </param>
        /// <param name="startingIndex">
        /// The index to start at in the <see cref="List{T}"/>.
        /// </param>
        /// <param name="takeCount">
        /// The number of items to take from the starting index of the <see cref="List{T}"/>.
        /// </param>
        /// <typeparam name="T">
        /// The type of elements in the collection.
        /// </typeparam>
        /// <returns>
        /// Returns a collection of <see cref="T"/> items.
        /// </returns>
        public static IEnumerable<T> Take<T>(this IReadOnlyList<T> list, int startingIndex, int takeCount)
        {
            var results = new List<T>();

            var itemsToTake = takeCount;

            if (list.Count - 1 - startingIndex > itemsToTake)
            {
                var items = list.GetRange(startingIndex, itemsToTake);
                results.AddRange(items);
            }
            else
            {
                itemsToTake = list.Count - startingIndex;
                if (itemsToTake > 0)
                {
                    var items = list.GetRange(startingIndex, itemsToTake);
                    results.AddRange(items);
                }
            }

            return results;
        }

        /// <summary>
        /// Creates a copy of a range of elements in a <see cref="IReadOnlyList{T}"/>.
        /// </summary>
        /// <param name="list">
        /// The list to get a range from.
        /// </param>
        /// <param name="index">
        /// The index to start at.
        /// </param>
        /// <param name="count">
        /// The number of items to get in the range.
        /// </param>
        /// <typeparam name="T">
        /// The type of item in the list.
        /// </typeparam>
        /// <returns>
        /// The range as a collection.
        /// </returns>
        public static IEnumerable<T> GetRange<T>(this IReadOnlyList<T> list, int index, int count)
        {
            var range = new List<T>();
            for (var i = 0; i < count; i++)
            {
                var j = i + index;
                if (j >= index + count)
                {
                    break;
                }
                range.Add(list[j]);
            }
            return range;
        }
    }
}