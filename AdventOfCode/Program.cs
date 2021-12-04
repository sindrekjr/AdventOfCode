global using System;
global using System.Collections.Generic;
global using System.Linq;

using AdventOfCode.Infrastructure;
using AdventOfCode.Infrastructure.Helpers;

foreach (var solution in new SolutionCollector())
{
    Console.WriteLine();
    foreach (var line in FormatHelper.FunctionFormat(solution))
    {
        Console.WriteLine(line);
    }
}
