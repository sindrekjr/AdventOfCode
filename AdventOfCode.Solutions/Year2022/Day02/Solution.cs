namespace AdventOfCode.Solutions.Year2022.Day02;

class Solution : SolutionBase
{
    public Solution() : base(02, 2022, "Rock Paper Scissors") { }

    protected override string SolvePartOne()
    {
        var input = Input.SplitByNewline();
        var rounds = input.Select(round =>
        {
            var (a, b, _) = round.Split(" ");
            var points = b switch
            {
                "X" => 1,
                "Y" => 2,
                "Z" => 3
            };

            if (a == "A" && b == "Y") points += 6;
            if (a == "B" && b == "Z") points += 6;
            if (a == "C" && b == "X") points += 6;
            if (a == "A" && b == "X") points += 3;
            if (a == "B" && b == "Y") points += 3;
            if (a == "C" && b == "Z") points += 3;

            return points;
        });
        return rounds.Sum().ToString();
    }

    protected override string SolvePartTwo()
    {
        var input = Input.SplitByNewline();
        var rounds = input.Select(round =>
        {
            var (a, b, _) = round.Split(" ");

            var points = b switch
            {
                "X" => 0,
                "Y" => 3,
                "Z" => 6
            };

            if (b == "X")
            {
                points += a switch
                {
                    "A" => 3,
                    "B" => 1,
                    "C" => 2
                };
            }

            if (b == "Y")
            {
                points += a switch
                {
                    "A" => 1,
                    "B" => 2,
                    "C" => 3
                };
            }

            if (b == "Z")
            {
                points += a switch
                {
                    "A" => 2,
                    "B" => 3,
                    "C" => 1
                };
            }

            return points;
        });
        return rounds.Sum().ToString();
    }
}
