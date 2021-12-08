using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    class Day08 : ASolution
    {
        public Day08() : base(08, 2021, "Seven Segment Search")
        {

        }

        protected override string SolvePartOne() => Input.SplitByNewline() 
            .Aggregate(0, (acc, entry) => entry
                .Split(" | ")[1]
                .Split(" ")
                .Aggregate(acc, (a, output) =>
                {
                    var len = output.Length;
                    if (len == 2 || len == 4 || len == 3 || len == 7) return a + 1;
                    return a;
                })).ToString();

        protected override string SolvePartTwo()
        {
            return null;
        }
    }
}
