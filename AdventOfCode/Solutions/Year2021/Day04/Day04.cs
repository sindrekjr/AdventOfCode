using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2021
{
    class Day04 : ASolution
    {

        public Day04() : base(04, 2021, "Giant Squid") { }

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

                    var markedX = 0;
                    var markedY = 0;

                    foreach (var values in board.Values)
                    {
                        if (!values.marked) continue;
                        
                        if (values.x == x) markedX++;
                        if (values.y == y) markedY++;
                        if (markedX == 5 || markedY == 5) break;
                    }

                    if (markedX == 5 || markedY == 5)
                    {
                        var sum = board
                            .Where(field => !field.Value.marked)
                            .Aggregate(0, (acc, field) => field.Key + acc);

                        return (num * sum).ToString();
                    }
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

                    var markedX = 0;
                    var markedY = 0;

                    foreach (var values in board.Values)
                    {
                        if (!values.marked) continue;
                        
                        if (values.x == x) markedX++;
                        if (values.y == y) markedY++;
                        if (markedX == 5 || markedY == 5) break;
                    }

                    if (markedX == 5 || markedY == 5)
                    {
                        if (boards.Count == 1)
                        {
                            var sum = board
                                .Where(field => !field.Value.marked)
                                .Aggregate(0, (acc, field) => field.Key + acc);

                            return (num * sum).ToString();
                        }
                        
                        nextBoards.Remove(board);
                    }
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
}
