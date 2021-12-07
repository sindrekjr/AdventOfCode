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
            var positions = Input.ToIntArray(",");

            var minimum = 0;
            foreach (var target in Enumerable.Range(0, positions.Max()))
            {
                var fuel = positions.Aggregate(0, (acc, p) =>
                {
                    var cost = Math.Abs(p - target) * (Math.Abs(p - target) + 1) / 2;
                    return cost + acc;
                });

                if (minimum == 0 || minimum > fuel) minimum = fuel;
            }

            return minimum.ToString();
        }
    }
}
