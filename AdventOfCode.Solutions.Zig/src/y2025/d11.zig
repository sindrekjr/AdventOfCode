const std = @import("std");

const core = @import("../core.zig");

pub fn solve(part: core.Part, input: []const u8) ?[*]u8 {
    return switch (part) {
        .Part1 => solvePartOne(input),
        .Part2 => solvePartTwo(input),
    };
}

fn recursiveMemoSearch(machine: []const u8, goal: []const u8, machines: std.StringHashMap(std.ArrayList([]const u8)), memo: *std.StringHashMap(usize)) usize {
    var paths: usize = 0;

    const outputs = machines.get(machine) orelse return 0;
    for (outputs.items) |output| {
        if (std.mem.eql(u8, output, goal)) {
            paths += 1;
            continue;
        }

        if (memo.get(output)) |m| {
            paths += m;
            continue;
        }

        const sub_paths = recursiveMemoSearch(output, goal, machines, memo);
        memo.put(output, sub_paths) catch unreachable;
        paths += sub_paths;
    }

    return paths;
}

pub fn solvePartOne(input: []const u8) ?[*]u8 {
    const allocator = std.heap.page_allocator;

    var machines = std.StringHashMap(std.ArrayList([]const u8)).init(allocator);
    defer machines.deinit();

    var lines = std.mem.tokenizeScalar(u8, input, '\n');
    while (lines.next()) |line| {
        var tokens = std.mem.tokenizeScalar(u8, line, ' ');
        const machine = std.mem.trimEnd(u8, tokens.next().?, ":");

        var outputs = std.ArrayList([]const u8).empty;
        while (tokens.next()) |o| {
            outputs.append(allocator, o) catch unreachable;
        }

        machines.put(machine, outputs) catch unreachable;
    }

    var memo = std.StringHashMap(usize).init(allocator);
    defer memo.deinit();

    const paths = recursiveMemoSearch("you", "out", machines, &memo);

    return core.toString(usize, &allocator, paths);
}

pub fn solvePartTwo(input: []const u8) ?[*]u8 {
    const allocator = std.heap.page_allocator;

    var machines = std.StringHashMap(std.ArrayList([]const u8)).init(allocator);
    defer machines.deinit();

    var lines = std.mem.tokenizeScalar(u8, input, '\n');
    while (lines.next()) |line| {
        var tokens = std.mem.tokenizeScalar(u8, line, ' ');
        const machine = std.mem.trimEnd(u8, tokens.next().?, ":");

        var outputs = std.ArrayList([]const u8).empty;
        while (tokens.next()) |o| {
            outputs.append(allocator, o) catch unreachable;
        }

        machines.put(machine, outputs) catch unreachable;
    }

    var memo = std.StringHashMap(usize).init(allocator);
    defer memo.deinit();

    const fft_dac = recursiveMemoSearch("fft", "dac", machines, &memo);
    memo.clearAndFree();
    const svr_fft = recursiveMemoSearch("svr", "fft", machines, &memo);
    memo.clearAndFree();
    const dac_out = recursiveMemoSearch("dac", "out", machines, &memo);

    const paths = svr_fft * fft_dac * dac_out;

    return core.toString(usize, &allocator, paths);
}
