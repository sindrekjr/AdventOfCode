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
        13 => @import("d13.zig").solve(part, input),
        14 => @import("d14.zig").solve(part, input),
        15 => @import("d15.zig").solve(part, input),
        16 => @import("d16.zig").solve(part, input),
        17 => @import("d17.zig").solve(part, input),
        18 => @import("d18.zig").solve(part, input),
        19 => @import("d19.zig").solve(part, input),
        20 => @import("d20.zig").solve(part, input),
        21 => @import("d21.zig").solve(part, input),
        22 => @import("d22.zig").solve(part, input),
        23 => @import("d23.zig").solve(part, input),
        24 => @import("d24.zig").solve(part, input),
        25 => @import("d25.zig").solve(part, input),
        else => null,
    };
}
