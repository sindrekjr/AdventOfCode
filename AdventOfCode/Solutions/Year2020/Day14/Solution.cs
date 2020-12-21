using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day14 : ASolution
    {
        string Bitmask;

        public Day14() : base(14, 2020, "Docking Data") { }

        protected override string SolvePartOne()
        {
            long[] memory = new long[99999];
            foreach (var instruction in GetProgram())
            {
                var (cmd, val, _) = instruction.Split(" = ");
                if (cmd == "mask")
                {
                    Bitmask = val;
                }
                else
                {
                    var address = int.Parse(cmd.Substring(4, cmd.Length - 5));
                    var b = new BitArray(Bitmask.Length);
                    var bitval = new BitArray(new int[] { int.Parse(val) });
                    for (int i = 0; i < b.Length; i++)
                    {
                        if (Bitmask[Bitmask.Length - i - 1] == 'X')
                        {
                            b[i] = (i < bitval.Length) ? bitval[i] : false;
                        }
                        else
                        {
                            b[i] = Bitmask[Bitmask.Length - i - 1] == '1';
                        }
                    }
                    memory[address] = b.ToLong();
                }
            }
            return memory.Sum().ToString();
        }

        protected override string SolvePartTwo()
        {
            var memory = new Dictionary<string, long>();
            foreach (var instruction in GetProgram())
            {
                var (cmd, val, _) = instruction.Split(" = ");
                if (cmd == "mask")
                {
                    Bitmask = val;
                }
                else
                {
                    foreach (var addr in ParseFloatingAddressesFromDecimal(int.Parse(cmd.Substring(4, cmd.Length - 5))))
                    {
                        if (memory.ContainsKey(addr))
                        {
                            memory[addr] = int.Parse(val);
                        }
                        else
                        {
                            memory.Add(addr, int.Parse(val));
                        }
                    }
                }
            }

            return memory.Values.Sum().ToString();
        }

        string[] GetProgram() => Input.SplitByNewline();

        IEnumerable<string> ParseFloatingAddressesFromDecimal(int dec)
        {
            var b = new BitArray(Bitmask.Length);
            var bitval = new BitArray(new int[] { dec });
            var baseStr = new char[Bitmask.Length];
            
            for (int i = 0; i < b.Length; i++)
            {
                var correspondingMaskValue = Bitmask[Bitmask.Length - i - 1];
                if (correspondingMaskValue == '0')
                {
                    baseStr[i] = (i < bitval.Length) ? bitval[i] ? '1' : '0' : '0';
                }
                else
                {
                    baseStr[i] = correspondingMaskValue;
                }
            }

            foreach (var addr in GetAllFloating(baseStr.JoinAsStrings(), baseStr.JoinAsStrings().AllIndexesOf("X").ToArray())) yield return addr;
        }

        IEnumerable<string> GetAllFloating(string addr, int[] indices)
        {
            if (indices.Length == 1)
            {
                yield return addr.Replace('X', '0');
                yield return addr.Replace('X', '1');
            }
            else
            {
                var i = indices.First();
                foreach (var a in GetAllFloating(addr.Substring(0, i) + "0" + addr.Substring(i + 1), indices.Skip(1).ToArray()))
                {
                    yield return a;
                }

                foreach (var a in GetAllFloating(addr.Substring(0, i) + "1" + addr.Substring(i + 1), indices.Skip(1).ToArray()))
                {
                    yield return a;
                }
            }
        }
    }
}
