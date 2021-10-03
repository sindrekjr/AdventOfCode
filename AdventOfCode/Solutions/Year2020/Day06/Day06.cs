using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day06 : ASolution
    {

        public Day06() : base(06, 2020, "Custom Customs") { }

        protected override string SolvePartOne()
            => Input.Split("\n\n").Aggregate(0, (count, group) => count + group.Replace("\n", "").ToHashSet().Count).ToString();

        protected override string SolvePartTwo()
            => Input.Split("\n\n").Aggregate(0, (count, group) => count + group.SplitByNewline().IntersectAll().Count()).ToString();
    }
}
