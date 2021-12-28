namespace AdventOfCode.Solutions.Year2021.Day17;

class Solution : SolutionBase
{
    int MinX;
    int MaxX;
    int MinY;
    int MaxY;

    public Solution() : base(17, 2021, "Trick Shot")
    {
        var (x, y, _) = Input.Split(": ")[1].Split(", ").Select(coor => coor.Substring(2).Split("..").Select(int.Parse)).ToArray();
        MinX = x.Min();
        MaxX = x.Max();
        MinY = y.Min();
        MaxY = y.Max();
    }

    protected override string SolvePartOne()
    {
        var actuallyHighest = 0;
        for (int y = 5; y < 2000; y++)
        {
            var (success, highest) = DryRun((0, 0), (0, y), true);
            if (success) actuallyHighest = highest;
        }

        return actuallyHighest.ToString();
    }

    protected override string SolvePartTwo()
    {
        var successes = 0;
        for (int y = -1000; y < 2000; y++) for (int x = 1; x < 2000; x++)
        {
            if (DryRun((0, 0), (x, y)).success) successes++;
        }

        return successes.ToString();
    }

    (bool success, int maxHeight) DryRun((int x, int y) pos, (int x, int y) vel, bool yTest = false)
    {
        var highest = 0;
        while (pos.x <= MaxX && pos.y >= MinY)
        {
            (pos, vel) = Step(pos, vel);

            if (pos.y > highest) highest = pos.y;

            if (pos.y >= MinY && pos.y <= MaxY)
            {
                if (!yTest && !(pos.x >= MinX && pos.x <= MaxX)) continue;
                return (true, highest);
            }
        }

        return (false, highest);
    }

    ((int x, int y) pos, (int x, int y) velocity) Step((int x, int y) pos, (int x, int y) vel)
    {
        pos.x += vel.x;
        pos.y += vel.y;

        if (vel.x != 0) vel.x -= vel.x > 0 ? 1 : -1;
        vel.y--;

        return (pos, vel);
    }
}
