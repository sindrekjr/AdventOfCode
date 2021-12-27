using System.Linq;

namespace AdventOfCode.Solutions.Year2021.Day04;

class Solution : SolutionBase
{
    public Solution() : base(04, 2021, "Giant Squid") { }

    protected override string SolvePartOne()
    {
        var (numbers, boards) = ParseInput();
        
        foreach (var num in numbers)
        {
            foreach (var board in boards)
            {
                if (!board.ContainsKey(num)) continue;
                var (x, y, _) = board[num];
                board[num] = (x, y, true);

                var (xAxis, yAxis) = board.Values
                    .Aggregate<(int x, int y, bool marked), (int x, int y)>((0, 0), (acc, current) =>
                    {
                        if (current.marked)
                        {
                            if (current.x == x) acc.x++;
                            if (current.y == y) acc.y++;
                        }

                        return acc;
                    });

                if (xAxis != 5 && yAxis != 5) continue;
                
                var sum = board
                    .Where(field => !field.Value.marked)
                    .Aggregate(0, (acc, field) => field.Key + acc);

                return (num * sum).ToString();
            }
        }

        return null;
    }

    protected override string SolvePartTwo()
    {
        var (numbers, boards) = ParseInput();
        
        foreach (var num in numbers)
        {
            var nextBoards = boards.Select(item => item).ToList();
            
            foreach (var board in boards)
            {
                if (!board.ContainsKey(num)) continue;
                var (x, y, _) = board[num];
                board[num] = (x, y, true);

                var (xAxis, yAxis) = board.Values
                    .Aggregate<(int x, int y, bool marked), (int x, int y)>((0, 0), (acc, current) =>
                    {
                        if (current.marked)
                        {
                            if (current.x == x) acc.x++;
                            if (current.y == y) acc.y++;
                        }

                        return acc;
                    });

                if (xAxis != 5 && yAxis != 5) continue;
                
                if (boards.Count == 1)
                {
                    var sum = board
                        .Where(field => !field.Value.marked)
                        .Aggregate(0, (acc, field) => field.Key + acc);

                    return (num * sum).ToString();
                }
                
                nextBoards.Remove(board);
            }

            boards = nextBoards;
        }
        
        return null;
    }

    (int[], IList<Dictionary<int, (int x, int y, bool marked)>>) ParseInput()
    {
        var (numbers, paragraphs) = Input.SplitByParagraph();
        var enumeratedBoards = paragraphs
            .Select(b => b.SplitByNewline()
                .Select(line => line
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(n => Convert.ToInt32(n))));

        var boards = new List<Dictionary<int, (int, int, bool)>>();
        foreach (var enumeratedBoard in enumeratedBoards)
        {
            var board = new Dictionary<int, (int, int, bool)>();

            var x = 0;
            foreach (var line in enumeratedBoard)
            {
                var y = 0;
                foreach (var num in line)
                {
                    board.Add(num, (x, y, false));
                    y++;
                }

                x++;
            }
            
            boards.Add(board);
        }

        return (numbers.ToIntArray(","), boards);
    }
}
