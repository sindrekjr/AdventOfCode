namespace AdventOfCode.Solutions.Year2021
{
    class Day16 : ASolution
    {
        public Day16() : base(16, 2021, "Packet Decoder") { }

        protected override string SolvePartOne() => ParsePacket(ParseInput()).versionSum.ToString();

        protected override string SolvePartTwo()
        {
            return null;
        }

        int[] ParseInput() => Input.Select(hex => Convert.ToString(Convert.ToInt32(hex.ToString(), 16), 2).PadLeft(4, '0')).JoinAsStrings().ToIntArray();

        (int versionSum, long valSum, int distance) ParsePacket(IEnumerable<int> packet, int siblings = 0)
        {
            if (!packet.Contains(1)) return (0, 0, 0);

            var version = Version(packet);

            if (TypeId(packet) == 4)
            {
                var i = 6;
                var last = false;
                var literal = "";

                do
                {
                    var section = packet.Skip(i).Take(5).JoinAsStrings();
                    last = section.First() == '0';
                    literal += section.Skip(1).JoinAsStrings();
                    i += 5;
                } while (!last);
                
                var other = ParsePacket(packet.Skip(i));
                return (version + other.versionSum, ToInt(literal) + other.valSum, i + other.distance);
            }

            var lengthTypeId = LengthTypeId(packet);

            if (lengthTypeId == 0)
            {
                var length = BitLength(packet);
                var sub = ParsePacket(packet.Skip(7 + 15).Take(length));
                var next = ParsePacket(packet.Skip(7 + 15 + length));
                return (version + sub.versionSum + next.versionSum, sub.valSum + next.valSum, 22 + sub.distance + next.distance);
            }

            var (subSum, subVal, subDis) = ParsePacket(packet.Skip(18), CountLength(packet));
            if (siblings == 0) return (subSum + version, subVal + 0, subDis + 18);
            
            var (sibSum, sibVal, sibDis) = ParsePacket(packet.Skip(subDis + 18), siblings - 1);
            return (subSum + sibSum + version, subVal + sibVal, subDis + sibDis + 18);
        }

        int Version(IEnumerable<int> bin) => (int) ToInt(bin.Take(3));

        int TypeId(IEnumerable<int> bin) => (int) ToInt(bin.Skip(3).Take(3));

        int LengthTypeId(IEnumerable<int> bin) => bin.Skip(6).Take(1).First();

        int BitLength(IEnumerable<int> bin) => (int) ToInt(bin.Skip(7).Take(15));

        int CountLength(IEnumerable<int> bin) => (int) ToInt(bin.Skip(7).Take(15));

        long ToInt(IEnumerable<int> bin) => ToInt(bin.JoinAsStrings());

        long ToInt(string bin) => Convert.ToInt64(bin, 2);
    }
}
