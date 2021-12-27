using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2021.Day18;

class Solution : SolutionBase
{
    public Solution() : base(18, 2021, "Snailfish") { }

    protected override string SolvePartOne() =>
        CalculateMagnitude(Input.SplitByNewline().Aggregate(Add)).ToString();

    protected override string SolvePartTwo()
    {
        var sums = new List<string>();
        var lines = Input.SplitByNewline();
        for (int i = 0; i < lines.Length; i++) for (int j = i + 1; j < lines.Length; j++)
        {
            sums.Add(Add(lines[i], lines[j]));
            sums.Add(Add(lines[j], lines[i]));
        }

        return sums.Max(sum => CalculateMagnitude(sum)).ToString();
    }

    long CalculateMagnitude(string sum)
    {
        if (!sum.Contains('[') && !sum.Contains(']') && sum.Any())
        {
            var nums = sum.Split(",").Select(int.Parse).ToArray();
            if (nums.Length == 1) return nums[0];
            return 3 * nums[0] + 2 * nums[1];
        }

        var depth = 0;
        var first = "";
        for (int i = 0; i < sum.Length; i++)
        {
            var ch = sum[i];
            first += ch;
            if (ch == '[') depth++;
            if (ch == ']') depth--;
            if (depth <= 0) break;
        }

        if (first.Length == sum.Count()) return CalculateMagnitude(first.Skip(1).SkipLast(1).JoinAsStrings());

        depth = 0;
        var second = "";
        for (int i = first.Length + 1; i < sum.Length; i++)
        {
            var ch = sum[i];
            second += ch;
            if (ch == '[') depth++;
            if (ch == ']') depth--;
            if (depth <= 0) break;
        }

        return 3 * CalculateMagnitude(first) + 2 * CalculateMagnitude(second);
    }

    string Add(string first, string second) => ReduceLine($"[{first},{second}]");

    string ReduceLine(string line)
    {
        while (true)
        {
            var prev = line;

            var exploded = ExplodeOne(line);
            if (exploded != prev)
            {
                line = exploded;
                continue;
            }

            var split = SplitOne(line);
            if (split != prev)
            {
                line = split;
                continue;
            }

            return prev;
        }
    }

    Regex ExStaRx = new Regex(@"(\d+)(\D+)$");
    Regex ExEndRx = new Regex(@"(\d+)");

    string ExplodeOne(string line)
    {
        var depth = 0;
        for (int i = 0; i < line.Length; i++)
        {
            var ch = line[i];
            if (ch == '[') depth++;
            if (ch == ']') depth--;

            if (depth > 4)
            {
                var match = Regex.Match(line.Substring(i), @"(\d+),(\d+)");

                var full = match.Value;
                var (left, right, _) = full.Split(",");

                var start = line.Substring(0, i);
                start = ExStaRx.Replace(start, m =>
                {
                    var (_, d, p) = m.Groups.Values.Select(v => v.Value).ToArray();
                    return $"{int.Parse(d) + int.Parse(left)}{p.JoinAsStrings()}";
                }, 1);

                var end = line.Substring(i + full.Length + 2);
                end = ExEndRx.Replace(end, m =>
                {
                    var val = m.Value;
                    return $"{int.Parse(val) + int.Parse(right)}";
                }, 1);

                return $"{start}0{end}";
            }
        }

        return line;
    }

    Regex SplitRx = new Regex(@"(\d\d+)");

    string SplitOne(string line) => SplitRx.Replace(line, match =>
    {
        var value = int.Parse(match.Value);
        return $"[{Math.Floor(value / 2d)},{Math.Ceiling(value / 2d)}]";
    }, 1);
}
