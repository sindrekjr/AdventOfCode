const std = @import("std");

const core = @import("../core.zig");

pub fn solve(part: core.Part, input: []const u8) ?[*]u8 {
    return switch (part) {
        .Part1 => solvePartOne(input),
        .Part2 => solvePartTwo(input),
    };
}

pub fn solvePartOne(input: []const u8) ?[*]u8 {
    _ = input;
    return null;
}

pub fn solvePartTwo(input: []const u8) ?[*]u8 {
    _ = input;
    return null;
}
