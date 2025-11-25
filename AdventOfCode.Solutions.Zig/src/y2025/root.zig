const std = @import("std");

const core = @import("../core.zig");

pub fn solve(day: u8, part: core.Part, input: []const u8) ?[*]u8 {
    return switch (day) {
        1 => @import("d01.zig").solve(part, input),
        else => std.debug.panic("Solutions for day {} not found", .{day}),
    };
}
