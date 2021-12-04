using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{

    class Day01 : ASolution
    {

        IEnumerable<int> Report;

        public Day01() : base(01, 2020, "Report Repair")
        {
            Report = Input.ToIntArray("\n").ToHashSet().Where(e => e < 2020);
        }

        protected override string SolvePartOne()
        {
            var expenses = new Queue<int>(Report);
            while (true) 
            {
                var e1 = expenses.Dequeue();
                foreach (var e2 in expenses)
                {
                    if (e1 + e2 == 2020) return (e1 * e2).ToString();
                }
            }
        }

        protected override string SolvePartTwo()
        {
            var expenses = new Queue<int>(Report);
            while (true)
            {
                var e1 = expenses.Dequeue();
                foreach (var e2 in expenses)
                {
                    foreach (var e3 in expenses)
                    {
                        if (e1 + e2 + e3 == 2020) return (e1 * e2 * e3).ToString();
                    }
                }
            }
        }
    }
}
