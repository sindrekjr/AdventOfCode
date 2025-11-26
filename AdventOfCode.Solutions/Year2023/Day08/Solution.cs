namespace AdventOfCode.Solutions.Year2023.Day08;

class Solution : SolutionBase
{
    public Solution() : base(08, 2023, "Haunted Wasteland") { }

    protected override string? SolvePartOne()
    {
        var lines = Input.SplitByNewline();
        var instructions = lines.First().ToCharArray();

        var mappings = new Dictionary<string, (string, string)>();
        foreach (var line in lines.Skip(1))
        {
            var (node, leftright, _) = line.Split(" = ");
            var (left, right, _) = leftright.Trim(['(', ')']).Split(",", StringSplitOptions.TrimEntries);
            mappings.Add(node, (left, right));
        }

        var steps = 0;
        var instruction = 0;
        var current = "AAA";
        while (current != "ZZZ")
        {
            if (instruction == instructions.Length)
            {
                instruction = 0;
            }

            steps++;
            var (left, right) = mappings[current];
            current = instructions[instruction++] is 'L'
                ? left
                : right;
        }

        return steps.ToString();
    }

    protected override string? SolvePartTwo() => null;
}
