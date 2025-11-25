const std = @import("std");

pub const Part = enum(u8) {
    Part1 = 1,
    Part2 = 2,
};

pub fn toString(comptime T: type, allocator: *const std.mem.Allocator, value: T) ?[*]u8 {
    var buf: [128]u8 = undefined;
    const written = std.fmt.bufPrint(&buf, "{}", .{value}) catch return null;
    const out = allocator.alloc(u8, written.len + 1) catch return null;

    @memmove(out[0..written.len], written);
    out[written.len] = 0;

    return out.ptr;
}
