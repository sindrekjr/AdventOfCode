using System.Collections.Generic; 

namespace AdventOfCode.Solutions.Year2019 {

    class Day14 : ASolution {

        Nanofactory Factory; 

        public Day14() : base(14, 2019, "Space Stoichiometry") {
            Factory = new Nanofactory(Input.SplitByNewline()); 
        }

        protected override string SolvePartOne() => Factory.CountRequiredOre(Factory.Reactions["FUEL"], 1).ToString(); 

        protected override string SolvePartTwo() {
            return null;
        }
    }
}
