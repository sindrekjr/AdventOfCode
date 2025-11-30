const std = @import("std");

const core = @import("core.zig");
const timer = @import("timer.zig");
const y2024 = @import("y2024/root.zig");
const y2025 = @import("y2025/root.zig");

export fn solve(year: u16, day: u8, part: core.Part, ptr: [*:0]const u8) callconv(.c) ?[*]u8 {
    const input = std.mem.sliceTo(ptr, 0);

    const start_time = std.time.nanoTimestamp();
    const result = switch (year) {
        2024 => y2024.solve(day, part, input),
        2025 => y2025.solve(day, part, input),
        else => null,
    };
    const end_time = std.time.nanoTimestamp();

    if (result != null) {
        timer.setDuration(year, day, part, end_time - start_time);
    }

    return result;
}

export fn getDuration(year: u16, day: u8, part: core.Part) callconv(.c) ?[*]u8 {
    if (timer.getDuration(year, day, part)) |ns| {
        return core.toString(i128, &std.heap.page_allocator, ns);
    } else return null;
}
