namespace AdventOfCode.Solutions.Year2021
{
    class Day16 : ASolution
    {
        Queue<int> InputAsQueue => new Queue<int>(ParseInput());

        public Day16() : base(16, 2021, "Packet Decoder") { }

        protected override string SolvePartOne() => ParseBitsAsPacket(InputAsQueue).VersionS.ToString();

        protected override string SolvePartTwo() => ParseBitsAsPacket(InputAsQueue).Value.ToString();

        int[] ParseInput() => Input.Select(hex => Convert.ToString(Convert.ToInt32(hex.ToString(), 16), 2).PadLeft(4, '0')).JoinAsStrings().ToIntArray();

        IPacket ParseBitsAsPacket(Queue<int> bits)
        {
            var version = ToInt(Dequeue(bits, 3));
            var typeId = ToInt(Dequeue(bits, 3));

            if (typeId == 4)
            {
                var value = 0L;
                var last = false;

                while (!last)
                {
                    last = bits.Dequeue() == 0;
                    value <<= 4;
                    value += ToLong(Dequeue(bits, 4));
                }

                return new LiteralValue { Version = version, TypeId = typeId, Value = value };
            }

            var packet = new Packet
            {
                Version = version,
                TypeId = typeId,
                SubPackets = new List<IPacket>(),
            };

            if (bits.Dequeue() == 0)
            {
                var bitLength = ToInt(Dequeue(bits, 15));
                var end = bits.Count - bitLength;

                while (end < bits.Count)
                {
                    var subPacket = ParseBitsAsPacket(bits);
                    packet.SubPackets.Add(subPacket);
                }
            }
            else
            {
                var subPacketCount = ToInt(Dequeue(bits, 11));
                for (int i = 0; i < subPacketCount; i++)
                {
                    var subPacket = ParseBitsAsPacket(bits);
                    packet.SubPackets.Add(subPacket);
                }
            }

            return packet;
        }

        string Dequeue(Queue<int> queue, int count) =>
            Enumerable.Range(0, count).Aggregate("", (acc, _) => acc + queue.Dequeue());

        int ToInt(string bin) => Convert.ToInt32(bin, 2);

        long ToLong(IEnumerable<int> bin) => ToLong(bin.JoinAsStrings());

        long ToLong(string bin) => Convert.ToInt64(bin, 2);
    }

    internal interface IPacket
    {
        int Version { get; set; }
        int TypeId { get; set; }
        int VersionS { get; }
        long Value { get; }
    }

    internal class Packet : IPacket
    {
        public IList<IPacket> SubPackets { get; set; }

        public int Version { get; set; }
        public int VersionS => Version + SubPackets.Sum(p => p.VersionS);
        public int TypeId { get; set; }
        public long Value => TypeId switch
        {
            0 => SubPackets.Sum(p => p.Value),
            1 => SubPackets.Aggregate(1L, (product, p) => product * p.Value),
            2 => SubPackets.Min(p => p.Value),
            3 => SubPackets.Max(p => p.Value),
            5 => SubPackets.First().Value > SubPackets.Last().Value ? 1 : 0,
            6 => SubPackets.First().Value < SubPackets.Last().Value ? 1 : 0,
            7 => SubPackets.First().Value == SubPackets.Last().Value ? 1 : 0,
            _ => throw new Exception("Something went bad."),
        };
    }

    internal class LiteralValue : IPacket
    {
        public int Version { get; set; }
        public int VersionS => Version;
        public int TypeId { get; set; }
        public long Value { get; set; }
    }
}
