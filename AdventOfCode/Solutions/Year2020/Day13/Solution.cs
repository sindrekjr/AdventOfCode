using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day13 : ASolution
    {

        public Day13() : base(13, 2020, "Shuttle Search") { }

        protected override string SolvePartOne()
        {
            var (arrival, buses) = ParseInput();
            (int offset, int bus) = buses.Aggregate<int, (int offset, int bus)>((-1, 0), (best, bus) =>
            {
                if (bus != -1)
                {
                    var depart = 0;
                    var busId = bus;
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
        }

        protected override string SolvePartTwo()
        {
            return null;
            // var (_, buses) = ParseInput();
            // for (long i = buses[0]; ; i += buses[0])
            // {
            //     var found = true;
            //     for (int j = 1; j < buses.Length; j++)
            //     {
            //         if (buses[j] != -1 && (i + j) % buses[j] != 0)
            //         {
            //             found = false;
            //             break;
            //         }
            //     }

            //     if (found) return i.ToString();
            // }
        }

        (int arrival, int[] buses) ParseInput()
        {
            var (arrivalEstimate, buses, _) = Input.SplitByNewline();
            return (int.Parse(arrivalEstimate), buses.Split(",").Select(bus => bus == "x" ? -1 : int.Parse(bus)).ToArray());
        }
    }
}
