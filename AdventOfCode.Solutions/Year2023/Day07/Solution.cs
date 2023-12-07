namespace AdventOfCode.Solutions.Year2023.Day07;

class Solution : SolutionBase
{
    public Solution() : base(07, 2023, "") { }

    protected override string SolvePartOne()
    {
        var hands = Input.SplitByNewline().Select(line => new CamelCardHand(line)).Order().ToArray();

        var winnings = 0;
        for (var i = 0; i < hands.Length; i++)
        {
            winnings += (i + 1) * hands[i].Bid;
        }

        return winnings.ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}

internal enum HandType
{
    HighCard,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind,
}

internal class CamelCardHand : IComparable<CamelCardHand>
{
    internal string CardsRaw { get; }
    internal int[] Cards { get; }
    internal int Bid { get; }

    internal CamelCardHand(string input)
    {
        var (cards, bid, _) = input.Split(" ");

        CardsRaw = cards;
        Cards = CardsRaw.Select(card => char.IsNumber(card)
            ? int.Parse(card.ToString())
            : card switch
            {
                'T' => 10,
                'J' => 11,
                'Q' => 12,
                'K' => 13,
                'A' => 14,
                _ => throw new InvalidOperationException()
            }).ToArray();

        Bid = int.Parse(bid);
    }

    internal HandType ParseHandType()
    {
        var (first, rest) = Cards
            .Distinct()
            .Select(card => Cards.Count(c => c == card))
            .OrderByDescending(count => count)
            .ToArray();

        return first switch
        {
            5 => HandType.FiveOfAKind,
            4 => HandType.FourOfAKind,
            3 when rest.FirstOrDefault() == 2 => HandType.FullHouse,
            3 => HandType.ThreeOfAKind,
            2 when rest.FirstOrDefault() == 2 => HandType.TwoPair,
            2 => HandType.OnePair,
            1 => HandType.HighCard,
            _ => throw new InvalidOperationException()
        };
    }

    public int CompareTo(CamelCardHand? other)
    {
        if (other == null) return 1;

        var handTypeCompare = ParseHandType().CompareTo(other.ParseHandType());
        if (handTypeCompare != 0)
        {
            return handTypeCompare;
        }

        for (var i = 0; i < 5; i++)
        {
            var cardCompare = Cards[i].CompareTo(other.Cards[i]);
            if (cardCompare != 0) return cardCompare;
        }

        return 0;
    }
}
