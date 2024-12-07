using AdventOfCode.Solutions;

var config = Config.Get();
var year = config.Year;
var days = config.Days;

if (args.Length > 0) days = DaysConverter.ParseString(args.First()).ToArray();

foreach (var solution in SolutionCollector.FetchSolutions(year, days))
{
    Console.WriteLine(solution.ToString());
}
