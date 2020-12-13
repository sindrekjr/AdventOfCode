using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day13 : ASolution
    {

        public Day13() : base(13, 2020, "Shuttle Search")
        {

        }

        protected override string SolvePartOne()
        {
            var (arrival, buses) = ParseInput();
            (int offset, int bus) = buses.Aggregate<string, (int offset, int bus)>((-1, 0), (best, bus) => 
            {
                if (bus != "x")
                {
                    var depart = 0;
                    var busId = int.Parse(bus);
                    while (depart < arrival) depart += busId;
                    var offset = depart - arrival;
                    
                    if (offset < best.offset || best.bus == 0)
                    {
                        return (offset, busId);
                    }
                }

                return best;
            });

            return (offset * bus).ToString();

            // foreach (var bus in buses)
            // {
            //     if (bus != "x")
            //     {
            //         var depart = 0;
            //         while (depart < arrival) depart += int.Parse(bus);
            //         var offset = depart - arrival;
            //         if (offset < best.offset || best.offset == -1)
            //         {
            //             best = (offset, bus);
            //         }
            //     }
            // }
            // return (int.Parse(best.bus) * best.offset).ToString();
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

        (int arrival, string[] buses) ParseInput()
        {
            var (arrivalEstimate, buses, _) = Input.SplitByNewline();
            return (int.Parse(arrivalEstimate), buses.Split(","));
        }
    }
}
