using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public IEnumerable<T>[] PeekAround((int x, int y) position, int radius = -1)
        {
            var directions = new IEnumerable<T>[8];
            directions[0] = radius == -1 ? PeekN(position) : PeekN(position).Take(radius);
            directions[1] = radius == -1 ? PeekNE(position) : PeekNE(position).Take(radius);
            directions[2] = radius == -1 ? PeekE(position) : PeekE(position).Take(radius);
            directions[3] = radius == -1 ? PeekSE(position) : PeekSE(position).Take(radius);
            directions[4] = radius == -1 ? PeekS(position) : PeekS(position).Take(radius);
            directions[5] = radius == -1 ? PeekSW(position) : PeekSW(position).Take(radius);
            directions[6] = radius == -1 ? PeekW(position) : PeekW(position).Take(radius);
            directions[7] = radius == -1 ? PeekNW(position) : PeekNW(position).Take(radius);
            return directions;
        }

        IEnumerable<T> PeekN((int x, int y) start)
        {
            var (x, y) = start;
            while (TryGetValue((--x, y), out T next)) yield return next;
        }

        IEnumerable<T> PeekNE((int x, int y) start)
        {
            var (x, y) = start;
            while (TryGetValue((--x, ++y), out T next)) yield return next;
        }

        IEnumerable<T> PeekE((int x, int y) start)
        {
            var (x, y) = start;
            while (TryGetValue((x, ++y), out T next)) yield return next;
        }

        IEnumerable<T> PeekSE((int x, int y) start)
        {
            var (x, y) = start;
            while (TryGetValue((++x, ++y), out T next)) yield return next;
        }

        IEnumerable<T> PeekS((int x, int y) start)
        {
            var (x, y) = start;
            while (TryGetValue((++x, y), out T next)) yield return next;
        }

        IEnumerable<T> PeekSW((int x, int y) start)
        {
            var (x, y) = start;
            while (TryGetValue((++x, --y), out T next)) yield return next;
        }

        IEnumerable<T> PeekW((int x, int y) start)
        {
            var (x, y) = start;
            while (TryGetValue((x, --y), out T next)) yield return next;
        }

        IEnumerable<T> PeekNW((int x, int y) start)
        {
            var (x, y) = start;
            while (TryGetValue((--x, --y), out T next)) yield return next;
        }
    }
}