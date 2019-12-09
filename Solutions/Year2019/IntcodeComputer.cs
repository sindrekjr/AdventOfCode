using System; 
using System.Collections.Generic; 
using System.Linq; 

namespace AdventOfCode.Solutions.Year2019 {

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
            if(inp != null) InputSequence((T)(object)inp.Value); 
            if(!initialized) Initialize(); 
            Paused = false; 
            dynamic i = 0; 
            dynamic rel = 0; 
            while(true) {
                //Console.WriteLine(Memory[2]);
                (Mode[] modes, Opcode opcode) = ParseInstruction((int)(object)Memory[i]); 
                //Console.WriteLine(modes.ToString());
                if(opcode == Opcode.Halt) {
                    return this; 
                } else if(opcode == Opcode.Input) {
                    if(Input.Count > 0) {
                        Memory[modes[0] switch {
                            Mode.Position => Memory[++i],
                            Mode.Immediate => ++i,
                            Mode.Relative => Memory[++i] + rel,
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
                        Mode.Relative => Memory[Memory[++i] + rel],
                        _ => throw new SomethingWentWrongException()
                    });
                } else if(opcode == Opcode.Adjust) {
                    rel += modes[0] switch {
                        Mode.Position => Memory[(dynamic)Memory[++i]],
                        Mode.Immediate => Memory[++i],
                        Mode.Relative => Memory[Memory[++i] + rel],
                        _ => throw new SomethingWentWrongException()
                    };
                } else {
                    dynamic val1 = modes[0] switch {
                        Mode.Position => Memory[(dynamic)Memory[++i]],
                        Mode.Immediate => Memory[++i],
                        Mode.Relative => Memory[Memory[++i] + rel],
                        _ => throw new SomethingWentWrongException()
                    };
                    dynamic val2 = modes[1] switch {
                        Mode.Position => Memory[(dynamic)Memory[++i]],
                        Mode.Immediate => Memory[++i],
                        Mode.Relative => Memory[Memory[++i] + rel],
                        _ => throw new SomethingWentWrongException()
                    };
                    switch(opcode) {
                        case Opcode.Add: 
                            Memory[modes[2] switch {
                                Mode.Position => Memory[++i],
                                Mode.Immediate => ++i,
                                Mode.Relative => Memory[++i] + rel,
                                _ => throw new SomethingWentWrongException()
                            }] = val1 + val2; 
                            break; 
                        case Opcode.Multiply: 
                            Memory[modes[2] switch {
                                Mode.Position => Memory[++i],
                                Mode.Immediate => ++i,
                                Mode.Relative => Memory[++i] + rel,
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
                                Mode.Position => Memory[++i],
                                Mode.Immediate => ++i,
                                Mode.Relative => Memory[++i] + rel,
                                _ => throw new SomethingWentWrongException()
                            }] = (dynamic) ((val1 < val2) ? 1 : 0); 
                            break; 
                        case Opcode.Eq:
                            Memory[modes[2] switch {
                                Mode.Position => Memory[++i],
                                Mode.Immediate => ++i,
                                Mode.Relative => Memory[++i] + rel,
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
