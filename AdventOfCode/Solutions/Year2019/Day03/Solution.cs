using System;
using System.Collections.Generic;
using static AdventOfCode.Solutions.Utilities;

namespace AdventOfCode.Solutions.Year2019
{

    class Day03 : ASolution
    {

        public Day03() : base(3, 2019, "Crossed Wires")
        {

        }

        protected override string SolvePartOne()
        {
            string[] wires = Input.SplitByNewline();
            List<(int x, int y)> path1 = Path(wires[0]);
            List<(int x, int y)> path2 = Path(wires[1]);

            int closest = 0;
            foreach((int, int) pos in path1)
            {
                if(path2.Contains(pos))
                {
                    int distance = ManhattanDistance((0, 0), pos);
                    if(closest > distance || closest == 0)
                    {
                        closest = distance;
                    }
                }
            }
            return closest.ToString();
        }

        protected override string SolvePartTwo()
        {
            string[] wires = Input.SplitByNewline();
            List<(int x, int y)> path1 = Path(wires[0]);
            List<(int x, int y)> path2 = Path(wires[1]);

            int closest = 0;
            for(int i = 0; i < path1.Count; i++)
            {
                (int, int) pos = path1[i];
                if(path2.Contains(pos))
                {
                    int steps = 2 + i + path2.FindIndex(0, (element) => element == pos);
                    if(closest > steps || closest == 0)
                    {
                        closest = steps;
                    }
                }
            }
            return closest.ToString();
        }

        List<(int, int)> Path(string instructions)
        {
            var path = new List<(int x, int y)>();
            (int x, int y) pos = (0, 0);

            foreach(string i in instructions.Split(","))
            {
                switch(i[0])
                {
                    case 'R':
                        new Action(() =>
                        {
                            pos.x++;
                            path.Add(pos);
                        }).Repeat(int.Parse(i.Substring(1)));
                        break;
                    case 'L':
                        new Action(() =>
                        {
                            pos.x--;
                            path.Add(pos);
                        }).Repeat(int.Parse(i.Substring(1)));
                        break;
                    case 'U':
                        new Action(() =>
                        {
                            pos.y++;
                            path.Add(pos);
                        }).Repeat(int.Parse(i.Substring(1)));
                        break;
                    case 'D':
                        new Action(() =>
                        {
                            pos.y--;
                            path.Add(pos);
                        }).Repeat(int.Parse(i.Substring(1)));
                        break;
                    default:
                        break;
                }
            }
            return path;
        }
    }
}
