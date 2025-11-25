const std = @import("std");

pub fn build(b: *std.Build) void {
    const target = b.standardTargetOptions(.{});
    const optimize = b.standardOptimizeOption(.{});

    const root_path = b.path("src/root.zig");
    const mod = b.addModule("aoc-solutions-zig", .{ .root_source_file = root_path, .target = target, .optimize = optimize });
    const lib = b.addLibrary(.{ .name = "AdventOfCode.Solutions.Zig", .root_module = mod, .linkage = .static });

    lib.linkLibC();
    b.installArtifact(lib);

    const tests = b.addTest(.{ .root_module = mod });
    const run_tests = b.addRunArtifact(tests);

    const test_step = b.step("test", "Run tests");
    test_step.dependOn(&run_tests.step);
}
