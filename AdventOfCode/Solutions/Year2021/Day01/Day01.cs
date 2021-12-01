using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.FSharp.Collections;

namespace AdventOfCode.Solutions.Year2021
{
    class Day01 : ASolution
    {
        readonly FSharpList<int> Measurements;

        public Day01() : base(01, 2021, "Sonar Sweep")
        {
            Measurements = ListModule.OfSeq(Input.ToIntArray("\n"));
        }

        protected override string SolvePartOne() => FSharpDay01
                .CountIncreases(Measurements, 0)
                .ToString();

        protected override string SolvePartTwo() => FSharpDay01
            .CountIncreases(
                FSharpDay01.SumGroupsOfThree(Measurements),
                0).ToString();
    }
}
