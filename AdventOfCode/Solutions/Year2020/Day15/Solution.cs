using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day15 : ASolution
    {

        public Day15() : base(15, 2020, "Rambunctious Recitation") { }

        protected override string SolvePartOne()
            => PredictSpokenNumber(GetStartingNumbers(), 2020).ToString();

        protected override string SolvePartTwo()
            => PredictSpokenNumber(GetStartingNumbers(), 30000000).ToString();

        int PredictSpokenNumber(int[] startingNumbers, int n)
        {
            var (memory, previous) = InitializeMemory(startingNumbers);
            for (int i = startingNumbers.Length; i < n; i++)
            {
                var current = memory[previous] == 0 ? 0 : i - memory[previous];
                memory[previous] = i;
                previous = current;
            }
            return previous;
        }

        int[] GetStartingNumbers(int debugIndex = -1)
            => debugIndex == -1 ? Input.ToIntArray(",") : Input.SplitByNewline().Select(l => l.ToIntArray(",")).ToArray()[debugIndex];

        (int[] memory, int last) InitializeMemory(int[] start, int capacity = 30000000)
        {
            var memory = new int[capacity];
            var last = 0;
            for (int i = 1; i <= start.Length; i++)
            {
                last = start[i - 1];
                memory[last] = i;
            }

            return (memory, last);
        }
    }
}
