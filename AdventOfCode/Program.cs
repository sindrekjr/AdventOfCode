using System;
using AdventOfCode.Infrastructure;
using AdventOfCode.Infrastructure.Helpers;

namespace AdventOfCode
{
    class Program
    {
        static readonly SolutionCollector Solutions = new SolutionCollector();

        static void Main(string[] args)
        {
            foreach (var solution in Solutions)
            {
                Console.WriteLine();
                foreach (var line in FormatHelper.FunctionFormat(solution))
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}
