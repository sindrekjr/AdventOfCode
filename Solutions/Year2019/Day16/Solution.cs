using System.Linq; 

namespace AdventOfCode.Solutions.Year2019 {

    class Day16 : ASolution {

        int[] pattern = {0, 1, 0, -1};
        int offset; 

        public Day16() : base(16, 2019, "Flawed Frequency Transmission") {
            //DebugInput = "80871224585914546619083218645595";
            //DebugInput = "03036732577212944063491565474664";
        }

        protected override string SolvePartOne() => string.Join("", RunPhases(Input.ToIntArray()).Take(8).Select(i => i.ToString()));

        protected override string SolvePartTwo() {
            offset = int.Parse(Input.Substring(0, 7)); 
            int[] rEaLsIgNaL = string.Concat(Enumerable.Repeat(Input, 10000)).ToIntArray(); 
            int[] signalReturn = RunPhases(rEaLsIgNaL); 
            string result = ""; 
            for(int i = offset; i < offset + 8; i++) result += signalReturn[i]; 
            return result;
        }

        int[] RunPhases(int[] signal, int phases = 100) {
            for(int phase = 0; phase < 100; phase++) {
                int[] deconstruct = new int[signal.Length + 1]; 
                for(int i = 0; i < signal.Length; i++) deconstruct[i + 1] = deconstruct[i] + signal[i];

                for(int i = 0; i < signal.Length; i++) {
                    int sum = 0; 
                    int ln = i + 1;
                    int pl = 0; 

                    for(int j = 0; j < signal.Length; j++) {
                        int pos = (j + 1) * ln - 1; 
                        if(pos >= signal.Length) {
                            sum += (deconstruct[signal.Length] - deconstruct[pl]) * pattern[j % 4]; 
                            break; 
                        } else {
                            sum += (deconstruct[pos] - deconstruct[pl]) * pattern[j % 4]; 
                            pl = pos; 
                        }
                    }

                    if(sum < 0) sum *= -1; 
                    sum %= 10;
                    signal[i] = sum; 
                }
            }

            return signal; 
        }
    }
}
