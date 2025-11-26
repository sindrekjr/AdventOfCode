namespace AdventOfCode.Solutions.Year2023.Day07;

class Solution : SolutionBase
{
    public Solution() : base(07, 2023, "Camel Cards") { }

    protected override string? SolvePartOne()
    {
        var hands = Input.SplitByNewline().Select(line => new CamelCardHand(line)).Order().ToArray();

        var winnings = 0;
        for (var i = 0; i < hands.Length; i++)
        {
            winnings += (i + 1) * hands[i].Bid;
        }

        return winnings.ToString();
    }

    protected override string? SolvePartTwo()
    {
        var hands = Input.SplitByNewline().Select(line => new CamelCardHand(line, true)).Order().ToArray();

        var winnings = 0;
        for (var i = 0; i < hands.Length; i++)
        {
            winnings += (i + 1) * hands[i].Bid;
        }

        return winnings.ToString();
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

    internal CamelCardHand(string input, bool joker = false)
    {
        var (cards, bid, _) = input.Split(" ");

        CardsRaw = cards;
        Cards = CardsRaw.Select(card => char.IsNumber(card)
            ? int.Parse(card.ToString())
            : card switch
            {
                'T' => 10,
                'J' => joker ? 1 : 11,
                'Q' => 12,
                'K' => 13,
                'A' => 14,
                _ => throw new InvalidOperationException()
            }).ToArray();

        Bid = int.Parse(bid);
    }

    internal HandType ParseHandType()
    {
        var counted = Cards
            .Distinct()
            .Where(card => card != 1)
            .Select(card => Cards.Count(c => c == card))
            .OrderByDescending(count => count)
            .ToArray();

        if (counted.Length is 0 or 1) return HandType.FiveOfAKind;

        var first = counted[0];
        var second = counted.Length > 1 ? counted[1] : default;

        var type = first switch
        {
            5 => HandType.FiveOfAKind,
            4 => HandType.FourOfAKind,
            3 when second == 2 => HandType.FullHouse,
            3 => HandType.ThreeOfAKind,
            2 when second == 2 => HandType.TwoPair,
            2 => HandType.OnePair,
            1 => HandType.HighCard,

            _ => throw new InvalidOperationException()
        };

        var jokers = Cards.Count(card => card == 1);
        return jokers == 0
            ? type
            : type switch
            {
                HandType.FiveOfAKind when jokers is 5 => HandType.FiveOfAKind,
                HandType.FourOfAKind => HandType.FiveOfAKind,
                HandType.ThreeOfAKind when jokers is 1 => HandType.FourOfAKind,
                HandType.ThreeOfAKind when jokers is 2 => HandType.FiveOfAKind,
                HandType.TwoPair => HandType.FullHouse,
                HandType.OnePair when second is 2 => HandType.FullHouse,
                HandType.OnePair when jokers is 1 => HandType.ThreeOfAKind,
                HandType.OnePair when jokers is 2 => HandType.FourOfAKind,
                HandType.OnePair when jokers is 3 => HandType.FiveOfAKind,
                HandType.HighCard when jokers is 1 => HandType.OnePair,
                HandType.HighCard when jokers is 2 => HandType.ThreeOfAKind,
                HandType.HighCard when jokers is 3 => HandType.FourOfAKind,
                HandType.HighCard when jokers is 4 => HandType.FiveOfAKind,

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
