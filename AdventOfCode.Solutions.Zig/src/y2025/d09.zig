const std = @import("std");

const core = @import("../core.zig");

pub fn solve(part: core.Part, input: []const u8) ?[*]u8 {
    return switch (part) {
        .Part1 => solvePartOne(input),
        .Part2 => solvePartTwo(input),
    };
}

const Coordinate = struct {
    x: isize,
    y: isize,

    fn new(str: []const u8) Coordinate {
        var parts = std.mem.splitScalar(u8, str, ',');
        const x = std.fmt.parseInt(isize, parts.next().?, 10) catch unreachable;
        const y = std.fmt.parseInt(isize, parts.next().?, 10) catch unreachable;

        return Coordinate{
            .x = x,
            .y = y,
        };
    }

    fn combinedArea(self: *const Coordinate, other: Coordinate) usize {
        return (1 + @abs(self.x - other.x)) * (1 + @abs(self.y - other.y));
    }
};

pub fn solvePartOne(input: []const u8) ?[*]u8 {
    const allocator = std.heap.page_allocator;
    var lines = std.mem.tokenizeScalar(u8, input, '\n');

    var corners: std.ArrayList(Coordinate) = .empty;
    while (lines.next()) |line| {
        const corner = Coordinate.new(line);
        corners.append(allocator, corner) catch unreachable;
    }

    var largestArea: usize = 0;
    for (corners.items) |a| {
        for (corners.items) |b| {
            const area = a.combinedArea(b);
            if (area > largestArea) largestArea = area;
        }
    }

    return core.toString(usize, &allocator, largestArea);
}

pub fn solvePartTwo(input: []const u8) ?[*]u8 {
    const allocator = std.heap.page_allocator;
    var lines = std.mem.tokenizeScalar(u8, input, '\n');

    var corners: std.ArrayList(Coordinate) = .empty;
    defer corners.deinit(allocator);

    var edges: std.ArrayList([2]Coordinate) = .empty;
    defer edges.deinit(allocator);

    var first: ?Coordinate = null;
    var prev: ?Coordinate = null;
    while (lines.next()) |line| {
        const red = Coordinate.new(line);

        if (prev != null) {
            edges.append(allocator, [_]Coordinate{ prev.?, red }) catch unreachable;
        }

        corners.append(allocator, red) catch unreachable;
        if (first == null) first = red;
        prev = red;
    }

    edges.append(allocator, [_]Coordinate{ prev.?, first.? }) catch unreachable;

    var largestArea: usize = 0;
    for (corners.items) |a| {
        for (corners.items) |b| {
            const left = @min(a.x, b.x);
            const right = @max(a.x, b.x);
            const top = @min(a.y, b.y);
            const bottom = @max(a.y, b.y);

            var valid = true;
            for (edges.items) |edge| {
                const start = edge[0];
                const end = edge[1];
                if (start.x == end.x) {
                    if (start.x > left and start.x < right) {
                        const minY = @min(start.y, end.y);
                        const maxY = @max(start.y, end.y);
                        if ((minY <= top and maxY >= top) or (minY <= bottom and maxY >= bottom)) {
                            valid = false;
                            break;
                        }
                    }
                } else {
                    if (start.y > top and start.y < bottom) {
                        const minX = @min(start.x, end.x);
                        const maxX = @max(start.x, end.x);
                        if ((minX <= left and maxX >= left) or (minX <= right and maxX >= right)) {
                            valid = false;
                            break;
                        }
                    }
                }
            }

            if (valid) {
                const area = a.combinedArea(b);
                if (area > largestArea) largestArea = area;
            }
        }
    }

    return core.toString(usize, &allocator, largestArea);
}
