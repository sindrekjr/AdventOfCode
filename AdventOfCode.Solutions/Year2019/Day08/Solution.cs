namespace AdventOfCode.Solutions.Year2019.Day08;

class Solution : SolutionBase
{

    public Solution() : base(8, 2019, "Space Image Format")
    {

    }

    protected override string? SolvePartOne()
    {
        var image = Input.ToIntArray().SplitIntoChunks(25).SplitIntoChunks(6);
        int fewest = 0, result = 0;
        foreach(var layer in image)
        {
            int digit0 = 0, digit1 = 0, digit2 = 0;
            foreach(var row in layer)
            {
                foreach(int pixel in row)
                {
                    if(pixel == 0) digit0++;
                    else if(pixel == 1) digit1++;
                    else if(pixel == 2) digit2++;
                }
            }
            if(digit0 < fewest || fewest == 0)
            {
                fewest = digit0;
                result = digit1 * digit2;
            }
        }
        return result.ToString();
    }

    protected override string? SolvePartTwo()
    {
        var image = Input.ToIntArray().SplitIntoChunks(25).SplitIntoChunks(6);
        int?[,] display = new int?[6, 25];
        foreach(var layer in image)
        {
            int r = 0;
            foreach(var row in layer)
            {
                int p = 0;
                foreach(int pixel in row)
                {
                    if(pixel != 2)
                    {
                        display[r, p] = display[r, p] ?? pixel;
                    }
                    p++;
                }
                r++;
            }
        }
        //var actualImage = new Bitmap(6, 25);
        string result = "\n";
        for(int i = 0; i < display.GetLength(0); i++)
        {
            for(int j = 0; j < display.GetLength(1); j++)
            {
                result += (display[i, j] == 0) ? " " : 0.ToString();
                //actualImage.SetPixel(i, j, (display[i,j] == 0) ? Color.Black : Color.White); 
            }
            result += "\n";
        }
        //actualImage.Save("SpaceImage.png", ImageFormat.Png); 
        return result;
    }
}
