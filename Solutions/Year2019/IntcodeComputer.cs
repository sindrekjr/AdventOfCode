using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Numerics; 

namespace AdventOfCode.Solutions.Year2019 {

    class IntcodeComputer : IntcodeComputer<int> {
        public IntcodeComputer(int[] input) : base(input) {

        }
    }

    class IntcodeComputer<T> where T : IComparable<T> {

        int pointer, relative; 
        readonly BigInteger[] intcode; 

        public bool Debug { get; set; }
        public bool Paused { get; private set; }
        public BigInteger[] Memory { get; private set; }
        public Queue<BigInteger> Input { get; private set; }
        public Queue<BigInteger> Output { get; private set; }

        // Constructor
        public IntcodeComputer(T[] input) {
            this.intcode = input.Select(n => (BigInteger) Convert.ToInt64(n)).ToArray();            
            Initialize(); 
        }

        // Field initializer
        public IntcodeComputer<T> Initialize(int? size = null) {
            Memory = new BigInteger[size ?? intcode.Length];
            Array.Copy(intcode, Memory, intcode.Length); 
            Input = new Queue<BigInteger>(); 
            Output = new Queue<BigInteger>(); 
            pointer = 0; 
            relative = 0; 
            return this; 
        }

        // Main method 
        public IntcodeComputer<T> Run(bool debug = false) {
            Debug = debug; 
            Paused = false; 
            while(DoOperation(ParseInstruction((int) Memory[pointer]))); 
            return this;                 
        }

        public IntcodeComputer<T> WriteInput(params T[] input) {
            foreach(T i in input) Input.Enqueue(Convert.ToInt64(i)); 
            return this; 
        }

        public IntcodeComputer<T> SetMemory(int pos, T val) {
            Memory[pos] = Convert.ToInt64(val); 
            return this; 
        }

        public IntcodeComputer<T> SetMemory(params (int, T)[] values) {
            foreach((int pos, T val) x in values) SetMemory(x.pos, x.val); 
            return this; 
        }

        public BigInteger Diagnose() => Output.Last(); 

        // Returns true as long as program should continue
        bool DoOperation((Opcode opcode, Mode[] modes) instruction) {
            if(Debug) {
                Console.WriteLine(); 
                Console.WriteLine($"Opcode: {instruction.opcode }; Ptr: {pointer}; Rel: {relative}");
                Console.WriteLine($"Modes: {instruction.modes[0]}, {instruction.modes[1]}, {instruction.modes[2]}");
                for(int i = 0; i < intcode.Length; i++) Console.Write(Memory[i] + ",");
                
                Console.Write("\n>> ");
                Console.ReadLine();
            }
            
            int[] pointers; 
            switch(instruction.opcode) {
                // Immediately halt the program
                case Opcode.Halt: 
                    return false; 


                // Store the sum of the first two parameters in the position given by the third parameter
                case Opcode.Add: 
                    pointers = ParseParams(instruction.modes, 3); 
                    Memory[pointers[2]] = Memory[pointers[0]] + Memory[pointers[1]]; 
                    return true; 


                // Store the product of the first two parameters in position given by the third parameter
                case Opcode.Multiply:
                    pointers = ParseParams(instruction.modes, 3); 
                    Memory[pointers[2]] = Memory[pointers[0]] * Memory[pointers[1]]; 
                    return true; 


                // Store the next queued input in the position given by the instruction's only parameter
                case Opcode.Input:
                    if(Input.Count == 0) {
                        Paused = true; 
                        return false; 
                    } else {
                        Memory[ParseParams(instruction.modes, 1)[0]] = Input.Dequeue(); 
                        return true; 
                    }


                // Output the value of the position given by the instruction's only parameter
                case Opcode.Output: 
                    Output.Enqueue(Memory[ParseParams(instruction.modes, 1)[0]]);
                    return true; 


                // Jump to the value given by the second parameter if the first parameter does not equal 0
                case Opcode.JumpIfTrue:
                    pointers = ParseParams(instruction.modes, 2); 
                    //Console.WriteLine(pointers[0] + ", " + pointers[1]);
                    if(Memory[pointers[0]] != 0) pointer = (int) Memory[pointers[1]];
                    return true; 


                // Jump to the value given by the second parameter if the first parameter equals 0
                case Opcode.JumpIfFalse:
                    pointers = ParseParams(instruction.modes, 2); 
                    if(Memory[pointers[0]] == 0) pointer = (int) Memory[pointers[1]];
                    return true;


                // Store 1 (true) or 0 (false) in the position given by the third parameter if the first parameter is less than the second
                case Opcode.LessThan:
                    pointers = ParseParams(instruction.modes, 3); 
                    Memory[pointers[2]] = (Memory[pointers[0]] < Memory[pointers[1]]) ? 1 : 0; 
                    return true; 


                // Store 1 (true) or 0 (false) in the position given by the third parameter if the first two parameters are equal
                case Opcode.Equals:
                    pointers = ParseParams(instruction.modes, 3); 
                    Memory[pointers[2]] = (Memory[pointers[0]] == Memory[pointers[1]]) ? 1 : 0; 
                    return true; 


                // Adjust the value of the relative base by the instruction's only parameter
                case Opcode.Adjust: 
                    relative += (int) Memory[ParseParams(instruction.modes, 1)[0]]; 
                    return true; 


                // Something went wrong
                default: 
                    string error = $"\nOpcode: {instruction.opcode }; Ptr: {pointer}; Rel: {relative}\n";
                    error += $"Modes: {instruction.modes[0]}, {instruction.modes[1]}, {instruction.modes[2]}\n";
                    throw new SomethingWentWrongException(error); 
            }
        }

        (Opcode opcode, Mode[] modes) ParseInstruction(int instruction) =>
            (
                (Opcode) (instruction % 100),
                instruction.ToString("D5").Remove(3).Select<char, Mode>(c => Enum.Parse<Mode>(c.ToString())).ToArray().Reverse().ToArray()
            );

        int[] ParseParams(Mode[] modes, int amount) {
            var result = new int[amount]; 
            for(int i = 0; i < amount; i++) {
                result[i] = modes[i] switch {
                    Mode.Position => (int) Memory[++pointer],
                    Mode.Immediate => ++pointer,
                    Mode.Relative => (int) Memory[++pointer] + relative,
                    _ => throw new SomethingWentWrongException()
                };
            }
            pointer++; 
            return result; 
        }

        enum Opcode { Add = 1, Multiply, Input, Output, JumpIfTrue, JumpIfFalse, LessThan, Equals, Adjust, Halt = 99 }
        enum Mode { Position, Immediate, Relative }
    }

    class SomethingWentWrongException : Exception {

        public SomethingWentWrongException() {}
        public SomethingWentWrongException(string message) : base(message) {}
    }
}
