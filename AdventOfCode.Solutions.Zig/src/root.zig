const std = @import("std");

const core = @import("core.zig");
const y2025 = @import("y2025/root.zig");

export fn solve(year: u16, day: u8, part: core.Part, ptr: [*:0]const u8) callconv(.c) ?[*]u8 {
    const input = std.mem.sliceTo(ptr, 0);

    const result = switch (year) {
        2025 => y2025.solve(day, part, input),
        else => std.debug.panic("Solutions for year {} not found", .{year}),
    };

    return result;
}

// export fn free_string(ptr: [*:0]const u8) callconv(.c) void {
//     std.heap.c_allocator.free(ptr);
// }
