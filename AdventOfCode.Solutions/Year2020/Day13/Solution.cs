using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020.Day13;

class Solution : SolutionBase
{

    public Solution() : base(13, 2020, "Shuttle Search") { }

    protected override string? SolvePartOne()
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

    protected override string? SolvePartTwo()
    {
        var (_, buses) = ParseInput();
        for (long i = buses[0], j = 1, mod = i;; i += mod)
        {
            while (buses[j] == -1) j++;
            if ((i + j) % buses[j] == 0)
            {
                mod *= buses[j];
                if (++j == buses.Length) return i.ToString();
            }
        }
    }

    (int arrival, int[] buses) ParseInput(int timeTableIndex = 0)
    {
        var (arrivalEstimate, buses) = Input.SplitByNewline();
        return (int.Parse(arrivalEstimate), buses[timeTableIndex].Split(",").Select(bus => bus == "x" ? -1 : int.Parse(bus)).ToArray());
    }
}
