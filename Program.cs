using AdventOfCode.Solutions;

var arguments = args.ToList();
var config = Config.Get();
var year = config.Year;
var days = config.Days;
var targets = config.Targets;
var debug = arguments.Remove("debug");

if (arguments.Count > 0)
{
    days = DaysConverter.ParseString(args.First()).ToArray();
}

Console.WriteLine();
foreach (var solution in SolutionCollector.FetchSolutions(year, days, targets, debug))
{
    Console.WriteLine(solution.ToString());
}
