using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;

namespace AdventOfCode.Solutions;

public struct SolutionResult
{
    public string Answer { get; set; }
    public TimeSpan? Duration { get; set; }
    public SolutionTarget Target { get; set; }

    public override readonly string ToString() => string.IsNullOrEmpty(Answer)
        ? $"Unsolved ({Target.GetType().GetMember(Target.ToString()).First().GetCustomAttribute<DisplayAttribute>()?.Name})"
        : $"{Answer} ({Target.GetType().GetMember(Target.ToString()).First().GetCustomAttribute<DisplayAttribute>()?.Name}{(Duration.HasValue ? $", {FormatTime(Duration.Value)}" : "")})";

    private static string FormatTime(TimeSpan time) =>
        time.Microseconds < 1 ? $"{time.Nanoseconds}ns" :
        time.TotalMilliseconds < 1 ? $"{time.TotalMicroseconds}µs" :
        time.TotalSeconds < 1 ? $"{time.TotalMilliseconds}ms" :
        $"{time.TotalSeconds}s";
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SolutionTarget
{
    [Display(Name = "C#")]
    [JsonStringEnumMemberName("C#")]
    CSharp,

    [Display(Name = "Rust")]
    Rust,

    [Display(Name = "Zig")]
    Zig
}
