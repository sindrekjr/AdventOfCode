const std = @import("std");

const core = @import("../core.zig");

pub fn solve(day: u8, part: core.Part, input: []const u8) ?[*]u8 {
    return switch (day) {
        1 => @import("d01.zig").solve(part, input),
        2 => @import("d02.zig").solve(part, input),
        3 => @import("d03.zig").solve(part, input),
        4 => @import("d04.zig").solve(part, input),
        5 => @import("d05.zig").solve(part, input),
        6 => @import("d06.zig").solve(part, input),
        7 => @import("d07.zig").solve(part, input),
        8 => @import("d08.zig").solve(part, input),
        9 => @import("d09.zig").solve(part, input),
        10 => @import("d10.zig").solve(part, input),
        11 => @import("d11.zig").solve(part, input),
        12 => @import("d12.zig").solve(part, input),
        else => null,
    };
}
