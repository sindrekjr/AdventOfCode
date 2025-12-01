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
    var dial: u16 = 50;
    while (lines.next()) |line| {
        const direction = line[0..1][0];

        var clicks: u16 = 0;
        for (line[1..]) |ch| {
            const digit = ch - '0';
            clicks = clicks * 10 + digit;
        }

        if (direction == 'R') {
            dial += clicks;
        } else {
            if (clicks > dial) {
                const diff = (clicks - dial) % 100;
                dial = 100 - diff;
            } else {
                dial -= clicks;
            }
        }

        dial %= 100;

        if (dial == 0) {
            pass += 1;
        }
    }

    return core.toString(u16, &std.heap.page_allocator, pass);
}

pub fn solvePartTwo(input: []const u8) ?[*]u8 {
    var lines = std.mem.tokenizeScalar(u8, input, '\n');

    var pass: u16 = 0;
    var dial: u16 = 50;
    while (lines.next()) |line| {
        const direction = line[0..1][0];

        var clicks: u16 = 0;
        for (line[1..]) |ch| {
            const digit = ch - '0';
            clicks = clicks * 10 + digit;
        }

        if (clicks >= 100) {
            pass += clicks / 100;
            clicks %= 100;
        }

        if (direction == 'R') {
            const diff = dial + clicks;
            if (diff >= 100 and dial != 100) {
                pass += 1;
            }

            dial = (dial + clicks) % 100;
        } else {
            if (clicks >= dial) {
                if (dial != 0) {
                    pass += 1;
                }
                const diff = (clicks - dial);
                dial = 100 - (diff % 100);
            } else {
                dial -= clicks;
            }
        }
    }

    return core.toString(u16, &std.heap.page_allocator, pass);
}
