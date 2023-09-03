using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public static class CardCollectionExtensions
{
    public static IEnumerable<Card> GetRandom(this IEnumerable<Card> source, int count)
    {
        return source.OrderBy(_ => Random.value).Take(count);
    }
}