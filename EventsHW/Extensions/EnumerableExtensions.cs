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
            foreach (var item in collection)
            {
                float value = convertToNumber(item);
                if (maxItem == null || value > maxValue)
                {
                    maxValue = value;
                    maxItem = item;
                }
            }
            return maxItem;
        }
    }
}
