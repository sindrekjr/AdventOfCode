namespace AdventOfCode.Solutions.Utils;

public static class CollectionUtils
{
    public static void Deconstruct<T>(this IList<T> list, out T first, out IList<T> rest)
    {
        if (list.Count < 1) throw new InvalidOperationException();
        first = list[0];
        rest = list.Skip(1).ToList();
    }

    public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out IList<T> rest)
    {
        if (list.Count < 2) throw new InvalidOperationException();
        first = list[0];
        second = list[1];
        rest = list.Skip(2).ToList();
    }

    public static IEnumerable<T> IntersectAll<T>(this IEnumerable<IEnumerable<T>> input)
        => input.Aggregate(input.First(), (intersector, next) => intersector.Intersect(next));

    public static string JoinAsStrings<T>(this IEnumerable<T> items, string delimiter = "") =>
        string.Join(delimiter, items);

    public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> values) => values.Count() == 1
        ? new[] { values }
        : values.SelectMany(v =>
            Permutations(values.Where(x => x?.Equals(v) == false)), (v, p) => p.Prepend(v));

    public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> array, int size)
    {
        for (var i = 0; i < (float)array.Count() / size; i++)
        {
            yield return array.Skip(i * size).Take(size);
        }
    }
}
