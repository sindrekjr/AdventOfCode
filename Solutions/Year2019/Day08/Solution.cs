namespace AdventOfCode.Solutions.Year2019 {

    class Day08 : ASolution {

        public Day08() : base(8, 2019, "Space Image Format") {

        }

        protected override string SolvePartOne() {
            var image = Input.ToIntArray().Split(25).Split(6); 
            int fewest = 0, result = 0; 
            foreach(var layer in image) {
                int digit0 = 0, digit1 = 0, digit2 = 0; 
                foreach(var row in layer) {
                    foreach(int pixel in row) {
                        if(pixel == 0) {
                            digit0++; 
                        } else if(pixel == 1) {
                            digit1++; 
                        } else if(pixel == 2) {
                            digit2++; 
                        }
                    }
                }
                if(digit0 < fewest || fewest == 0) {
                    fewest = digit0; 
                    result = digit1 * digit2; 
                }
            }
            return result.ToString();
        }

        protected override string SolvePartTwo() {
            return null;
        }
    }
}
