global using AdventOfCode.Solutions.Rust;
global using AdventOfCode.Solutions.Zig;
global using System;
global using System.Collections;
global using System.Collections.Generic;
global using System.Linq;
global using static AdventOfCode.Solutions.Utils.CalculationUtils;
global using static AdventOfCode.Solutions.Utils.CollectionUtils;
global using static AdventOfCode.Solutions.Utils.StringUtils;
using System.Diagnostics;
using System.Net;

namespace AdventOfCode.Solutions;

public abstract class SolutionBase
{
    public int Day { get; }
    public int Year { get; }
    public bool Debug { get; set; }
    public SolutionTarget[] Targets { get; set; }

    public string Title => LoadTitle(Debug);
    public string Input => LoadInput(Debug);
    public string DebugInput => LoadInput(true);

    private readonly string _titleOverride;

    private protected SolutionBase(int day, int year, string title, bool useDebugInput = false, SolutionTarget[]? targets = null)
    {
        Day = day;
        Year = year;
        Debug = useDebugInput;
        Targets = targets ?? [];

        _titleOverride = title;
    }

    public SolutionResult? Solve(int part = 1, SolutionTarget target = SolutionTarget.CSharp)
    {
        if (part is not 1 and not 2)
        {
            throw new InvalidOperationException("Invalid part param supplied.");
        }

        if (Debug)
        {
            if (string.IsNullOrEmpty(DebugInput))
            {
                throw new Exception("DebugInput is null or empty");
            }
        }
        else if (string.IsNullOrEmpty(Input))
        {
            throw new Exception("Input is null or empty");
        }

        (string? answer, TimeSpan duration) SolveCSharp()
        {
            var then = DateTime.Now;
            var answer = part == 1 ? SolvePartOne() : SolvePartTwo();
            var now = DateTime.Now;
            var duration = now - then;

            return (answer, duration);
        }

        (string? answer, TimeSpan? duration) SolveRust()
        {
            var answer = RustSolver.Solve(Year, Day, part, Input);
            var duration = RustSolver.GetSolveDuration(Year, Day, part);

            return (answer, duration);

        }

        (string? answer, TimeSpan duration) SolveZig()
        {
            var answer = ZigSolver.Solve(Year, Day, part, Input);
            var duration = TimeSpan.MaxValue;

            return (answer, duration);
        }

        try
        {
            var (answer, duration) = target switch
            {
                SolutionTarget.CSharp => SolveCSharp(),
                SolutionTarget.Rust => SolveRust(),
                SolutionTarget.Zig => SolveZig(),
                _ => throw new NotImplementedException(),
            };

            return string.IsNullOrEmpty(answer)
                ? null
                : new SolutionResult
                {
                    Answer = answer,
                    Duration = duration,
                    Target = target,
                };
        }
        catch (Exception)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
                return null;
            }
            else
            {
                throw;
            }
        }
    }

    private string InputsDirectory => $"./inputs/y{Year}/d{Day:D2}";

    string LoadTitle(bool debug = false)
    {
        if (!string.IsNullOrEmpty(_titleOverride))
        {
            return _titleOverride;
        }

        var titleFilePath = $"{InputsDirectory}/title";
        var file = new FileInfo(titleFilePath);
        if (File.Exists(titleFilePath) && file.Length > 0)
        {
            return File.ReadAllText(titleFilePath);
        }

        if (debug) return "";

        try
        {
            var title = InputService.FetchTitle(Year, Day).Result;
            file.Directory?.Create();
            File.WriteAllText(titleFilePath, title);
            return title;
        }
        catch (InvalidOperationException)
        {
            var colour = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Day {Day}: Cannot fetch puzzle input before given date (Eastern Standard Time).");
            Console.ForegroundColor = colour;
        }

        return "";
    }

    string LoadInput(bool debug = false)
    {
        var inputFilepath = $"{InputsDirectory}/{(debug ? "debug" : "input")}";
        var file = new FileInfo(inputFilepath);

        if (File.Exists(inputFilepath) && file.Length > 0)
        {
            return File.ReadAllText(inputFilepath);
        }

        if (debug) return "";

        try
        {
            var input = InputService.FetchInput(Year, Day).Result;
            file.Directory?.Create();
            File.WriteAllText(inputFilepath, input);
            return input;
        }
        catch (HttpRequestException e)
        {
            var code = e.StatusCode;
            var colour = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (code == HttpStatusCode.BadRequest)
            {
                Console.WriteLine($"Day {Day}: Received 400 when attempting to retrieve puzzle input. Your session cookie is probably not recognized.");

            }
            else if (code == HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Day {Day}: Received 404 when attempting to retrieve puzzle input. The puzzle is probably not available yet.");
            }
            else
            {
                Console.ForegroundColor = colour;
                Console.WriteLine(e.ToString());
            }
            Console.ForegroundColor = colour;
        }
        catch (InvalidOperationException)
        {
            var colour = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Day {Day}: Cannot fetch puzzle input before given date (Eastern Standard Time).");
            Console.ForegroundColor = colour;
        }

        return "";
    }

    public override string ToString() =>
        $"\n--- Day {Day}: {Title} --- {(Debug ? "!! Debug mode active, using DebugInput !!" : "")}\n"
        + PartToString(1) + '\n'
        + PartToString(2);

    private string PartToString(int part)
    {
        var header = $" - Part{part} => ";
        try
        {
            var results = Targets.Select(t => Solve(part, t)).OfType<SolutionResult>().ToArray();
            return header + (
                results.Length == 0
                ? "Unsolved"
                : string.Join("\n".PadRight(header.Length + 1), results)
            );
        }
        catch (Exception e)
        {
            return header + $"Exception: {e.Message}";
        }
    }

    protected abstract string? SolvePartOne();
    protected abstract string? SolvePartTwo();
}
