namespace DeckOfCardsGame.Common.Extensions
{
    public static class ThreadSafeRandom
    {
        [ThreadStatic] private static Random? _local;

        public static Random Instance
        {
            get { return _local ??= new Random(unchecked(Environment.TickCount * 13 + Environment.CurrentManagedThreadId)); }
        }
    }

    public static class ListShuffleExtension
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                //Fisher-Yates shuffle:
                n--;
                var k = ThreadSafeRandom.Instance.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}
