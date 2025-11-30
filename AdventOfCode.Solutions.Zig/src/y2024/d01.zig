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

    var list1: std.ArrayList(i32) = .empty;
    defer list1.deinit(allocator);

    var list2: std.ArrayList(i32) = .empty;
    defer list2.deinit(allocator);

    var iter = std.mem.tokenizeScalar(u8, input, '\n');
    while (iter.next()) |line| {
        var parts = std.mem.tokenizeScalar(u8, line, ' ');

        if (parts.next()) |a| {
            if (parts.next()) |b| {
                list1.append(allocator, try parseI32(a)) catch return null;
                list2.append(allocator, try parseI32(b)) catch return null;
            }
        }
    }

    std.mem.sort(i32, list1.items, {}, comptime std.sort.asc(i32));
    std.mem.sort(i32, list2.items, {}, comptime std.sort.asc(i32));

    var sum: u32 = 0;
    const count = list1.items.len;
    var i: usize = 0;
    while (i < count) : (i += 1) {
        const a = list1.items[i];
        const b = list2.items[i];
        sum += @intCast(@abs(a - b));
    }

    return core.toString(u32, &allocator, sum);
}

pub fn solvePartTwo(input: []const u8) ?[*]u8 {
    _ = input;
    return null;
}

fn parseI32(value: []const u8) !i32 {
    var result: i32 = 0;

    for (value) |ch| {
        const digit = ch - '0';
        result = result * 10 + digit;
    }

    return result;
}
