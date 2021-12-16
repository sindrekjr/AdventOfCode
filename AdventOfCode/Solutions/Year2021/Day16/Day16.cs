using System.Text;

namespace AdventOfCode.Solutions.Year2021
{
    class Day16 : ASolution
    {
        Queue<int> InputAsQueue => new Queue<int>(ParseInput());

        public Day16() : base(16, 2021, "Packet Decoder") { }

        protected override string SolvePartOne() => ParsePacketQueue(InputAsQueue).version.ToString();

        protected override string SolvePartTwo() => ParsePacketQueue(InputAsQueue).values.JoinAsStrings(",");

        int[] ParseInput() => Input.Select(hex => Convert.ToString(Convert.ToInt32(hex.ToString(), 16), 2).PadLeft(4, '0')).JoinAsStrings().ToIntArray();

        (int version, IList<long> values) ParsePacketQueue(Queue<int> packet)
        {
            var values = new List<long>();
            if (!packet.Contains(1)) return (0, values);

            var version = ToInt(Dequeue(packet, 3));
            var typeId = ToInt(Dequeue(packet, 3));

            if (typeId == 4)
            {
                var last = false;
                var literal = "";

                do
                {
                    var section = Dequeue(packet, 5);
                    last = section.First() == '0';
                    literal += section.Skip(1).JoinAsStrings();
                    values.Add(ToLong(literal));
                } while (!last);

                return (version, values);
            }

            var lengthTypeId = ToInt(Dequeue(packet, 1));

            if (lengthTypeId == 0)
            {
                var subValues = new List<long>();
                var length = ToInt(Dequeue(packet, 15));
                var end = packet.Count - length;
                
                while (end < packet.Count)
                {
                    var sub = ParsePacketQueue(packet);
                    subValues.AddRange(sub.values);
                    version += sub.version;
                }

                values.Add(MathByType(subValues, typeId));
                return (version, values);
            }

            var subPacketCount = ToInt(Dequeue(packet, 11));
            values.Add(MathByType(Enumerable.Range(0, subPacketCount).Aggregate(new List<long>(), (list, _) =>
            {
                var sub = ParsePacketQueue(packet);
                list.AddRange(sub.values);
                version += sub.version;
                return list;
            }), typeId));

            return (version, values);
        }

        string Dequeue(Queue<int> queue, int count) =>
            Enumerable.Range(0, count).Aggregate("", (acc, _) => acc + queue.Dequeue());

        (int versionSum, IList<long> values, int distance) ParsePacket(IEnumerable<int> packet, int siblings = 0)
        {
            var values = new List<long>();
            if (!packet.Contains(1)) return (0, values, 0);

            var version = Version(packet);
            var typeId = TypeId(packet);
            var distance = 6;

            if (typeId == 4)
            {
                var last = false;
                var literal = "";

                do
                {
                    var section = packet.Skip(distance).Take(5).JoinAsStrings();
                    last = section.First() == '0';
                    literal += section.Skip(1).JoinAsStrings();
                    values.Add(ToLong(literal));
                    distance += 5;
                } while (!last);

                var other = ParsePacket(packet.Skip(distance));
                values.AddRange(other.values);
                return (version + other.versionSum, values, distance + other.distance);
            }

            var lengthTypeId = LengthTypeId(packet);
            distance++;

            if (lengthTypeId == 0)
            {
                var subValues = new List<long>();

                distance += 15;
                var length = BitLength(packet);
                var sub = ParsePacket(packet.Skip(distance).Take(length));
                subValues.AddRange(sub.values);
                version += sub.versionSum;

                distance += length;
                while (packet.Skip(distance).Contains(1))
                {
                    var nextSub = ParsePacket(packet.Skip(distance));
                    distance += nextSub.distance;
                    subValues.AddRange(nextSub.values);
                    version += nextSub.versionSum;
                }


                values.Add(MathByType(subValues, typeId));

                // values.AddRange(nextSub.values);
                return (version, values, distance);
            }

            distance += 11;
            var subPacket = ParsePacket(packet.Skip(distance), CountLength(packet));
            values.Add(MathByType(subPacket.values, typeId));
            version += subPacket.versionSum;
            distance += subPacket.distance;

            if (siblings > 0)
            {
                var sibling = ParsePacket(packet.Skip(distance), siblings - 1);
                values.AddRange(sibling.values);
                version += sibling.versionSum;
                distance += sibling.distance;
            }

            return (version, values, distance);
        }

        long MathByType(IList<long> values, int type) => type switch
        {
            0 => values.Sum(),
            1 => values.Aggregate(1L, (product, value) => product * value),
            2 => values.Min(),
            3 => values.Max(),
            5 => values.First() > values.Last() ? 1 : 0,
            6 => values.First() < values.Last() ? 1 : 0,
            7 => values.First() == values.Last() ? 1 : 0,
            _ => throw new Exception("Something went bad."),
        };

        int Version(IEnumerable<int> bin) => (int)ToLong(bin.Take(3));

        int TypeId(IEnumerable<int> bin) => (int)ToLong(bin.Skip(3).Take(3));

        int LengthTypeId(IEnumerable<int> bin) => bin.Skip(6).Take(1).First();

        int BitLength(IEnumerable<int> bin) => (int)ToLong(bin.Skip(7).Take(15));

        int CountLength(IEnumerable<int> bin) => (int)ToLong(bin.Skip(7).Take(15));

        int ToInt(string bin) => Convert.ToInt32(bin, 2);

        long ToLong(IEnumerable<int> bin) => ToLong(bin.JoinAsStrings());

        long ToLong(string bin) => Convert.ToInt64(bin, 2);
    }
}
