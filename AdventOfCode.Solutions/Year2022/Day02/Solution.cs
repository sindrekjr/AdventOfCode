namespace AdventOfCode.Solutions.Year2022.Day02;

class Solution : SolutionBase
{
    public Solution() : base(02, 2022, "Rock Paper Scissors") { }

    protected override string? SolvePartOne() => Input
        .SplitByNewline()
        .Aggregate(0, (score, round) => score + (round[0], round[2]) switch
        {
            ('A', 'X') => 4,
            ('B', 'X') => 1,
            ('C', 'X') => 7,
            ('A', 'Y') => 8,
            ('B', 'Y') => 5,
            ('C', 'Y') => 2,
            ('A', 'Z') => 3,
            ('B', 'Z') => 9,
            ('C', 'Z') => 6,
            _ => throw new InvalidGameException()
        })
        .ToString();

    protected override string? SolvePartTwo() => Input
        .SplitByNewline()
        .Aggregate(0, (score, round) => score + (round[0], round[2]) switch
        {
            ('A', 'X') => 3,
            ('B', 'X') => 1,
            ('C', 'X') => 2,
            ('A', 'Y') => 4,
            ('B', 'Y') => 5,
            ('C', 'Y') => 6,
            ('A', 'Z') => 8,
            ('B', 'Z') => 9,
            ('C', 'Z') => 7,
            _ => throw new InvalidGameException()
        })
        .ToString();
}

class InvalidGameException : Exception { }
