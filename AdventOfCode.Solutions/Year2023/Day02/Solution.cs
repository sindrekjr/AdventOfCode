namespace AdventOfCode.Solutions.Year2023.Day02;

class Solution : SolutionBase
{
    public Solution() : base(02, 2023, "Cube Conundrum") { }

    protected override string SolvePartOne()
    {
        var possibles = 0;
        foreach (var line in Input.SplitByNewline())
        {
            var (game, cubes, _) = line.Split(":");

            if (IsGamePossible(cubes))
            {
                possibles += int.Parse(game.Split(" ").Last());
            }
        }

        return possibles.ToString();
    }

    protected override string SolvePartTwo()
    {
        var powerSum = 0;
        foreach (var line in Input.SplitByNewline())
        {
            var (_, cubes, _) = line.Split(":");

            var (red, green, blue) = LeastPossibleCubes(cubes);
            powerSum += red * green * blue;
        }

        return powerSum.ToString();
    }

    internal static readonly char[] separator = [',', ';'];

    bool IsGamePossible(string cubes)
    {
        foreach (var cube in cubes.Split(separator, StringSplitOptions.TrimEntries))
        {
            var (n, c, _) = cube.Split(" ");
            var count = int.Parse(n);

            var possible = c switch
            {
                "red" when count > 12 => false,
                "green" when count > 13 => false,
                "blue" when count > 14 => false,
                _ => true,
            };

            if (!possible)
            {
                return false;
            }
        }

        return true;
    }

    (int red, int green, int blue) LeastPossibleCubes(string cubes)
    {
        var red = 0;
        var green = 0;
        var blue = 0;

        foreach (var cube in cubes.Split(separator, StringSplitOptions.TrimEntries))
        {
            var (n, c, _) = cube.Split(" ");
            var count = int.Parse(n);

            if (c == "red" && count > red) red = count;
            if (c == "green" && count > green) green = count;
            if (c == "blue" && count > blue) blue = count;
        }

        return (red, green, blue);
    }
}
