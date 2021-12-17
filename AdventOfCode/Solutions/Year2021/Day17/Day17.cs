namespace AdventOfCode.Solutions.Year2021
{
    class Day17 : ASolution
    {
        int MinX;
        int MaxX;
        int MinY;
        int MaxY;

        public Day17() : base(17, 2021, "Trick Shot")
        {
            var (x, y, _) = Input.Split(": ")[1].Split(", ").Select(coor => coor.Substring(2).Split("..").ToIntArray()).ToArray();
            MinX = x.Min();
            MaxX = x.Max();
            MinY = y.Min();
            MaxY = y.Max();
        }

        protected override string SolvePartOne()
        {
            var actuallyHighest = 0;
            for (int y = 5; y < 2000; y++)
            {
                var (success, highest) = DryRun((0, 0), (0, y));
                if (success) actuallyHighest = highest;
            }

            return actuallyHighest.ToString();
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

        (bool success, int maxHeight) DryRun((int x, int y) pos, (int x, int y) vel)
        {
            var highest = 0;
            while (pos.y >= MinY)
            {
                (pos, vel) = Step(pos, vel);

                if (pos.y > highest) highest = pos.y;

                if (pos.y >= MinY && pos.y <= MaxY) return (true, highest);
            }

            return (false, highest);
        }

        ((int x, int y) pos, (int x, int y) velocity) Step((int x, int y) pos, (int x, int y) vel)
        {
            pos.x += vel.x;
            pos.y += vel.y;

            if (vel.x != 0) vel.x -= vel.x > 0 ? 1 : -1;
            vel.y--;

            return (pos, vel);
        }
    }
}
