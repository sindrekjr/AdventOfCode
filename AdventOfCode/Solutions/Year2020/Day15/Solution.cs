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
        {
            var start = GetStartingNumbers();
            var (memory, previous) = InitializeMemory(start, 2020);
            for (int i = start.Length; i < 2020; i++)
            {
                var current = memory[previous] == 0 ? 0 : i - memory[previous];
                memory[previous] = i;
                previous = current;
            }

            return previous.ToString();
        }

        protected override string SolvePartTwo()
        {
            var start = GetStartingNumbers();
            var (memory, previous) = InitializeMemory(start);
            for (int i = start.Length; i < 30000000; i++)
            {
                var current = memory[previous] == 0 ? 0 : i - memory[previous];
                memory[previous] = i;
                previous = current;
            }

            return previous.ToString();
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
