using AdventOfCode.Solutions.Year2020;

namespace AdventOfCode.Solutions.Year2021.Day22;

class Solution : SolutionBase
{
    public Solution() : base(22, 2021, "Reactor Reboot") { }

    protected override string? SolvePartOne()
    {
        var grid = new Grid<bool>();
        var instructions = Input.SplitByNewline().Select(ParseInputLine);
        
        foreach (var (desSt, steps) in instructions)
        {
            if (!IsInitialization(steps.x) || !IsInitialization(steps.y) || !IsInitialization(steps.z)) continue;
            
            for (int x = steps.x.Min(); x <= steps.x.Max(); x++)
            {
                for (int y = steps.y.Min(); y <= steps.y.Max(); y++)
                {
                    for (int z = steps.z.Min(); z <= steps.z.Max(); z++)
                    {
                        grid[(x, y, z)] = desSt;
                    }
                }
            }
        }

        return grid.Values.Count(val => val).ToString();
    }

    protected override string? SolvePartTwo()
    {
        var cubes = Input.SplitByNewline().Select(ParseInputLineAsCubeRange);
        return cubes.First().Count().ToString();
    }

    bool IsInitialization(params int[] values) =>
        !values.Any(val => val > 50 || val < -50);

    Cuboid ParseInputLineAsCubeRange(string line)
    {
        var (trigger, (x, y, z)) = ParseInputLine(line);
        throw new NotImplementedException();
    }

    (bool trigger, (int[] x, int[] y, int[] z) steps) ParseInputLine(string line)
    {
        var (trigger, prms, _) = line.Split(" ");
        var sections = prms.Split(",").Select(p => p.Split("=")[1]).ToArray();

        var x = sections[0].Split("..").Select(int.Parse).ToArray();
        var y = sections[1].Split("..").Select(int.Parse).ToArray();
        var z = sections[2].Split("..").Select(int.Parse).ToArray();
        
        return (trigger == "on", (x, y, z));
    }
}

internal record Cuboid(int[] X, int[] Y, int[] Z, bool on)
{
    public int Count()
    {
        var count = 0;
        for (int x = X.Min(); x <= X.Max(); x++)
        {
            for (int y = Y.Min(); y <= Y.Max(); y++)
            {
                for (int z = Z.Min(); z <= Z.Max(); z++)
                {
                    count++;
                }
            }
        }
        return count;
    }
}
