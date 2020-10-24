namespace AdventOfCode.Solutions.Year2019
{

    class Day13 : ASolution
    {

        Arcade Arcade;

        public Day13() : base(13, 2019, "Care Package")
        {
            Arcade = new Arcade(new IntcodeComputer(Input.ToIntArray(",")));
        }

        protected override string SolvePartOne() => Arcade.Initialize().Run().GetTileAmount(2).ToString();
        protected override string SolvePartTwo() => Arcade.Initialize().Run(2).Score.ToString();
    }
}
