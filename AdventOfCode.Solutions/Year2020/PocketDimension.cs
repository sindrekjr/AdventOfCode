using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020;

public class PocketDimension<T> : Dictionary<(int x, int y, int z, int w), T>
{
    public PocketDimension<T>? InfiniteChildren { get; private set; }

    public PocketDimension() : base() {}

    public PocketDimension(PocketDimension<T> pocketDimension) : base(pocketDimension) {}

    public PocketDimension(T[][][][] positions) : base()
    {
        for (int x = 0; x < positions.Length; x++)
        {
            for (int y = 0; y < positions[x].Length; y++)
            {
                for (int z = 0; z < positions[x][y].Length; z++)
                {
                    for (int w = 0; w < positions[x][y][z].Length; w++)
                    {
                        Add((x, y, z, w), positions[x][y][z][w]);
                    }
                }
            }
        }
    }

    public IEnumerable<IEnumerable<T>> PokeAround((int x, int y, int z, int w) position, int radius = 1)
    {
        if (InfiniteChildren == null) InfiniteChildren = new PocketDimension<T>();
        for (int x = -1; x <= 1; x++) for (int y = -1; y <= 1; y++) for (int z = -1; z <= 1; z++) for (int w = -1; w <= 1; w++)
        {
            if (x == 0 && y == 0 && z == 0 && w == 0) continue;
            yield return RelativelyIncrementalPeek(position, (x, y, z, w), radius);
        }
    }

    IEnumerable<T> RelativelyIncrementalPeek((int x, int y, int z, int w) start, (int x, int y, int z, int w) increment, int count)
    {
        while (count-- > 0)
        {
            start = (start.x + increment.x, start.y + increment.y, start.z + increment.z, start.w + increment.w);

            if (PokePosition(start, out T? found) && found != null) 
            {
                yield return found;
            }
            else if (found != null)
            {
                if (InfiniteChildren == null) InfiniteChildren = new();
                InfiniteChildren.Add(start, found);
                yield return found;
            }
        }
    }

    public bool PokePosition((int x, int y, int z, int w) key, out T? value)
    {
        if (InfiniteChildren != null && InfiniteChildren.TryGetValue(key, out T? foundChild))
        {
            value = foundChild;
            return true;
        }
        
        if (TryGetValue(key, out T? found))
        {
            value = found;
            return true;
        }

        value = default;
        return false;
    }
}
