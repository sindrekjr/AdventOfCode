namespace AdventOfCode.Solutions.Year2022.Day03;

class Solution : SolutionBase
{
    public Solution() : base(03, 2022, "Rucksack Reorganization") { }

    protected override string? SolvePartOne()
    {
        return Input
            .SplitByNewline()
            .Select(line =>
            {
                var x = line.Substring(0, line.Length / 2);
                var y = line.Substring(line.Length / 2);
                return x.Intersect(y).ToArray()[0];
            })
            .Aggregate(0, (pri, item) => pri + Array.IndexOf(GetAlphabetArray(), item) + 1)
            .ToString();
    }

    protected override string? SolvePartTwo()
    {
        var sum = 0;
        var index = 0;
        var alphabet = GetAlphabetArray();
        var input = Input.SplitByNewline();
        while (index < input.Count())
        {
            var group = input.Skip(index).Take(3);
            var badge = group.IntersectAll().ToArray()[0];
            sum += Array.IndexOf(alphabet, badge) + 1;
            index += 3;
        }

        return sum.ToString();
    }

    private char[] GetAlphabetArray() =>
        Enumerable.Range('a', 26).Select(x => (char) x)
            .Concat(Enumerable.Range('A', 26).Select(x => (char) x))
            .ToArray();
}
