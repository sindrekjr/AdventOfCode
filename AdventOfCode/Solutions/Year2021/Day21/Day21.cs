namespace AdventOfCode.Solutions.Year2021
{
    class Day21 : ASolution
    {
        public Day21() : base(21, 2021, "Dirac Dice") { }

        protected override string SolvePartOne()
        {
            var (player1, player2, _) = Input.SplitByNewline();
            var a = int.Parse(player1.Split(":").Last().Trim());
            var b = int.Parse(player2.Split(":").Last().Trim());
            var (rolls, scores) = PlayDeterministic(a, b);
            return (--rolls * scores.Min()).ToString();
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

        (int rolls, int[] scores) PlayDeterministic(int aPos, int bPos)
        {
            var deterministic = 1;
            var aScore = 0;
            var bScore = 0;
            var rolls = 0;

            while (aScore <= 1000 && bScore <= 1000)
            {
                rolls += 3;
                var roll = deterministic++ + deterministic++ + deterministic++;
                aPos = (aPos + roll) % 10;
                if (aPos == 0) aPos = 10;
                aScore += aPos;

                if (aScore >= 1000) break;

                rolls += 3;
                roll = deterministic++ + deterministic++ + deterministic++;
                bPos = (bPos + roll) % 10;
                if (bPos == 0) bPos = 10;
                bScore += bPos;
            }

            return (deterministic, new[] { aScore, bScore });
        }
    }
}
