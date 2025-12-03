const std = @import("std");

const core = @import("../core.zig");

pub fn solve(part: core.Part, input: []const u8) ?[*]u8 {
    return switch (part) {
        .Part1 => solvePartOne(input),
        .Part2 => solvePartTwo(input),
    };
}

pub fn solvePartOne(input: []const u8) ?[*]u8 {
    var banks = std.mem.tokenizeScalar(u8, input, '\n');

    var sum: u64 = 0;
    while (banks.next()) |bank| {
        var largest: u8 = 0;
        var x: usize = 0;

        for (bank[0 .. bank.len - 1], 0..) |b, i| {
            const battery = b - '0';
            if (battery > largest) {
                largest = battery;
                x = i;
            }
        }

        var secondLargest: u8 = 0;

        for (bank[x + 1 ..], 0..) |b, i| {
            _ = i;
            const battery = b - '0';
            if (battery > secondLargest) secondLargest = battery;
        }

        const joltage = (largest * 10) + secondLargest;

        sum += joltage;
    }

    return core.toString(u64, &std.heap.page_allocator, sum);
}

pub fn solvePartTwo(input: []const u8) ?[*]u8 {
    var banks = std.mem.tokenizeScalar(u8, input, '\n');

    var sum: u64 = 0;
    while (banks.next()) |bank| {
        var joltage: u64 = 0;

        var previousIndex: usize = 0;
        var count: usize = 12;
        while (count > 0) : (count -= 1) {
            var largest: u8 = 0;
            var foundAt: usize = 0;
            for (bank[previousIndex .. bank.len - (count - 1)], 0..) |b, i| {
                const battery = b - '0';
                if (battery > largest) {
                    largest = battery;
                    foundAt = i;
                }
            }

            if (joltage > 0) joltage *= 10;
            joltage += largest;
            previousIndex += foundAt + 1;
        }

        sum += joltage;
    }

    return core.toString(u64, &std.heap.page_allocator, sum);
}
