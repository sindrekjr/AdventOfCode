
namespace AdventOfCode.Solutions.Year2023.Day09;

class Solution : SolutionBase
{
    public Solution() : base(09, 2023, "Mirage Maintenance") { }

    protected override string? SolvePartOne()
    {
        return Input.SplitByNewline().Aggregate(0, (sum, line) =>
        {
            return sum + PredictNextValue(line.ToIntArray(" "));
        }).ToString();
    }

    protected override string? SolvePartTwo()
    {
        return Input.SplitByNewline().Aggregate(0, (sum, line) =>
        {
            return sum + PredictNextValue(line.ToIntArray(" ").Reverse().ToArray());
        }).ToString();
    }

    private int PredictNextValue(int[] values)
    {
        var diff = new int[values.Length - 1];
        for (var i = 1; i < values.Length; i++)
        {
            diff[i - 1] = values[i] - values[i - 1];
        }

        if (diff.All(n => n is 0))
        {
            return values.Last();
        }

        return values.Last() + PredictNextValue(diff);
    }
}
