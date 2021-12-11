
using AdventOfCode.Solutions.Year2020;

namespace AdventOfCode.Solutions.Year2021
{

    class Day11 : ASolution
    {

        public Day11() : base(11, 2021, "")
        {

        }

        protected override string SolvePartOne()
        {
            var octopuses = new Map<int>(Input.SplitByNewline().Select(line => line.ToIntArray()).ToArray());

            var flashes = 0;
            for (int i = 1; i <= 100; i++)
            {
                var positions = octopuses.Keys.ToArray();

                foreach (var pos in positions) octopuses[pos]++;

                var primed = new HashSet<(int x, int y)>();
                while (octopuses.Any(o => o.Value > 9 && !primed.Contains(o.Key)))
                {
                    var (pos, _) = octopuses.First(o => o.Value > 9 && !primed.Contains(o.Key));
                    foreach (var adj in FindAdjacentOctopuses(octopuses, pos)) octopuses[adj]++;
                    primed.Add(pos);
                }

                foreach (var pos in positions)
                {
                    var energy = octopuses[pos];
                    if (energy > 9)
                    {
                        flashes++;
                        octopuses[pos] = 0;
                    }
                }

                //if (i % 1 == 0)
                //{
                //    PaintOctopuses(octopuses);
                //    Console.WriteLine($"{i}: {flashes} flashes");
                //    Console.WriteLine();
                //}
            }

            return flashes.ToString();
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

        IEnumerable<(int x, int y)> FindAdjacentOctopuses(Map<int> octopuses, (int x, int y) position)
        {
            for (int x = -1; x <= 1; x++) for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;
                var (nX, nY) = position.Add((x, y));
                if (octopuses.ContainsKey((nX, nY))) yield return (nX, nY);
            }
        }

        void IncreaseAdjacentOctopuses(Map<int> octopuses, HashSet<(int x, int y)> primed, (int x, int y) position)
        {
            primed.Add(position);
            for (int x = -1; x <= 1; x++) for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;
                var adjPos = position.Add((x, y));
                if (octopuses.ContainsKey(adjPos) && !primed.Contains(adjPos)) {
                    primed.Add(adjPos);
                    octopuses[adjPos]++;
                    if (octopuses[adjPos] > 9) IncreaseAdjacentOctopuses(octopuses, primed, adjPos);
                }
            }
        }

        void PaintOctopuses(Map<int> octopuses)
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    var energy = octopuses[(x, y)];
                    if (energy == 0) Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(energy);
                    Console.ResetColor();
                }

                Console.WriteLine();
            }
        }
    }
}
