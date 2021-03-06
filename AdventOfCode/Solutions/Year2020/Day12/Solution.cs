using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day12 : ASolution
    {
        (int x, int y) StartingPosition = (0, 0);

        public Day12() : base(12, 2020, "Rain Risk") { }

        protected override string SolvePartOne()
            => Utilities.ManhattanDistance(
                StartingPosition,
                Input.SplitByNewline().Aggregate(new Ship(StartingPosition), (ship, action) => ship.DoActionByErroneousAssumptions(action)).Position
            ).ToString();

        protected override string SolvePartTwo()
            => Utilities.ManhattanDistance(
                StartingPosition,
                Input.SplitByNewline().Aggregate(new Ship(StartingPosition, StartingPosition.Add((-1, 10))), (ship, action) => ship.DoAction(action)).Position
            ).ToString();
    }
}
