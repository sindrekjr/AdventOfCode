using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.FSharp.Collections;

namespace AdventOfCode.Solutions.Year2021
{
    class Day01 : ASolution
    {
        public Day01() : base(01, 2021, "Sonar Sweep") { }

        protected override string SolvePartOne()
            => FSharpDay01
                .CountIncreases(ListModule.OfSeq(Input.ToIntArray("\n")), 0)
                .ToString();

        protected override string SolvePartTwo()
        {
            var measurements = Input.ToIntArray("\n");

            var sums = measurements.Select((m, i) => {
                if (measurements.Length <= i + 2) return 0;

                return measurements[i] + measurements[i + 1] + measurements[i + 2];
            }).ToArray();

            var increases = 0;
            for (int i = 0; i < sums.Length; i++) {
                if (i == 0) continue;

                if (sums[i] > sums[i - 1]) increases++;
            }

            return increases.ToString();
        }
    }
}
