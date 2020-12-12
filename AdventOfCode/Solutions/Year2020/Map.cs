using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{
    public class Map<T> : Dictionary<(int x, int y), T>
    {
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

        IEnumerable<T> PokeAround((int x, int y) position)
        {
            var (x, y) = position;
            if (TryGetValue((x, y + 1), out T up)) yield return up;
            if (TryGetValue((x + 1, y), out T rt)) yield return rt;
            if (TryGetValue((x, y - 1), out T dw)) yield return dw;
            if (TryGetValue((x - 1, y), out T lf)) yield return lf;
        }
    }
}