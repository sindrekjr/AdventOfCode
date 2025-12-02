const std = @import("std");

const core = @import("../core.zig");

pub fn solve(part: core.Part, input: []const u8) ?[*]u8 {
    return switch (part) {
        .Part1 => solvePartOne(input),
        .Part2 => solvePartTwo(input),
    };
}

pub fn solvePartOne(input: []const u8) ?[*]u8 {
    var lines = std.mem.tokenizeScalar(u8, input, '\n');

    var pass: u16 = 0;
    var dial: i16 = 50;
    var last_direction: ?bool = null;
    while (lines.next()) |line| {
        const direction: bool = line[0] == 'R';
        if (direction != last_direction) {
            dial = 100 - dial;
            last_direction = direction;
        }

        dial += std.fmt.parseUnsigned(i16, line[1..], 10) catch return null;
        dial = @mod(dial, 100);

        if (dial == 0) {
            pass += 1;
        }
    }

    return core.toString(u16, &std.heap.page_allocator, pass);
}

pub fn solvePartTwo(input: []const u8) ?[*]u8 {
    var lines = std.mem.tokenizeScalar(u8, input, '\n');

    var pass: u16 = 0;
    var dial: i16 = 50;
    var last_direction: ?bool = null;
    while (lines.next()) |line| {
        const direction: bool = line[0] == 'R';
        dial = @mod(if (direction != last_direction) 100 - dial else dial, 100);
        last_direction = direction;

        dial += std.fmt.parseUnsigned(i16, line[1..], 10) catch return null;
        pass += @divFloor(@abs(dial), 100);
        dial = @mod(dial, 100);
    }

    return core.toString(u16, &std.heap.page_allocator, pass);
}
