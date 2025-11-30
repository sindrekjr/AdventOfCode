const std = @import("std");

const core = @import("core.zig");

const Key = struct {
    year: u16,
    day: u8,
    part: core.Part,
};

var durations: ?*std.AutoHashMap(Key, u64) = null;

var init_timer_once = std.once(initializeTimer);

fn initializeTimer() void {
    durations = std.heap.page_allocator.create(std.AutoHashMap(Key, u64)) catch return;
    durations.?.* = std.AutoHashMap(Key, u64).init(std.heap.page_allocator);
}

pub fn getDuration(year: u16, day: u8, part: core.Part) ?u64 {
    init_timer_once.call();

    if (durations) |map| {
        return map.get(.{ .year = year, .day = day, .part = part });
    } else return null;
}

pub fn setDuration(year: u16, day: u8, part: core.Part, duration: u64) void {
    init_timer_once.call();

    if (durations) |durations_map| {
        durations_map.put(Key{ .year = year, .day = day, .part = part }, duration) catch return;
    }
}
