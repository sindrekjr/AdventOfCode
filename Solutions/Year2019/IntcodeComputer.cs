using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Numerics; 

namespace AdventOfCode.Solutions.Year2019 {

    class NewIntcodeComputer<T> where T : IComparable<T> {

        int pointer, relative; 
        readonly BigInteger[] intcode; 

        public bool Debug { get; set; }
        public BigInteger[] Memory { get; private set; }
        public Queue<BigInteger> Input { get; private set; }
        public Queue<BigInteger> Output { get; private set; }

        // Constructor
        public NewIntcodeComputer(T[] input) {
            var list = new List<BigInteger>();
            foreach(T i in input) {
                list.Add(Convert.ToInt64(i));
            }
            this.intcode = list.ToArray(); 
            
            Initialize(); 
        }

        // Field initializer
        public NewIntcodeComputer<T> Initialize() {
            Memory = new BigInteger[intcode.Length];
            Array.Copy(intcode, Memory, intcode.Length); 
            Input = new Queue<BigInteger>(); 
            Output = new Queue<BigInteger>(); 
            pointer = 0; 
            relative = 0; 
            return this; 
        }

        // Main method 
        public NewIntcodeComputer<T> Run(bool debug = false) {
            Debug = debug; 
            while(DoOperation(ParseInstruction((int) Memory[pointer]))); 
            return this;                 
        }

        public NewIntcodeComputer<T> WriteInput(params T[] input) {
            foreach(T i in input) Input.Enqueue(Convert.ToInt64(i)); 
            return this; 
        }

        public NewIntcodeComputer<T> SetMemory(int pos, T val) {
            Memory[pos] = Convert.ToInt64(val); 
            return this; 
        }

        public NewIntcodeComputer<T> SetMemory(params (int, T)[] values) {
            foreach((int pos, T val) x in values) SetMemory(x.pos, x.val); 
            return this; 
        }

        public BigInteger Diagnose() => Output.Last(); 

        // Returns true as long as program should continue
        bool DoOperation((Opcode opcode, Mode[] modes) instruction) {
            if(Debug) {
                Console.WriteLine("Opcode: " + instruction.opcode + "; Ptr: " + pointer);
                Console.WriteLine("Modes " + instruction.modes[0] + " " + instruction.modes[1] + " " + instruction.modes[2]);
                foreach(BigInteger i in Memory) Console.Write(i + ",");
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


                // Something went wrong
                default: 
                    throw new SomethingWentWrongException(); 
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
                    Mode.Relative => (int) Memory[++pointer + relative],
                    _ => throw new SomethingWentWrongException()
                };
            }
            pointer++; 
            return result; 
        }

        enum Opcode { Add = 1, Multiply, Input, Output, JumpIfTrue, JumpIfFalse, LessThan, Equals, Adjust, Halt = 99 }
        enum Mode { Position, Immediate, Relative }
    }

    class IntcodeComputer<T> where T : IComparable<T> {

        readonly T[] intcode; 
        bool initialized = false; 
        
        public T[] Memory { get; private set; }
        public bool Paused { get; private set; }
        public Queue<T> Input { get; private set; }
        public Queue<T> Output { get; private set; }

        public IntcodeComputer(T[] intcode) {
            this.intcode = intcode;
        }

        public IntcodeComputer<T> Initialize(int? noun = null, int? verb = null) {
            Memory = new T[intcode.Length];
            intcode.CopyTo(Memory, 0);
            if(noun != null) Memory[1] = (T)(object)noun.Value; 
            if(verb != null) Memory[2] = (T)(object)verb.Value; 
            Input = new Queue<T>(); 
            Output = new Queue<T>(); 
            initialized = true; 
            return this; 
        } 

        public IntcodeComputer<T> InputSequence(params T[] inp) {
            Initialize(); 
            foreach(T i in inp) Input.Enqueue(i); 
            return this; 
        }

        public IntcodeComputer<T> Run(int? inp = null) {
            if(inp != null) InputSequence((T)(dynamic)inp.Value); 
            if(!initialized) Initialize(); 
            Paused = false; 
            int i = 0; 
            int rel = 0; 
            while(true) {
                (Mode[] modes, Opcode opcode) = ParseInstruction((int)(dynamic)Memory[i]); 
                if(opcode == Opcode.Halt) {
                    return this; 
                } else if(opcode == Opcode.Input) {
                    if(Input.Count > 0) {
                        Memory[modes[0] switch {
                            Mode.Position => (dynamic)Memory[++i],
                            Mode.Immediate => ++i,
                            Mode.Relative => (dynamic)Memory[++i + rel],
                            _ => throw new SomethingWentWrongException()
                        }] = Input.Dequeue(); 
                    } else {
                        Paused = true; 
                        return this; 
                    }
                } else if(opcode == Opcode.Output) {
                    Output.Enqueue(modes[0] switch {
                        Mode.Position => Memory[(dynamic)Memory[++i]],
                        Mode.Immediate => Memory[++i],
                        Mode.Relative => Memory[(dynamic)Memory[++i + rel]],
                        _ => throw new SomethingWentWrongException()
                    });
                } else if(opcode == Opcode.Adjust) {
                    rel += modes[0] switch {
                        Mode.Position => (dynamic)Memory[(dynamic)Memory[++i]],
                        Mode.Immediate => (dynamic)Memory[++i],
                        Mode.Relative => (dynamic)Memory[(dynamic)Memory[++i + rel]],
                        _ => throw new SomethingWentWrongException()
                    };
                } else {
                    dynamic val1 = modes[0] switch {
                        Mode.Position => Memory[(dynamic)Memory[++i]],
                        Mode.Immediate => Memory[++i],
                        Mode.Relative => Memory[(dynamic)Memory[++i + rel]],
                        _ => throw new SomethingWentWrongException()
                    };
                    dynamic val2 = modes[1] switch {
                        Mode.Position => Memory[(dynamic)Memory[++i]],
                        Mode.Immediate => Memory[++i],
                        Mode.Relative => Memory[(dynamic)Memory[++i + rel]],
                        _ => throw new SomethingWentWrongException()
                    };
                    switch(opcode) {
                        case Opcode.Add: 
                            Memory[modes[2] switch {
                                Mode.Position => (dynamic)Memory[++i],
                                Mode.Immediate => ++i,
                                Mode.Relative => (dynamic)Memory[++i + rel],
                                _ => throw new SomethingWentWrongException()
                            }] = val1 + val2; 
                            break; 
                        case Opcode.Multiply: 
                            Memory[modes[2] switch {
                                Mode.Position => (dynamic)Memory[++i],
                                Mode.Immediate => ++i,
                                Mode.Relative => (dynamic)Memory[++i + rel],
                                _ => throw new SomethingWentWrongException()
                            }] = val1 * val2; 
                            break;
                        case Opcode.JumpTrue:
                            if(val1 != 0) {
                                i = val2; 
                                continue; 
                            }
                            break; 
                        case Opcode.JumpFalse:
                            if(val1 == 0) {
                                i = val2; 
                                continue; 
                            }
                            break; 
                        case Opcode.Lt:
                            Memory[modes[2] switch {
                                Mode.Position => (dynamic)Memory[++i],
                                Mode.Immediate => ++i,
                                Mode.Relative => (dynamic)Memory[++i + rel],
                                _ => throw new SomethingWentWrongException()
                            }] = (dynamic) ((val1 < val2) ? 1 : 0); 
                            break; 
                        case Opcode.Eq:
                            Memory[modes[2] switch {
                                Mode.Position => (dynamic)Memory[++i],
                                Mode.Immediate => ++i,
                                Mode.Relative => (dynamic)Memory[++i + rel],
                                _ => throw new SomethingWentWrongException()
                            }] = (dynamic) ((val1 == val2) ? 1 : 0); 
                            break; 
                        default: 
                            throw new SomethingWentWrongException();
                    }
                }
                i++; 
            }
        }

        public T Diagnose() => Output.Last(); 

        (Mode[] modes, Opcode opcode) ParseInstruction(int instruction) {
            return (
                instruction.ToString("D5").Remove(3).Reverse()
                    .Select<char, Mode>(c => Enum.Parse<Mode>(c.ToString()))
                    .ToArray(),
                (Opcode) (instruction % 100)
            );
        }

        enum Opcode { Add = 1, Multiply, Input, Output, JumpTrue, JumpFalse, Lt, Eq, Adjust, Halt = 99 }
        enum Mode { Position, Immediate, Relative }
    }

    class SomethingWentWrongException : Exception {

    }
}
