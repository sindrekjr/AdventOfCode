using System.Linq;

namespace AdventOfCode.Solutions.Year2019.Day01;

class Solution : SolutionBase
{

    public Solution() : base(1, 2019, "The Tyranny of the Rocket Equation")
    {

    }

    protected override string SolvePartOne() => Input.ToIntArray("\n").Select(Fuel).Sum().ToString();

    protected override string SolvePartTwo() => Input.ToIntArray("\n").Select(FuelFuel).Sum().ToString();

    int Fuel(int module) => module / 3 - 2;

    int FuelFuel(int module)
    {
        int fuel = Fuel(module);
        return fuel <= 0 ? 0 : fuel + FuelFuel(fuel);
    }
}
