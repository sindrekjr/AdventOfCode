using Microsoft.FSharp.Collections;

namespace AdventOfCode.Solutions.Year2021.Day01;

class Solution : SolutionBase
{
    readonly FSharpList<int> Measurements;

    public Solution() : base(01, 2021, "Sonar Sweep")
    {
        Measurements = ListModule.OfSeq(Input.ToIntArray("\n"));
    }

    protected override string? SolvePartOne() => FSharp
            .CountIncreases(Measurements, 0)
            .ToString();

    protected override string? SolvePartTwo() => FSharp
        .CountIncreases(
            FSharp.SumGroupsOfThree(Measurements),
            0).ToString();
}
