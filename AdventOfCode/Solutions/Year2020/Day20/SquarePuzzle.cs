using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    internal class SquarePuzzle : SquareMap<SquareTile>
    {
        public int Bounds { get; set; }
        public Queue<(int x, int y)> Frontier { get; set; } = new Queue<(int x, int y)>();

        public SquarePuzzle(int bounds) : base()
        {
            Bounds = bounds;
            Frontier = new Queue<(int, int)>();
        }

        public bool Fit((int x, int y) pos, SquareTile tile)
        {
            if (ContainsKey(pos)) return false;

            var ideal = GetNeighbouringSides(pos);
            for (int i = 0; i < 8; i++)
            {
                if (tile.FullMatch(ideal))
                {
                    Add(pos, tile);
                    UpdateFrontier(pos);

                    // Console.WriteLine("Placed Tile " + pos + " " + tile.Id);
                    // Console.WriteLine(tile.ToString());

                    return true;
                }

                tile.Orientation++;
            }
            
            Frontier.Enqueue(pos);
            return false;
        }

        public (string top, string right, string bottom, string left) GetNeighbouringSides((int x, int y) pos)
            => (
                pos.x > 0 && TryGetValue(pos.Add((-1, 0)), out SquareTile top) ? top.Bottom : null,
                pos.y <= Bounds && TryGetValue(pos.Add((0, 1)), out SquareTile right) ? right.Left : null,
                pos.x <= Bounds && TryGetValue(pos.Add((1, 0)), out SquareTile bottom) ? bottom.Top : null,
                pos.y > 0 && TryGetValue(pos.Add((0, -1)), out SquareTile left) ? left.Right : null
            );

        public override string ToString()
        {
            var str = "";
            for (int i = 0; i <= Bounds; i++)
            {
                for (int j = 0; j <= this.FirstOrDefault().Value.Bounds; j++)
                {
                    for (int k = 0; k <= Bounds; k++)
                    {
                        if (TryGetValue((i, k), out SquareTile tile))
                        {
                            for (int l = 0; l <= this.FirstOrDefault().Value.Bounds; l++) str += tile[j, l];
                        }
                        else
                        {
                            str += new string(' ', this.FirstOrDefault().Value.Bounds + 1);
                        }
                        str += " ";
                    }
                    str += "\n";
                }
                str += "\n";
            }
            return str;
        }

        void UpdateFrontier((int x, int y) pos)
        {
            for (int i = -1; i <= 1; i++) for (int j = -1; j <= 1; j++)
            {
                if ((i == 0 && j == 0) || (i != 0 && j != 0)) continue;
                var (x, y) = pos.Add((i, j));
                if (!ContainsKey((x, y)) && x >= 0 && x <= Bounds && y >= 0 && y <= Bounds) Frontier.Enqueue((x, y));
            }
        }
    }

    internal class SquareTile : SquareMap<char>
    {
        public int Id { get; set; }
        public int Orientation { get; set; } = 0;
        public int Bounds { get; }

        public string Top => Enumerable.Range(0, Bounds + 1).Aggregate("", (str, col) => str + this[0, col]);
        public string Right => Enumerable.Range(0, Bounds + 1).Aggregate("", (str, row) => str + this[row, Bounds]);
        public string Bottom => Enumerable.Range(0, Bounds + 1).Aggregate("", (str, col) => str + this[Bounds, col]);
        public string Left => Enumerable.Range(0, Bounds + 1).Aggregate("", (str, row) => str + this[row, 0]);

        public List<SquareTile> TopMatches { get; set; } = new List<SquareTile>();
        public List<SquareTile> RightMatches { get; set; } = new List<SquareTile>();
        public List<SquareTile> BottomMatches { get; set; } = new List<SquareTile>();
        public List<SquareTile> LeftMatches { get; set; } = new List<SquareTile>();

        public char this[int row, int col]
        {
            get
            {
                for (int i = 0; i < Orientation % 4; i++) (row, col) = (col, Bounds - row);
                if (Orientation % 8 >= 4) col = Bounds - col;
                return this[(row,col)];
            }
        }

        public SquareTile(IEnumerable<string> img) : base(img.Select(str => str.ToCharArray()).ToArray()) => Bounds = img.Count() - 1;

        public int CountMatchingSides()
        {
            int count = 0;
            if (TopMatches.Any()) count++;
            if (RightMatches.Any()) count++;
            if (BottomMatches.Any()) count++;
            if (LeftMatches.Any()) count++;
            return count;
        }

        public bool AnyMatch(string side)
            => new string[]
            {
                Top, Top.Reverse(),
                Right, Right.Reverse(),
                Bottom, Bottom.Reverse(),
                Left, Left.Reverse(),
            }.Contains(side);

        public bool PossibleFullMatch((string t, string r, string b, string l) sides)
            => (sides.t == null || AnyMatch(sides.t))
            && (sides.r == null || AnyMatch(sides.r))
            && (sides.b == null || AnyMatch(sides.b))
            && (sides.l == null || AnyMatch(sides.l));

        public bool FullMatch((string t, string r, string b, string l) sides)
            => (sides.t == null || Top == sides.t)
            && (sides.r == null || Right == sides.r)
            && (sides.b == null || Bottom == sides.b)
            && (sides.l == null || Left == sides.l);

        public override string ToString()
        {
            var str = "";
            for (int x = 0; x <= Bounds; x++)
            {
                for (int y = 0; y <= Bounds; y++) str += this[x, y];
                str += "\n";
            }
            return str;
        }
    }
}