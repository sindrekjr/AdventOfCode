using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{
    class Day22 : ASolution
    {
        public Day22() : base(22, 2020, "Crab Combat") { }

        protected override string SolvePartOne()
        {
            var (Queue1, Queue2) = GetDecks();

            while (Queue1.Count > 0 && Queue2.Count > 0)
            {
                var (p1, p2) = (Queue1.Dequeue(), Queue2.Dequeue());
                if (p1 > p2)
                {
                    Queue1.Enqueue(p1);
                    Queue1.Enqueue(p2);
                }
                else
                {
                    Queue2.Enqueue(p2);
                    Queue2.Enqueue(p1);
                }
            }

            var score = 0;
            var Winner = Queue1.Count > 0 ? Queue1 : Queue2;
            while (Winner.Count > 0) score += Winner.Count * Winner.Dequeue();
            
            return score.ToString();
        }

        protected override string SolvePartTwo()
        {
            var (Queue1, Queue2) = GetDecks();
            var (score1, score2) = RecursiveCombat(Queue1, Queue2);
            return score1 > 0 ? score1.ToString() : score2.ToString();
        }

        (int, int) RecursiveCombat(Queue<int> deck1, Queue<int> deck2)
        {
            var stop = false;
            var history = new HashSet<(string, string)>();
            while (deck1.Count > 0 && deck2.Count > 0)
            {
                
                if (history.Contains((deck1.JoinAsStrings(","), deck2.JoinAsStrings(","))))
                {
                    stop = true;
                    break;
                }
                else
                {
                    history.Add((deck1.JoinAsStrings(","), deck2.JoinAsStrings(",")));
                }

                var (p1, p2) = (deck1.Dequeue(), deck2.Dequeue());

                if (deck1.Count >= p1 && deck2.Count >= p2)
                {
                    var (s1, s2) = RecursiveCombat(new Queue<int>(deck1.Take(p1)), new Queue<int>(deck2.Take(p2)));

                    if (s1 > s2)
                    {
                        deck1.Enqueue(p1);
                        deck1.Enqueue(p2);
                    }
                    else
                    {
                        deck2.Enqueue(p2);
                        deck2.Enqueue(p1);
                    }
                }
                else if (p1 > p2)
                {
                    deck1.Enqueue(p1);
                    deck1.Enqueue(p2);
                }
                else
                {
                    deck2.Enqueue(p2);
                    deck2.Enqueue(p1);
                }
            }

            var score1 = 0;
            while (deck1.Count > 0) score1 += deck1.Count * deck1.Dequeue();

            var score2 = 0;
            if (!stop) while (deck2.Count > 0) score2 += deck2.Count * deck2.Dequeue();

            return (score1, score2);
        }

        (Queue<int>, Queue<int>) GetDecks()
        {
            var input = Input.SplitByParagraph();
            var deck1 = input[0].SplitByNewline();
            var deck2 = input[1].SplitByNewline();

            var Queue1 = new Queue<int>();
            var Queue2 = new Queue<int>();

            for (int i = 1; i < deck1.Length; i++)
            {
                Queue1.Enqueue(int.Parse(deck1[i]));
                Queue2.Enqueue(int.Parse(deck2[i]));
            }

            return (Queue1, Queue2);
        }
    }
}
