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
            return null;
        }
    }
}
