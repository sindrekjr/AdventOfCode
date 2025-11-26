namespace AdventOfCode.Solutions.Year2023.Day01;

class Solution : SolutionBase
{
    public Solution() : base(01, 2023, "Trebuchet?!") { }

    protected override string? SolvePartOne()
    {
        return null;

        // var values = Input.SplitByNewline().Select(line =>
        // {
        //     var digits = line.ToIntArray();
        //     return int.Parse($"{digits.First()}{digits.Last()}");
        // });
        // return values.Sum().ToString();
    }

    protected override string? SolvePartTwo()
    {
        return null;

        // var values = Input.SplitByNewline().Select(line =>
        // {
        //     var digits = TranslateTextNumbers(line).ToIntArray();
        //     return int.Parse($"{digits.First()}{digits.Last()}");
        // });
        // return values.Sum().ToString();
    }

    // string TranslateTextNumbers(string text) => text
    //     .Replace("one", "o1e")
    //     .Replace("two", "t2o")
    //     .Replace("three", "t3e")
    //     .Replace("four", "f4r")
    //     .Replace("five", "f5e")
    //     .Replace("six", "s6x")
    //     .Replace("seven", "s7n")
    //     .Replace("eight", "e8t")
    //     .Replace("nine", "n9e")
    //     .Replace("zero", "z0o");
}
