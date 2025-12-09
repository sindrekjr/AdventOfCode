const std = @import("std");

const core = @import("../core.zig");

pub fn solve(part: core.Part, input: []const u8) ?[*]u8 {
    return switch (part) {
        .Part1 => solvePartOne(input),
        .Part2 => solvePartTwo(input),
    };
}

const JunctionBox = struct {
    x: isize,
    y: isize,
    z: isize,
    pub fn new(str: []const u8) !JunctionBox {
        var parts = std.mem.tokenizeScalar(u8, str, ',');
        return JunctionBox{
            .x = try std.fmt.parseUnsigned(isize, parts.next().?, 10),
            .y = try std.fmt.parseUnsigned(isize, parts.next().?, 10),
            .z = try std.fmt.parseUnsigned(isize, parts.next().?, 10),
        };
    }

    pub fn distance(self: *const JunctionBox, other: JunctionBox) u64 {
        const x = @abs(self.x - other.x);
        const y = @abs(self.y - other.y);
        const z = @abs(self.z - other.z);
        return (x * x) + (y * y) + (z * z);
    }
};

const JunctionBoxPair = struct {
    a: JunctionBox,
    b: JunctionBox,
    distance: u64,

    fn sortByDistance(_: void, a: JunctionBoxPair, b: JunctionBoxPair) bool {
        return a.distance > b.distance;
    }
};

fn sortByLength(_: void, a: std.ArrayList(JunctionBox), b: std.ArrayList(JunctionBox)) bool {
    return a.items.len > b.items.len;
}

pub fn solvePartOne(input: []const u8) ?[*]u8 {
    const allocator = std.heap.page_allocator;
    var lines = std.mem.tokenizeScalar(u8, input, '\n');

    var boxes: std.ArrayList(JunctionBox) = .empty;
    defer boxes.deinit(allocator);
    while (lines.next()) |line| {
        const junctionBox = JunctionBox.new(line) catch unreachable;
        boxes.append(allocator, junctionBox) catch unreachable;
    }

    var pairs: std.ArrayList(JunctionBoxPair) = .empty;
    defer pairs.deinit(allocator);

    while (boxes.pop()) |a| {
        for (boxes.items) |b| {
            const distance = a.distance(b);
            pairs.append(allocator, JunctionBoxPair{ .a = a, .b = b, .distance = distance }) catch unreachable;
        }
    }

    std.mem.sort(JunctionBoxPair, pairs.items, {}, JunctionBoxPair.sortByDistance);

    var circuits: std.ArrayList(std.ArrayList(JunctionBox)) = .empty;
    var connected: usize = 0;
    while (pairs.pop()) |pair| {
        const a = pair.a;
        const b = pair.b;

        var foundA: ?usize = null;
        var foundB: ?usize = null;
        for (circuits.items, 0..) |circuit, i| {
            for (circuit.items) |box| {
                if (box.x == a.x and box.y == a.y and box.z == a.z) {
                    foundA = i;
                }
                if (box.x == b.x and box.y == b.y and box.z == b.z) {
                    foundB = i;
                }
            }

            if (foundA != null and foundB != null) break;
        }

        if (foundA != null and foundB == null) {
            circuits.items[foundA.?].append(allocator, b) catch unreachable;
            connected += 1;
        } else if (foundA == null and foundB != null) {
            circuits.items[foundB.?].append(allocator, a) catch unreachable;
            connected += 1;
        } else if (foundA != null and foundB != null) {
            if (foundA.? != foundB.?) {
                while (circuits.items[foundA.?].pop()) |box| {
                    circuits.items[foundB.?].append(allocator, box) catch unreachable;
                }
            }
            connected += 1;
        } else {
            var circuit: std.ArrayList(JunctionBox) = .empty;
            circuit.append(allocator, a) catch unreachable;
            circuit.append(allocator, b) catch unreachable;
            circuits.append(allocator, circuit) catch unreachable;
            connected += 1;
        }

        if (connected >= 1000) break;
    }

    std.mem.sort(std.ArrayList(JunctionBox), circuits.items, {}, sortByLength);

    var product: usize = 1;
    for (0..3) |i| {
        product *= circuits.items[i].items.len;
    }

    return core.toString(usize, &allocator, product);
}

pub fn solvePartTwo(input: []const u8) ?[*]u8 {
    _ = input;
    return null;
}
