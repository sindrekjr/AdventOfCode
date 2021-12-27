namespace AdventOfCode.Solutions.Year2021.Day20;

class Solution : SolutionBase
{
    public Solution() : base(20, 2021, "Trench Map")
    {

    }

    protected override string SolvePartOne()
    {
        var (alg, pixels) = ParseInput();
        return EnhanceImage(pixels, alg, 2).Values.Count(v => v).ToString();
    }

    protected override string SolvePartTwo()
    {
        var (alg, pixels) = ParseInput();
        return EnhanceImage(pixels, alg, 50).Values.Count(v => v).ToString();
    }

    Dictionary<(int x, int y), bool> EnhanceImage(Dictionary<(int x, int y), bool> pixels, string alg, int n) =>
        Enumerable.Range(0, n).Aggregate(pixels, (enhanced, i) => EnhanceImage(enhanced, alg, alg[0] == '#' ? i % 2 != 0 : false));

    Dictionary<(int x, int y), bool> EnhanceImage(Dictionary<(int x, int y), bool> pixels, string alg, bool def)
    {
        pixels = ExpandImage(pixels, def);
        var newPixels = new Dictionary<(int x, int y), bool>();
        foreach (var pos in pixels.Keys)
        {
            var bin = "";
            for (int i = -1; i <= 1; i++) for (int j = -1; j <= 1; j++)
            {
                bin += pixels.GetValueOrDefault(pos.Add((i, j)), def) ? '1' : '0';
            }

            newPixels[pos] = alg[Convert.ToInt32(bin, 2)] == '#';
        }

        return newPixels;
    }

    Dictionary<(int x, int y), bool> ExpandImage(Dictionary<(int x, int y), bool> image, bool def)
    {
        var minX = image.Keys.Min(k => k.y);
        var maxX = image.Keys.Max(k => k.x);
        var minY = image.Keys.Min(k => k.y);
        var maxY = image.Keys.Max(k => k.y);

        for (int x = minX - 1; x <= maxX + 1; x++) for (int y = minY - 1; y <= maxY + 1; y++)
        {
            if (!image.ContainsKey((x, y))) image.Add((x, y), def);
        }

        return image;
    }

    (string algorithm, Dictionary<(int x, int y), bool> pixels) ParseInput()
    {
        var (algorithm, input, _) = Input.SplitByParagraph();
        var image = input.SplitByNewline();

        var pixels = new Dictionary<(int x, int y), bool>();
        for (int x = 0; x < image.Length; x++) for (int y = 0; y < image[x].Length; y++)
        {
            if (image[x][y] == '#') pixels.Add((x, y), image[x][y] == '#');
        }

        return (algorithm, pixels);
    }

    void PaintImage(Dictionary<(int x, int y), bool> image)
    {
        var minX = image.Keys.Min(k => k.y);
        var maxX = image.Keys.Max(k => k.x);
        var minY = image.Keys.Min(k => k.y);
        var maxY = image.Keys.Max(k => k.y);

        var str = "\n";
        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                str += image[(x, y)] ? '#' : '.';
            }
            str += "\n";
        }

        Console.WriteLine(str);
    }
}
