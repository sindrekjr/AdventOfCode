using System; 
using System.Linq; 
using System.Collections.Generic; 

namespace AdventOfCode.Solutions.Year2019 {

    class Nanofactory {

        public Dictionary<string, Reaction> Reactions; 
        public Dictionary<string, int> Surplus; 

        public Nanofactory(string[] reactions) {
            Reactions = new Dictionary<string, Reaction>(); 
            Surplus = new Dictionary<string, int>(); 
            foreach(string r in reactions) AddReaction(new Reaction(r)); 
        }

        public int CountRequiredOre(Reaction R, int amount) {
            int count = (Surplus.ContainsKey(R.Output)) ? Surplus[R.Output] : 0; 
            int sum = 0;

            for(; count < amount; count += R.Amount) {
                sum += (from c in R.Input select (c.Key == "ORE") ? c.Value : CountRequiredOre(Reactions[c.Key], c.Value)).Sum();
            }
            Surplus[R.Output] = count - amount; 

            return sum; 
        }
        
        void AddReaction(Reaction R) => Reactions[R.Output] = R; 
    }

    class Reaction {

        public Dictionary<string, int> Input { get; private set; }
        public string Output { get; private set; }
        public int Amount { get; private set; }

        public Reaction(string text) {
            int split = text.IndexOf("=>"); 
            SetInput(text.Substring(0, split).Trim()); 
            SetOutput(text.Substring(split + 2).Trim()); 
        }

        public void SetInput(string input) {
            Input = new Dictionary<string, int>(); 
            foreach(string s in input.Split(", ")) {
                var (amount, chemical, _) = s.Split(" "); 
                Input[chemical] = int.Parse(amount); 
            }
        }

        public void SetOutput(string output) {
            var (amount, chemical, _) = output.Split(" "); 
            Amount = int.Parse(amount); 
            Output = chemical; 
        }
    }
}