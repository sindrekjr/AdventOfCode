using AdventOfCode.Solutions;

var arguments = args.ToList();
var config = Config.Get();
var year = config.Year;
var days = config.Days;
var debug = arguments.Remove("debug");

if (arguments.Count > 0)
{
    days = DaysConverter.ParseString(args.First()).ToArray();
}

foreach (var solution in SolutionCollector.FetchSolutions(year, days, debug))
{
    Console.WriteLine(solution.ToString());
}
