namespace AdventOfCode.Solutions.Year2020
{
    public class Map<T> : SortedDictionary<(int x, int y), T>
    {
        public Map() : base() {}

        public Map(T[][] positions) : base()
        {
            for (int x = 0; x < positions.Length; x++)
            {
                for (int y = 0; y < positions[x].Length; y++)
                {
                    Add((x, y), positions[x][y]);
                }
            }
        }

        public IEnumerable<IEnumerable<T>> PokeAround((int x, int y) position, int radius = -1, bool diagonal = true)
        {
            for (int x = -1; x <= 1; x++) for (int y = -1; y <= 1; y++)
            {
                if ((x == 0 && y == 0) || (!diagonal && (x != 0 && y != 0))) continue;
                yield return RelativelyIncrementalPoke(position, (x, y), radius);
            }
        }

        IEnumerable<T> RelativelyIncrementalPoke((int x, int y) start, (int x, int y) increment, int count)
        {
            var pos = start.Add(increment);
            while ((count == -1 || count-- > 0) && TryGetValue(pos, out T next)) 
            {
                pos = pos.Add(increment);
                yield return next;
            }
        }
    }
}