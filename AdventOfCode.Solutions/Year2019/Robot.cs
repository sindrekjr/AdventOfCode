using System.Collections.Generic;

namespace AdventOfCode.Solutions.Year2019;

partial class XYRobot
{

    protected IntcodeComputer Computer;
    protected (int x, int y) Position;
    protected Dictionary<(int x, int y), int> Map;

    public XYRobot(IntcodeComputer comp)
    {
        Computer = comp;
        Position = (0, 0);
        Map = new Dictionary<(int x, int y), int>();
    }

    public XYRobot(IntcodeComputer comp, (int x, int y) pos, Dictionary<(int, int), int> map)
    {
        Computer = comp;
        Position = pos;
        Map = map;
    }

    protected void InitializeComputer(int memory) => Computer.Initialize(memory);

    protected void MoveUp() => Position.y++;
    protected void MoveRight() => Position.x++;
    protected void MoveDown() => Position.y--;
    protected void MoveLeft() => Position.x--;

    /*
     * Returns an array of adjacent posiion values
     * int[]{up, right, down, left}
     */
    protected int?[] PokeAround(int? def = null)
    {
        int?[] around = new int?[4];
        around[0] = Map.TryGetValue((Position.x, Position.y + 1), out int up) ? new int?(up) : def;
        around[1] = Map.TryGetValue((Position.x + 1, Position.y), out int rt) ? new int?(rt) : def;
        around[2] = Map.TryGetValue((Position.x, Position.y - 1), out int dw) ? new int?(dw) : def;
        around[3] = Map.TryGetValue((Position.x - 1, Position.y), out int lf) ? new int?(lf) : def;
        return around;
    }
}
