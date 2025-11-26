const std = @import("std");

const core = @import("core.zig");
const y2024 = @import("y2024/root.zig");
const y2025 = @import("y2025/root.zig");

export fn solve(year: u16, day: u8, part: core.Part, ptr: [*:0]const u8) callconv(.c) ?[*]u8 {
    const input = std.mem.sliceTo(ptr, 0);

    return switch (year) {
        2024 => y2024.solve(day, part, input),
        2025 => y2025.solve(day, part, input),
        else => null,
    };
}

// export fn free_string(ptr: [*:0]const u8) callconv(.c) void {
//     std.heap.c_allocator.free(ptr);
// }
