namespace EventsHW.Extensions
{
    public static class EnumerableExtensions
    {
        public static T GetMax<T>(this IEnumerable<T> collection, Func<T, float> convertToNumber) where T : class
        {
            ArgumentNullException.ThrowIfNull(collection);
            ArgumentNullException.ThrowIfNull(convertToNumber);

            T maxItem = null;
            float maxValue = float.MinValue;
            bool foundAny = false;
            foreach (var item in collection)
            {
                float value = convertToNumber(item);
                if (!foundAny || value > maxValue)
                {
                    maxValue = value;
                    maxItem = item;
                    foundAny = true;
                }
            }

            if (!foundAny)
                throw new InvalidOperationException("Collection contains no elements.");

            return maxItem;
        }
    }
}
