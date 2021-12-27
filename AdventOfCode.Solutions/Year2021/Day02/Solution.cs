namespace AdventOfCode.Solutions.Year2021.Day02;

class Solution : SolutionBase
{
    public Solution() : base(02, 2021, "Dive!")
    {
    }

    protected override string SolvePartOne()
    {
        var x = 0;
        var y = 0;

        foreach (var instruction in Input.SplitByNewline())
        {
            var (cmd, amt, _) = instruction.Split(" ");
            var units = int.Parse(amt);

            switch (cmd)
            {
                case "forward":
                    x += units;
                    break;
                case "up":
                    y -= units;
                    break;
                case "down":
                    y += units;
                    break;
            }
        }

        return (x * y).ToString();
    }

    protected override string SolvePartTwo()
    {
        var a = 0;
        var x = 0;
        var y = 0;

        foreach (var instruction in Input.SplitByNewline())
        {
            var (cmd, amt, _) = instruction.Split(" ");
            var units = int.Parse(amt);

            switch (cmd)
            {
                case "up":
                    a -= units;
                    break;
                case "down":
                    a += units;
                    break;
                case "forward":
                    x += units;
                    y += units * a;
                    break;
            }
        }

        return (x * y).ToString();
    }
}
