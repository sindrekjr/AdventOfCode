namespace AdventOfCode.Solutions.Year2019.Day10;

class Solution : SolutionBase
{

    int size;
    bool[,] Map;
    (int x, int y) Station;
    SortedDictionary<double, (int x, int y)> Asteroids = new();

    public Solution() : base(10, 2019, "Monitoring Station")
    {
        var lines = Input.SplitByNewline();

        size = lines.Length;
        Map = new bool[size, size];
        for(int x = 0; x < size; x++)
        {
            for(int y = 0; y < size; y++)
            {
                Map[x, y] = lines[y][x] == '#';
            }
        }
    }

    protected override string SolvePartOne() => DeployStation().ToString();

    protected override string SolvePartTwo()
    {
        (int x, int y) = FindNthAsteroidToBeVaporized(200);
        return (x * 100 + y).ToString();
    }

    int DeployStation()
    {
        int best = 0;
        for(int x = 0; x < size; x++)
        {
            for(int y = 0; y < size; y++)
            {
                if(Map[x, y])
                {
                    var visible = FindVisibleAsteroids((x, y));
                    int count = visible.Count;
                    if(count > best)
                    {
                        best = count;
                        Station = (x, y);
                        Asteroids = visible;
                    }
                }
            }
        }
        return best;
    }

    SortedDictionary<double, (int x, int y)> FindVisibleAsteroids((int x, int y) asteroid)
    {
        var asteroids = new SortedDictionary<double, (int x, int y)>();
        for(int x = 0; x < size; x++)
        {
            for(int y = 0; y < size; y++)
            {
                if(!Map[x, y] || (x, y) == asteroid) continue;

                int v = asteroid.x - x;
                int h = asteroid.y - y;
                if(v == 0 || h == 0)
                {
                    v = v < 0 ? -1 : v > 0 ? 1 : 0;
                    h = h < 0 ? -1 : h > 0 ? 1 : 0;
                }
                else
                {
                    int GCD = (int)FindGCD(Math.Abs(v), Math.Abs(h));
                    if(GCD > 1)
                    {
                        v = v / GCD;
                        h = h / GCD;
                    }
                }

                double angle = Math.Atan2(v, h);
                if(asteroids.ContainsKey(angle))
                {
                    if(ManhattanDistance(asteroid, (x, y)) < ManhattanDistance(asteroid, asteroids[angle]))
                    {
                        asteroids[angle] = (x, y);
                    }
                }
                else
                {
                    asteroids[angle] = (x, y);
                }
            }
        }
        return asteroids;
    }

    (int x, int y) FindNthAsteroidToBeVaporized(int n)
    {
        while(true)
        {
            if(Asteroids == null)
            {
                DeployStation();
            }
            else if(Asteroids.Count == 0)
            {
                FindVisibleAsteroids(Station);
            }

            if (Asteroids != null)
            {
                var keys = Asteroids.Keys.ToList();
                foreach(double i in keys)
                {
                    if(i >= 0)
                    {
                        return Asteroids[keys[keys.Count - (n - keys.IndexOf(i)) + 1]];
                    }
                }
            }
        }
    }
}
