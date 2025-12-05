const std = @import("std");

const core = @import("../core.zig");

pub fn solve(part: core.Part, input: []const u8) ?[*]u8 {
    return switch (part) {
        .Part1 => solvePartOne(input),
        .Part2 => solvePartTwo(input),
    };
}

pub fn solvePartOne(input: []const u8) ?[*]u8 {
    const width: isize = @intCast(std.mem.indexOf(u8, input, "\n").?);
    const size = std.mem.replacementSize(u8, input, "\n", "");
    const oneline = std.heap.page_allocator.alloc(u8, size) catch unreachable;
    _ = std.mem.replace(u8, input, "\n", "", oneline);

    const directions = [_]isize{ -width - 1, -width, -width + 1, -1, 1, width - 1, width, width + 1 };

    var sum: u32 = 0;
    for (oneline, 0..) |ch, i| {
        if (ch != '@') continue;

        var adjacent: u8 = 0;
        for (directions) |dir| {
            const pos = @as(isize, @intCast(i)) + dir;
            if (@rem(@as(isize, @intCast(i)), width) == 0 and (dir == -1 or dir == -width - 1 or dir == width - 1)) continue;
            if (@rem(@as(isize, @intCast(i)), width) == width - 1 and (dir == 1 or dir == width + 1 or dir == -width + 1)) continue;
            if (pos < 0 or pos >= size) continue;

            const adj_ch = oneline[@intCast(pos)];
            if (adj_ch == '@') {
                adjacent += 1;
            }
            if (adjacent >= 4) break;
        }

        if (adjacent < 4) {
            sum += 1;
        }
    }

    return core.toString(u32, &std.heap.page_allocator, sum);
}

pub fn solvePartTwo(input: []const u8) ?[*]u8 {
    const width: isize = @intCast(std.mem.indexOf(u8, input, "\n").?);
    const size = std.mem.replacementSize(u8, input, "\n", "");
    const oneline = std.heap.page_allocator.alloc(u8, size) catch unreachable;
    _ = std.mem.replace(u8, input, "\n", "", oneline);

    const directions = [_]isize{ -width - 1, -width, -width + 1, -1, 1, width - 1, width, width + 1 };

    var sum: u32 = 0;
    while (true) {
        const previous = sum;

        for (oneline, 0..) |ch, i| {
            if (ch != '@') continue;

            var adjacent: u8 = 0;
            for (directions) |dir| {
                const pos = @as(isize, @intCast(i)) + dir;
                if (@rem(@as(isize, @intCast(i)), width) == 0 and (dir == -1 or dir == -width - 1 or dir == width - 1)) continue;
                if (@rem(@as(isize, @intCast(i)), width) == width - 1 and (dir == 1 or dir == width + 1 or dir == -width + 1)) continue;
                if (pos < 0 or pos >= size) continue;

                const adj_ch = oneline[@intCast(pos)];
                if (adj_ch == '@') {
                    adjacent += 1;
                }
                if (adjacent >= 4) break;
            }

            if (adjacent < 4) {
                oneline[i] = '.';
                sum += 1;
            }
        }

        if (previous == sum) break;
    }

    return core.toString(u32, &std.heap.page_allocator, sum);
}
