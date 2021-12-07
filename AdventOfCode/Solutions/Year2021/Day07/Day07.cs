using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    class Day07 : ASolution
    {
        public Day07() : base(07, 2021, "The Treachery of Whales") { }

        protected override string SolvePartOne()
        {
            var positions = Input.ToIntArray(",");
            Array.Sort(positions);

            var middle = positions[positions.Length / 2];
            return positions.Aggregate(0, (acc, p) => acc + Math.Abs(p - middle)).ToString();
        }

        protected override string SolvePartTwo()
        {
            return null;
        }
    }
}
