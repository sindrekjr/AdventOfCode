const std = @import("std");

const core = @import("../core.zig");

pub fn solve(part: core.Part, input: []const u8) ?[*]u8 {
    return switch (part) {
        .Part1 => solvePartOne(input),
        .Part2 => solvePartTwo(input),
    };
}

fn recursiveMemoSearch(machine: []const u8, machines: std.StringHashMap(std.ArrayList([]const u8)), memo: *std.StringHashMap(usize)) usize {
    var paths: usize = 0;

    const outputs = machines.get(machine).?;
    for (outputs.items) |output| {
        if (std.mem.eql(u8, output, "out")) {
            paths += 1;
            continue;
        }

        if (memo.get(output)) |m| {
            paths += m;
            continue;
        }

        const sub_paths = recursiveMemoSearch(output, machines, memo);
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

    const paths = recursiveMemoSearch("you", machines, &memo);

    return core.toString(usize, &allocator, paths);
}

fn recursiveMemoSearchPart2(allocator: std.mem.Allocator, machine: []const u8, goal: []const u8, machines: std.StringHashMap(std.ArrayList([]const u8)), memo: *std.StringHashMap(std.ArrayList([]const u8))) std.ArrayList([]const u8) {
    var paths = std.ArrayList([]const u8).empty;

    const outputs = machines.get(machine) orelse return paths;
    for (outputs.items) |output| {
        if (std.mem.eql(u8, output, goal)) {
            var path = std.ArrayList(u8).empty;
            defer path.deinit(allocator);
            path.appendSlice(allocator, output) catch unreachable;
            paths.append(allocator, path.toOwnedSlice(allocator) catch unreachable) catch unreachable;
            continue;
        }

        if (memo.get(output)) |m| {
            for (m.items) |known_path| {
                var path = std.ArrayList(u8).empty;
                defer path.deinit(allocator);
                path.appendSlice(allocator, output) catch unreachable;
                path.appendSlice(allocator, known_path) catch unreachable;
                paths.append(allocator, path.toOwnedSlice(allocator) catch unreachable) catch unreachable;
            }
            continue;
        }

        const sub_paths = recursiveMemoSearchPart2(allocator, output, goal, machines, memo);
        memo.put(output, sub_paths) catch unreachable;

        for (sub_paths.items) |sub_path| {
            var path = std.ArrayList(u8).empty;
            defer path.deinit(allocator);
            path.appendSlice(allocator, output) catch unreachable;
            path.appendSlice(allocator, sub_path) catch unreachable;
            paths.append(allocator, path.toOwnedSlice(allocator) catch unreachable) catch unreachable;
        }
    }

    memo.put(machine, paths) catch unreachable;

    return paths;
}

pub fn solvePartTwo(input: []const u8) ?[*]u8 {
    // const allocator = std.heap.page_allocator;

    // var machines = std.StringHashMap(std.ArrayList([]const u8)).init(allocator);
    // defer machines.deinit();

    // var lines = std.mem.tokenizeScalar(u8, input, '\n');
    // while (lines.next()) |line| {
    //     var tokens = std.mem.tokenizeScalar(u8, line, ' ');
    //     const machine = std.mem.trimEnd(u8, tokens.next().?, ":");

    //     var outputs = std.ArrayList([]const u8).empty;
    //     while (tokens.next()) |o| {
    //         outputs.append(allocator, o) catch unreachable;
    //     }

    //     machines.put(machine, outputs) catch unreachable;
    // }

    // var memo = std.StringHashMap(std.ArrayList([]const u8)).init(allocator);
    // defer memo.deinit();

    // const paths = recursiveMemoSearchPart2(allocator, "fft", "dac", machines, &memo);

    // var count: usize = 0;
    // for (paths.items) |path| {
    //     std.debug.print("Path: {s}\n", .{path});
    //     if (std.mem.indexOf(u8, path, "fft") != null and std.mem.indexOf(u8, path, "dac") != null) {
    //         count += 1;
    //     }
    // }

    // return core.toString(usize, &allocator, count);

    _ = input;
    return null;
}
