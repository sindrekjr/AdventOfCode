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
    var lines = std.mem.tokenizeScalar(u8, input, '\n');

    var numbers: std.ArrayList([]const u64) = .empty;
    defer numbers.deinit(allocator);

    var i: usize = 0;
    var result: u64 = 0;
    while (lines.next()) |line| {
        var items = std.mem.tokenizeScalar(u8, line, ' ');

        var row: std.ArrayList(u64) = .empty;
        while (items.next()) |item| {
            if (item.len == 0) continue;
            switch (item[0]) {
                '*' => {
                    var product: u64 = 1;
                    for (numbers.items) |prev_row| {
                        product *= prev_row[i];
                    }
                    result += product;
                    i += 1;
                },
                '+' => {
                    var sum: u64 = 0;
                    for (numbers.items) |prev_row| {
                        sum += prev_row[i];
                    }
                    result += sum;
                    i += 1;
                },
                else => {
                    const n = std.fmt.parseUnsigned(u16, item, 10) catch unreachable;
                    row.append(allocator, n) catch unreachable;
                },
            }
        }
        numbers.append(allocator, row.items[0..]) catch unreachable;
    }

    return core.toString(u64, &allocator, result);
}

pub fn solvePartTwo(input: []const u8) ?[*]u8 {
    const allocator = std.heap.page_allocator;
    var lines = std.mem.tokenizeScalar(u8, input, '\n');

    var rows: std.ArrayList([]const u8) = .empty;
    defer rows.deinit(allocator);

    var sum: u64 = 0;
    while (lines.next()) |line| {
        if (line[0] != '*' and line[0] != '+') {
            rows.append(allocator, line) catch unreachable;
            continue;
        }

        for (line, 0..) |op, i| {
            if (op == ' ') continue;
            if (op == '\n') break;

            const start = i;
            const end = std.mem.indexOfAny(u8, line[i + 1 ..], &[_]u8{ '*', '+' });
            const length = if (end != null) end.? else line[i..].len;

            var result: u64 = 0;
            for (start..start + length) |col| {
                var str: std.ArrayList(u8) = .empty;
                for (rows.items) |numbers| {
                    str.append(allocator, numbers[col]) catch unreachable;
                }
                const n = std.fmt.parseUnsigned(u64, std.mem.trim(u8, str.items, " "), 10) catch unreachable;

                if (op == '*') {
                    result = @max(1, result) * n;
                } else {
                    result += n;
                }
            }

            sum += result;
        }
    }

    return core.toString(u64, &allocator, sum);
}
