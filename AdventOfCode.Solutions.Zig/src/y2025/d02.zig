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
    const allocator = std.heap.page_allocator;

    var ranges: std.ArrayList([2]usize) = .empty;
    defer ranges.deinit(allocator);

    var min_len: usize = std.math.maxInt(usize);
    var max_len: usize = 0;

    var lines = std.mem.tokenizeScalar(u8, std.mem.trim(u8, input, "\n"), ',');
    while (lines.next()) |line| {
        if (line.len == 0) continue;
        var split = std.mem.splitScalar(u8, line, '-');

        const start_str = split.next().?;
        const start_len = start_str.len;
        const start = std.fmt.parseUnsigned(usize, start_str, 10) catch unreachable;
        if (start_len < min_len) min_len = start_len;

        const end_str = split.next().?;
        const end_len = end_str.len;
        const end = std.fmt.parseUnsigned(usize, end_str, 10) catch unreachable;
        if (end_len > max_len) max_len = end_len;

        ranges.append(allocator, [_]usize{ start, end }) catch unreachable;
    }

    var sum: usize = 0;
    var checked: std.AutoHashMap(usize, bool) = std.AutoHashMap(usize, bool).init(allocator);
    defer checked.deinit();
    for (min_len..max_len + 1) |length| {
        for (1..(length / 2) + 1) |part_length| {
            if (length % part_length != 0) continue;
            const repetitions = length / part_length;

            const start = std.math.pow(usize, 10, part_length - 1);
            const end = std.math.pow(usize, 10, part_length);

            for (start..end) |partition| {
                var n = partition;
                for (0..repetitions - 1) |_| n = (n * std.math.pow(usize, 10, part_length)) + partition;
                if (checked.contains(n)) continue;

                checked.put(n, true) catch unreachable;
                for (ranges.items) |range| {
                    if (range[0] <= n and n <= range[1]) {
                        sum += n;
                        break;
                    }
                }
            }
        }
    }

    return core.toString(usize, &allocator, sum);
}
