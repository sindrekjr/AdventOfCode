namespace AdventOfCode.Solutions.Year2021.Day21;

class Solution : SolutionBase
{
    Dictionary<GameState, (long a, long b)> Memo = new Dictionary<GameState, (long, long)>();

    public Solution() : base(21, 2021, "Dirac Dice") { }

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
        var (player1, player2, _) = Input.SplitByNewline();
        var a = int.Parse(player1.Split(":").Last().Trim());
        var b = int.Parse(player2.Split(":").Last().Trim());
        var results = PlayQuantum(new PlayerState(a, 0), new PlayerState(b, 0));

        Console.WriteLine(results.a);
        Console.WriteLine(results.b);

        return "";
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

    (long a, long b) PlayQuantum(PlayerState a, PlayerState b)
    {
        (long a, long b) wins = (0, 0);

        foreach (var roll in GetPossibleRolls())
        {
            var state = new GameState(a, b, 0, roll);
            if (Memo.ContainsKey(state))
            {
                var result = Memo[state];
                wins = (wins.a + result.a, wins.b + result.b);
                continue;
            }

            var aNext = a.NextState(roll);
            if (aNext.Score >= 21)
            {
                wins.a++;
                continue;
            }

            var bNext = b.NextState(roll);
            if (bNext.Score >= 21)
            {
                wins.b++;
                continue;
            }

            var subGameResult = PlayQuantum(aNext, bNext);
            wins.a += subGameResult.a;
            wins.b += subGameResult.b;

            Memo[state] = subGameResult;
        }

        return wins;
    }

    IEnumerable<int> GetPossibleRolls()
    {
        for (int i = 1; i <= 3; i++) for (int j = 1; j <= 3; j++) for (int k = 1; k <= 3; k++)
        {
            yield return i + j + k;
        }
    }
}

internal record PlayerState(int Position, int Score)
{
    public PlayerState NextState(int roll)
    {
        var nextPosition = (Position + roll - 1) % 10 + 1;
        var nextScore = Score + nextPosition;
        return new PlayerState(nextPosition, nextScore);
    }
}

internal record GameState(PlayerState Player1, PlayerState Player2, int Turn, int Roll) { }
