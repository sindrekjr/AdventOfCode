using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Year2019
{

    class Nanofactory
    {

        long Ore;
        Dictionary<string, int> Surplus;

        public Dictionary<string, Reaction> Reactions;

        public Nanofactory(string[] reactions)
        {
            Reactions = new Dictionary<string, Reaction>();
            foreach(string r in reactions) AddReaction(new Reaction(r));
        }

        public Nanofactory Initialize()
        {
            Ore = 0;
            Surplus = new Dictionary<string, int>();
            return this;
        }

        public int CountRequiredOre(Reaction R, int amount)
        {
            int count = (Surplus.ContainsKey(R.Output)) ? Surplus[R.Output] : 0;
            int sum = 0;

            for(; count < amount; count += R.Amount)
            {
                sum += (from c in R.Input select (c.Key == "ORE") ? c.Value : CountRequiredOre(Reactions[c.Key], c.Value)).Sum();
            }
            Surplus[R.Output] = count - amount;

            return sum;
        }

        public long CollectOre(long amount) => Ore += amount;

        public int ProduceMaxFuel()
        {
            int initialCost = CountRequiredOre(Reactions["FUEL"], 1);
            int fuel = (int)(Ore / initialCost);

            foreach(string c in Reactions.Keys)
            {
                Surplus[c] *= fuel;
            }
            while(CountRequiredOre(Reactions["FUEL"], 1) == 0) fuel++;

            return fuel;
        }

        void AddReaction(Reaction R) => Reactions[R.Output] = R;
    }

    class Reaction
    {

        public Dictionary<string, int> Input { get; private set; }
        public string Output { get; private set; }
        public int Amount { get; private set; }

        public Reaction(string text)
        {
            int split = text.IndexOf("=>");
            SetInput(text.Substring(0, split).Trim());
            SetOutput(text.Substring(split + 2).Trim());
        }

        public void SetInput(string input)
        {
            Input = new Dictionary<string, int>();
            foreach(string s in input.Split(", "))
            {
                var (amount, chemical, _) = s.Split(" ");
                Input[chemical] = int.Parse(amount);
            }
        }

        public void SetOutput(string output)
        {
            var (amount, chemical, _) = output.Split(" ");
            Amount = int.Parse(amount);
            Output = chemical;
        }
    }
}