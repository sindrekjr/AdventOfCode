using System.Numerics;

namespace AdventOfCode.Solutions.Year2021.Day19;

class Solution : SolutionBase
{
    public Solution() : base(19, 2021, "Beacon Scanner") { }

    protected override string? SolvePartOne() => GetScannersAligned()
        .SelectMany(a => a.AbsBeacons)
        .ToHashSet().Count
        .ToString();

    protected override string? SolvePartTwo()
    {
        var largestDistance = 0F;
        var positions = GetScannersAligned().Select(a => a.Position).ToArray();
        for (int i = 0; i < positions.Length; i++) for (int j = i + 1; j < positions.Length; j++)
        {
            var distance = positions[i].Distance(positions[j]);
            if (distance > largestDistance) largestDistance = distance;
        }

        return largestDistance.ToString();
    }

    IEnumerable<Scanner> GetScannersAligned()
    {
        var scanners = Input.SplitByParagraph().Select(p => new Scanner(p)).ToDictionary(s => s.Id, s => s);

        var first = scanners[0];
        scanners.Remove(first.Id);

        var aligned = new Dictionary<int, Scanner>() { [first.Id] = first };
        var checklist = new Queue<int>();
        checklist.Enqueue(first.Id);

        while (checklist.Count > 0 && scanners.Count > 0)
        {
            var id = checklist.Dequeue();
            var matchlist = new List<Scanner>();
            foreach (var scanner in scanners.Values)
            {
                for (int o = 0; o < scanner.BeaconSets.Count; o++)
                {
                    scanner.Orientation = o;
                    if (AreAligned(aligned[id], scanner))
                    {
                        matchlist.Add(scanner);
                        break;
                    }
                }
            }

            foreach(var match in matchlist)
            {
                aligned[match.Id] = match;
                checklist.Enqueue(match.Id);
                scanners.Remove(match.Id);
            }
        }

        return aligned.Values;
    }

    bool AreAligned(Scanner a, Scanner b)
    {
        foreach (var aB in a.AbsBeacons) foreach (var bB in b.Beacons)
        {
            b.Position = aB - bB;
            if (a.AbsBeacons.Intersect(b.AbsBeacons).Count() >= 12) return true;
        }

        return false;
    }
}

internal record Scanner
{
    public int Id { get; }
    public int Orientation { get; set; } = 0;
    public Vector3 Position { get; set; } = new Vector3(0,0,0);
    public IDictionary<int, HashSet<Vector3>> BeaconSets { get; set; }
    public HashSet<Vector3> Beacons => BeaconSets[Orientation];
    public HashSet<Vector3> AbsBeacons => Beacons.Select(vec => vec + Position).ToHashSet();

    public Scanner(string paragraph)
    {
        var (header, beacons) = paragraph.SplitByNewline();

        Id = int.Parse(header.Remove(header.Length - 4).Substring(12));
        BeaconSets = new Dictionary<int, HashSet<Vector3>>();
        ParseBeaconsInOrientations(beacons);
    }

    void ParseBeaconsInOrientations(IEnumerable<string> beacons)
    {
        foreach (var beacon in beacons)
        {
            var permutations = beacon.ToVector3().Orientations().ToArray();

            for (int i = 0; i < permutations.Length; i++)
            {
                if (!BeaconSets.ContainsKey(i)) BeaconSets.Add(i, new HashSet<Vector3>());
                BeaconSets[i].Add(permutations[i]);
            }
        }
    }
}

static class Vector3Extensions
{
    public static float Distance(this Vector3 a, Vector3 b) => new[]
    {
        Math.Abs(a.X - b.X),
        Math.Abs(a.Y - b.Y),
        Math.Abs(a.Z - b.Z),
    }.Sum();
        

    public static IEnumerable<Vector3> Orientations(this Vector3 vec) =>
        vec.Facings().SelectMany(vector => vector.Rotations());

    public static IEnumerable<Vector3> Facings(this Vector3 vec) => new[]
    {
        vec,
        new Vector3(-vec.X, -vec.Y, vec.Z),
        new Vector3(vec.Y, vec.Z, vec.X),
        new Vector3(-vec.Y, -vec.Z, vec.X),
        new Vector3(vec.Z, vec.X, vec.Y),
        new Vector3(-vec.Z, -vec.X, vec.Y),
    };

    public static IEnumerable<Vector3> Rotations(this Vector3 vec) => new[]
    {
        vec,
        new Vector3(vec.X, -vec.Z, vec.Y),
        new Vector3(vec.X, -vec.Y, -vec.Z),
        new Vector3(vec.X, vec.Z, -vec.Y),
    };

    public static Vector3 ToVector3(this string str)
    {
        var points = str.Split(",").Select(int.Parse).ToArray();
        return new Vector3(points[0], points[1], points[2]);
    }
}
