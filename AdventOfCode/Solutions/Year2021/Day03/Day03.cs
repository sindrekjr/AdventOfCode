using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2021
{

    class Day03 : ASolution
    {
        public Day03() : base(03, 2021, "Binary Diagnostic") { }

        protected override string SolvePartOne()
        {
            var counter = CountBits(Input.SplitByNewline());
            var gamma = counter.Aggregate("", (acc, n) => acc + (n >= 0 ? "1" : "0"));
            var epsilon = counter.Aggregate("", (acc, n) => acc + (n < 0 ? "1" : "0"));
            return (Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2)).ToString();
        }

        protected override string SolvePartTwo()
        {
            var report = Input.SplitByNewline();
            var oxygenGenerator = report.ToList();
            var co2Scrubber = report.ToList();
            for (var i = 0; i < report[0].Length; i++)
            {
                if (oxygenGenerator.Count > 1)
                {
                    var oxygenCounter = CountBits(oxygenGenerator);
                    var commonOxygenBit = oxygenCounter[i] >= 0 ? '1' : '0';
                    oxygenGenerator = oxygenGenerator.Where(num => num[i] == commonOxygenBit).ToList();
                }

                if (co2Scrubber.Count > 1)
                {
                    var co2ScrubberCounter = CountBits(co2Scrubber);
                    var commonCo2ScrubberBit = co2ScrubberCounter[i] >= 0 ? '1' : '0';
                    co2Scrubber = co2Scrubber.Where(num => num[i] != commonCo2ScrubberBit).ToList();
                    
                }
            }
            
            return (Convert.ToInt32(oxygenGenerator[0], 2) * Convert.ToInt32(co2Scrubber[0], 2)).ToString();
        }

        int[] CountBits(IList<string> report)
        {
            var counter = new int[report.First().Length];
            
            foreach (var number in report)
            {
                for (int i = 0; i < number.Length; i++)
                {
                    if (number[i] == '1') counter[i]++;
                    if (number[i] == '0') counter[i]--;
                }
            }

            return counter;
        }
    }
}
