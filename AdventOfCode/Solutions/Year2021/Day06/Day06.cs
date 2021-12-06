using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    class Day06 : ASolution
    {
        public Day06() : base(06, 2021, "Lanternfish", true) { }

        protected override string SolvePartOne() =>
            CountFishForDays(Input.ToIntArray(",").ToList(), 80).Count.ToString();

        protected override string SolvePartTwo()
        {
            return SimulateFishForDays(Input.ToIntArray(",").ToList(), 256).ToString();
        }

        IList<int> CountFishForDays(List<int> fish, int days)
        {
            for (var day = 1; day <= days; day++)
            {
                var newFish = new List<int>();

                for (var i = 0; i < fish.Count; i++)
                {
                    var f = fish[i];

                    if (--f == -1) {
                        newFish.Add(8);
                        f = 6;
                    }

                    fish[i] = f;
                }
                
                fish.AddRange(newFish);
            }

            return fish;
        }

        int SimulateFishForDays(IList<int> fish, int days)
        {
            var count = 0;
            
            for (var i = 0; i <= fish.Count; i += 8)
            {
                var f = fish[i];
                count = FishLife(days - f);
            }

            return count;
        }

        int FishLife(int days)
        {
            var fishCount = 1;

            for (var reproduction = days - 2; reproduction >= 0; reproduction -= 6)
            {
                fishCount += FishLife(reproduction);
            }

            return fishCount;
        }
    }
}
