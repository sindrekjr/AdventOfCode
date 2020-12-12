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

        public IEnumerable<IEnumerable<T>> PeekAround((int x, int y) position, int radius = -1, bool diagonal = true)
        {
            yield return radius == -1 ? RelativelyIncrementalPeek(position, (-1, 0)) : RelativelyIncrementalPeek(position, (-1, 0)).Take(radius);
            yield return radius == -1 ? RelativelyIncrementalPeek(position, (0, 1)) : RelativelyIncrementalPeek(position, (0, 1)).Take(radius);
            yield return radius == -1 ? RelativelyIncrementalPeek(position, (1, 0)) : RelativelyIncrementalPeek(position, (1, 0)).Take(radius);
            yield return radius == -1 ? RelativelyIncrementalPeek(position, (0, -1)) : RelativelyIncrementalPeek(position, (0, -1)).Take(radius);
            
            if (diagonal)
            {
                yield return radius == -1 ? RelativelyIncrementalPeek(position, (1, 1)) : RelativelyIncrementalPeek(position, (1, 1)).Take(radius);
                yield return radius == -1 ? RelativelyIncrementalPeek(position, (1, -1)) : RelativelyIncrementalPeek(position, (1, -1)).Take(radius);
                yield return radius == -1 ? RelativelyIncrementalPeek(position, (-1, 1)) : RelativelyIncrementalPeek(position, (-1, 1)).Take(radius);
                yield return radius == -1 ? RelativelyIncrementalPeek(position, (-1, -1)) : RelativelyIncrementalPeek(position, (-1, -1)).Take(radius);
            }
        }

        IEnumerable<T> RelativelyIncrementalPeek((int x, int y) start, (int x, int y) increment)
        {
            var pos = start.Add(increment);
            while (TryGetValue(pos, out T next)) 
            {
                pos = pos.Add(increment);
                yield return next;
            }
        }
    }
}