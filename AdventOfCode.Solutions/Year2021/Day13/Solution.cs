namespace AdventOfCode.Solutions.Year2021.Day13;

class Solution : SolutionBase
{
    public Solution() : base(13, 2021, "Transparent Origami") { }

    protected override string? SolvePartOne()
    {
        var (dots, instructions) = ParseInput();
        var (axis, param) = instructions.First();
        var paper = dots.Select(dot =>
        {
            var (x, y, _) = dot.Split(",").Select(int.Parse).ToArray();
            return (x, y);
        }).ToHashSet();

        return Fold(paper, axis, param).Count.ToString();
    }

    protected override string? SolvePartTwo()
    {
        var (dots, instructions) = ParseInput();

        var paper = dots.Select(dot =>
        {
            var (x, y, _) = dot.Split(",").Select(int.Parse).ToArray();
            return (x, y);
        }).ToHashSet();

        foreach (var instr in instructions)
        {
            var (axis, param) = instr;
            paper = Fold(paper, axis, param);
        }

        return PaintPaper(paper);
    }

    (string[], (string, int)[]) ParseInput()
    {
        var (dots, instructions, _) = Input.SplitByParagraph().Select(p => p.SplitByNewline()).ToArray();
        return (dots, instructions.Select(instr =>
        {
            var (axis, strVal, _) = instr.Split("along ").ToArray()[1].Split("=");
            return (axis, int.Parse(strVal));
        }).ToArray());
    }

    HashSet<(int x, int y)> Fold(HashSet<(int x, int y)> dots, string axis, int value) => dots.Select(dot =>
    {
        var (x, y) = dot;
        if (axis == "x" && x > value) return (value - (x - value), y);
        if (axis == "y" && y > value) return (x, value - (y - value));
        return (x, y);
    }).ToHashSet();

    string PaintPaper(HashSet<(int x, int y)> paper)
    {
        var str = "\n";
        for (int y = 0; y <= paper.Max(dot => dot.y); y++)
        {
            for (int x = 0; x <= paper.Max(dot => dot.x); x++)
            {
                str += paper.Contains((x, y)) ? "#" : " ";
            }
            str += "\n";
        }
        return str;
    }
}
