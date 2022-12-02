namespace AdventOfCode.Solutions.Year2022.Day02;

class Solution : SolutionBase
{
    public Solution() : base(02, 2022, "Rock Paper Scissors") { }

    protected override string SolvePartOne() => Input
        .SplitByNewline()
        .Aggregate(0, (score, round) => score + round[2] switch
        {
            'X' => 1 + round[0] switch
            {
                'C' => 6,
                'A' => 3,
                _ => 0
            },
            'Y' => 2 + round[0] switch
            {
                'A' => 6,
                'B' => 3,
                _ => 0
            },
            'Z' => 3 + round[0] switch
            {
                'B' => 6,
                'C' => 3,
                _ => 0
            },
            _ => throw new InvalidGameException()
        })
        .ToString();

    protected override string SolvePartTwo() => Input
        .SplitByNewline()
        .Aggregate(0, (score, round) => score + round[2] switch
        {
            'X' => 0 + round[0] switch
            {
                'A' => 3,
                'B' => 1,
                'C' => 2,
                _ => throw new InvalidGameException()
            },
            'Y' => 3 + round[0] switch
            {
                'A' => 1,
                'B' => 2,
                'C' => 3,
                _ => throw new InvalidGameException()
            },
            'Z' => 6 + round[0] switch
            {
                'A' => 2,
                'B' => 3,
                'C' => 1,
                _ => throw new InvalidGameException()
            },
            _ => throw new InvalidGameException()
        })
        .ToString();
}

class InvalidGameException : Exception { }
