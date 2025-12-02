const std = @import("std");

const core = @import("../core.zig");

pub fn solve(part: core.Part, input: []const u8) ?[*]u8 {
    return switch (part) {
        .Part1 => solvePartOne(input),
        .Part2 => solvePartTwo(input),
    };
}

pub fn solvePartOne(input: []const u8) ?[*]u8 {
    var seqs = std.mem.tokenizeScalar(u8, std.mem.trim(u8, input, "\n"), ',');

    var sum: u64 = 0;
    while (seqs.next()) |seq| {
        var parts = std.mem.splitScalar(u8, seq, '-');

        const start_str = parts.next().?;
        const start_len = start_str.len;
        const start = std.fmt.parseUnsigned(u64, start_str, 10) catch unreachable;

        const end_str = parts.next().?;
        const end_len = end_str.len;
        const end = std.fmt.parseUnsigned(u64, end_str, 10) catch unreachable;

        var half = if (start_len % 2 == 0)
            start / std.math.pow(u64, 10, start_len / 2)
        else
            end / std.math.pow(u64, 10, end_len - 1);

        while (true) : (half += 1) {
            var half_len: usize = 1;
            var h = half;
            while (h >= 10) : (h /= 10) half_len += 1;

            const n = half * std.math.pow(u64, 10, half_len) + half;
            if (n >= start and n <= end) {
                sum += n;
            } else if (n > end) {
                break;
            }
        }
    }

    return core.toString(u64, &std.heap.page_allocator, sum);
}

pub fn solvePartTwo(input: []const u8) ?[*]u8 {
    _ = input;
    return null;
}
