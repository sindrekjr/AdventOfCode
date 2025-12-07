const std = @import("std");

const core = @import("../core.zig");

pub fn solve(part: core.Part, input: []const u8) ?[*]u8 {
    return switch (part) {
        .Part1 => solvePartOne(input),
        .Part2 => solvePartTwo(input),
    };
}

pub fn solvePartOne(input: []const u8) ?[*]u8 {
    const allocator = std.heap.page_allocator;
    var beams = std.AutoHashMap(usize, bool).init(allocator);
    defer beams.deinit();

    var lines = std.mem.tokenizeScalar(u8, input, '\n');
    var splits: u16 = 0;
    while (lines.next()) |line| {
        if (beams.count() == 0) {
            const start = std.mem.indexOf(u8, line, "S").?;
            beams.put(start, true) catch unreachable;
            continue;
        }

        var current = beams.iterator();
        var next = std.AutoHashMap(usize, bool).init(allocator);
        while (current.next()) |beam| {
            const i = beam.key_ptr.*;
            if (!beam.value_ptr.*) continue;

            if (line[i] == '^') {
                splits += 1;
                next.put(i - 1, true) catch unreachable;
                next.put(i + 1, true) catch unreachable;
            } else next.put(i, true) catch unreachable;
        }
        beams = next;
    }

    return core.toString(u16, &allocator, splits);
}

pub fn solvePartTwo(input: []const u8) ?[*]u8 {
    const allocator = std.heap.page_allocator;
    var beams = std.AutoHashMap(usize, usize).init(allocator);
    defer beams.deinit();

    var lines = std.mem.tokenizeScalar(u8, input, '\n');
    while (lines.next()) |line| {
        if (beams.count() == 0) {
            const start = std.mem.indexOf(u8, line, "S").?;
            beams.put(start, 1) catch unreachable;
            continue;
        }

        for (line, 0..) |ch, i| {
            if (ch != '^') continue;

            const paths = beams.get(i);
            if (paths == null or paths == 0) continue;

            const left = if (beams.contains(i - 1)) beams.get(i - 1).? + paths.? else paths.?;
            const right = if (beams.contains(i + 1)) beams.get(i + 1).? + paths.? else paths.?;
            beams.put(i - 1, left) catch unreachable;
            beams.put(i + 1, right) catch unreachable;
            beams.put(i, 0) catch unreachable;
        }
    }

    var paths: usize = 0;
    var positions = beams.iterator();
    while (positions.next()) |pos| {
        paths += pos.value_ptr.*;
    }

    return core.toString(usize, &allocator, paths);
}
