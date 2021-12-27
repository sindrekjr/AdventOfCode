using System.Linq;

namespace AdventOfCode.Solutions.Year2021.Day08;

class Solution : SolutionBase
{
    public Solution() : base(08, 2021, "Seven Segment Search") { }

    protected override string SolvePartOne() => Input.SplitByNewline().Aggregate(0, (total, entry) =>
    {
        var (_, output, _) = entry.Split(" | ");
        return output
            .Split(" ")
            .Aggregate(total, (acc, value) => value.Length is 2 or 4 or 3 or 7 ? acc + 1 : acc);
    }).ToString();

    protected override string SolvePartTwo() => Input.SplitByNewline().Aggregate(0, (total, entry) =>
    {
        var (input, output, _) = entry.Split(" | ");
        var decodedSignals = DeduceSignals(input.Split(" "));

        var decodedValue = output
            .Split(" ")
            .Select(val => val.OrderBy(ch => ch).JoinAsStrings())
            .Aggregate("", (decodedAcc, value) => decodedAcc + Array.IndexOf(decodedSignals, value));

        return total + int.Parse(decodedValue);
    }).ToString();

    string[] DeduceSignals(string[] signals)
    {
        var one = signals.First(s => s.Length == 2);
        var four = signals.First(s => s.Length == 4);
        var seven = signals.First(s => s.Length == 3);
        var eight = signals.First(s => s.Length == 7);

        var a = seven.First(digit => !one.Contains(digit));
        var b = four.First(digit => !one.Contains(digit) && signals.Count(s => s.Contains(digit)) == 6);
        var c = one.First(digit => signals.Count(s => s.Contains(digit)) == 8);
        var d = four.First(digit => signals.Count(s => s.Contains(digit)) == 7 && digit != b && digit != c);
        var e = eight.First(digit => signals.Count(s => s.Contains(digit)) == 4);
        var f = one.First(digit => digit != c);
        var g = eight.First(digit => digit != e && !one.Contains(digit) && !four.Contains(digit) && !seven.Contains(digit));

        var zero = signals.First(signal => ContainsAll(signal, a, b, c, e, f, g));
        var two = signals.First(signal => ContainsAll(signal, a, c, d, e, g));
        var three = signals.First(signal => ContainsAll(signal, a, c, d, f, g));
        var five = signals.First(signal => ContainsAll(signal, a, b, d, f, g));
        var six = signals.First(signal => ContainsAll(signal, a, b, d, e, f, g));
        var nine = signals.First(signal => ContainsAll(signal, a, b, c, d, f, g));

        return new []
        {
            zero.OrderBy(ch => ch).JoinAsStrings(),
            one.OrderBy(ch => ch).JoinAsStrings(),
            two.OrderBy(ch => ch).JoinAsStrings(),
            three.OrderBy(ch => ch).JoinAsStrings(),
            four.OrderBy(ch => ch).JoinAsStrings(),
            five.OrderBy(ch => ch).JoinAsStrings(),
            six.OrderBy(ch => ch).JoinAsStrings(),
            seven.OrderBy(ch => ch).JoinAsStrings(),
            eight.OrderBy(ch => ch).JoinAsStrings(),
            nine.OrderBy(ch => ch).JoinAsStrings(),
        };
    }

    bool ContainsAll(string str, params char[] chars) =>
        new HashSet<char>(chars).SetEquals(new HashSet<char>(str));
}
