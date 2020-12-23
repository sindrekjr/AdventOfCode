using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{
    public class Grid<T> : Dictionary<(int x, int y, int z), T>
    {
        public Grid<T> InfiniteChildren { get; private set; }

        public Grid() : base() {}

        public Grid(Grid<T> grid) : base(grid) {}

        public Grid(T[][][] positions) : base()
        {
            for (int x = 0; x < positions.Length; x++)
            {
                for (int y = 0; y < positions[x].Length; y++)
                {
                    for (int z = 0; z < positions[x][y].Length; z++)
                    {
                        Add((x, y, z), positions[x][y][z]);
                    }
                }
            }
        }

        public IEnumerable<IEnumerable<T>> PeekAround((int x, int y, int z) position, int radius = 1)
        {
            if (InfiniteChildren == null) InfiniteChildren = new Grid<T>();
            for (int x = -1; x <= 1; x++) for (int y = -1; y <= 1; y++) for (int z = -1; z <= 1; z++)
            {
                if (x == 0 && y == 0 && z == 0) continue;
                yield return RelativelyIncrementalPeek(position, (x, y, z), radius);
            }
        }

        IEnumerable<T> RelativelyIncrementalPeek((int x, int y, int z) start, (int x, int y, int z) increment, int count)
        {
            while (count-- > 0)
            {
                start = (start.x + increment.x, start.y + increment.y, start.z + increment.z);

                if (PokePosition(start, out T found)) 
                {
                    yield return found;
                }
                else
                {
                    InfiniteChildren.Add(start, found);
                    yield return found;
                }
            }
        }

        public bool PokePosition((int x, int y, int z) key, out T value)
        {
            if (InfiniteChildren != null && InfiniteChildren.TryGetValue(key, out T foundChild))
            {
                value = foundChild;
                return true;
            }
            
            if (TryGetValue(key, out T found))
            {
                value = found;
                return true;
            }

            value = default;
            return false;
        }
    }
}