const std = @import("std");

const core = @import("../core.zig");

pub fn solve(part: core.Part, input: []const u8) ?[*]u8 {
    return switch (part) {
        .Part1 => solvePartOne(input),
        .Part2 => solvePartTwo(input),
    };
}

const Range = struct {
    start: usize,
    end: usize,
};

pub fn solvePartOne(input: []const u8) ?[*]u8 {
    const allocator = std.heap.page_allocator;
    var lines = std.mem.splitScalar(u8, std.mem.trimEnd(u8, input, "\n"), '\n');

    var ranges: std.ArrayList(Range) = .empty;
    defer ranges.deinit(allocator);
    while (lines.next()) |line| {
        if (line.len <= 0) break;
        var range = std.mem.splitScalar(u8, line, '-');
        const start = std.fmt.parseUnsigned(usize, range.next().?, 10) catch unreachable;
        const end = std.fmt.parseUnsigned(usize, range.next().?, 10) catch unreachable;
        const newRange: Range = Range{ .start = start, .end = end };
        ranges.append(allocator, newRange) catch unreachable;
    }

    var fresh: usize = 0;
    while (lines.next()) |line| {
        const ingredient = std.fmt.parseUnsigned(usize, line, 10) catch unreachable;
        for (ranges.items) |range| {
            if (ingredient >= range.start and ingredient <= range.end) {
                fresh += 1;
                break;
            }
        }
    }

    return core.toString(usize, &std.heap.page_allocator, fresh);
}

pub fn solvePartTwo(input: []const u8) ?[*]u8 {
    _ = input;
    return null;
}
