namespace AdventOfCode.Solutions.Year2020.Day14;

class Solution : SolutionBase
{
    string Bitmask = "";

    public Solution() : base(14, 2020, "Docking Data") { }

    protected override string? SolvePartOne()
    {
        var memory = new long[99999];
        foreach (var instruction in GetProgram())
        {
            var (cmd, val, _) = instruction.Split(" = ");
            if (cmd == "mask")
            {
                Bitmask = val;
                continue;
            }

            var b = new BitArray(Bitmask.Length);
            var bitval = new BitArray(new int[] { int.Parse(val) });
            for (int i = 0; i < b.Length; i++)
            {
                var correspondingMaskValue = Bitmask[Bitmask.Length - i - 1];
                b[i] = correspondingMaskValue == 'X'
                    ? (i < bitval.Length) ? bitval[i] : false
                    : correspondingMaskValue == '1';
            }

            memory[int.Parse(cmd.Substring(4, cmd.Length - 5))] = ToLong(b);
        }
        
        return memory.Sum().ToString();
    }

    protected override string? SolvePartTwo()
    {
        var memory = new Dictionary<string, long>();
        foreach (var instruction in GetProgram())
        {
            var (cmd, val, _) = instruction.Split(" = ");
            if (cmd == "mask")
            {
                Bitmask = val;
                continue;
            }

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

        return memory.Values.Sum().ToString();
    }

    string[] GetProgram() => Input.SplitByNewline();

    IEnumerable<string> ParseFloatingAddressesFromDecimal(int dec)
    {
        var bitval = new BitArray(new int[] { dec });
        var baseStr = new char[Bitmask.Length];
        
        for (int i = 0; i < Bitmask.Length; i++)
        {
            var correspondingMaskValue = Bitmask[Bitmask.Length - i - 1];
            baseStr[i] = correspondingMaskValue == '0'
                ? (i < bitval.Length) ? bitval[i] ? '1' : '0' : '0'
                : correspondingMaskValue;
        }

        foreach (var addr in GetAllFloating(baseStr.JoinAsStrings())) yield return addr;
    }

    IEnumerable<string> GetAllFloating(string addr)
    {
        var i = addr.IndexOf('X');

        if (i == -1)
        {
            yield return addr;
        }
        else
        {
            foreach (var a in GetAllFloating(addr.Substring(0, i) + "0" + addr.Substring(i + 1)))
            {
                yield return a;
            }

            foreach (var a in GetAllFloating(addr.Substring(0, i) + "1" + addr.Substring(i + 1)))
            {
                yield return a;
            }
        }
    }

    public long ToLong(BitArray bitArray)
    {
        long value = 0;

        for (int i = 0; i < bitArray.Count; i++)
        {
            if (bitArray[i])
                value += Convert.ToInt64(Math.Pow(2, i));
        }

        return value;
    }
}
