using System.Linq; 

namespace AdventOfCode.Solutions.Year2019 {

    class Day16 : ASolution {

        public Day16() : base(16, 2019, "Flawed Frequency Transmission") {
            //DebugInput = "80871224585914546619083218645595";
        }

        protected override string SolvePartOne() {
            int[] signal = Input.ToIntArray(); 
            int[] pattern = {0, 1, 0, -1};            


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
            /* for(int i = 0; i < signal.Length; i++) {
                int phase = signal[i]; 
                foreach(int p in pattern) {
                    for(int j = 0; j < i + 1; j++) {
                        if(i == 0 && j == 0) continue; 
                    }
                }
            } */
            return string.Join("", signal.Take(8).Select(i => i.ToString()));
        }

        void Phase() {
            
        }

        protected override string SolvePartTwo() {
            return null;
        }
    }
}
