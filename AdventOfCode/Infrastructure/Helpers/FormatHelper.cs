using System.Collections.Generic;
using AdventOfCode.Infrastructure.Models;
using AdventOfCode.Solutions;

namespace AdventOfCode.Infrastructure.Helpers
{
    static class FormatHelper
    {
        public static IEnumerable<string> SimpleFormat(ASolution solution)
        {
            yield return $"--- {FormatTitle(solution.Day, solution.Title)} ---";
            if (solution.Debug) yield return FormatDebug(solution.DebugInput);
            yield return $"Part 1: {solution.Part1.Answer}";
            yield return $"Part 2: {solution.Part2.Answer}";
        }

        public static IEnumerable<string> FunctionFormat(ASolution solution)
        {
            yield return FormatTitle(solution.Day, solution.Title);
            if (solution.Debug) yield return FormatDebug(solution.DebugInput);
            yield return FormatPart(1, solution.Part1);
            yield return FormatPart(2, solution.Part2);
        }

        public static string FormatTitle(int day, string title) => $"Day {day}: {title}";

        public static string FormatDebug(string debugInput) => "!! Debug mode active, using DebugInput";

        public static string FormatPart(int part, SolutionResult result)
            => $"  - Part{part} => " + (string.IsNullOrEmpty(result.Answer) ? "Unsolved" : $"{result.Answer} ({result.Time.TotalMilliseconds}ms)");
    }
}