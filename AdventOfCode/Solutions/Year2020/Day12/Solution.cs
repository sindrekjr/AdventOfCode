using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day12 : ASolution
    {
        Ship ship = new Ship((0, 0));

        public Day12() : base(12, 2020, "Rain Risk")
        {
            
        }

        protected override string SolvePartOne()
        {
            foreach (var action in Input.SplitByNewline()) ship.ParseAction(action);
            return Utilities.ManhattanDistance((0, 0), ship.Position).ToString();
        }

        protected override string SolvePartTwo()
        {
            return null;
        }
    }
}
