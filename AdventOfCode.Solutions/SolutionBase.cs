﻿global using AdventOfCode.Solutions.Rust;
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
    public string Title { get; }
    public bool Debug { get; set; }
    public string Input => LoadInput(Debug);
    public string DebugInput => LoadInput(true);

    public SolutionResult Part1 => Solve(1);
    public SolutionResult Part2 => Solve(2);

    private protected SolutionBase(int day, int year, string title, bool useDebugInput = false)
    {
        Day = day;
        Year = year;
        Title = title;
        Debug = useDebugInput;
    }

    public SolutionResult Solve(int part = 1)
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

        try
        {
            var then = DateTime.Now;
            var result = part == 1 ? SolvePartOne() : SolvePartTwo();
            var now = DateTime.Now;

            return string.IsNullOrEmpty(result)
                ? SolutionResult.Empty
                : new SolutionResult
                {
                    Answer = result,
                    Duration = RustSolver.GetSolveDuration(Year, Day, part) ?? now - then
                };
        }
        catch (Exception)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
                return SolutionResult.Empty;
            }
            else
            {
                throw;
            }
        }
    }

    string LoadInput(bool debug = false)
    {
        var inputFilepath =
            $"./AdventOfCode.Solutions/Year{Year}/Day{Day:D2}/{(debug ? "debug" : "input")}";

        if (File.Exists(inputFilepath) && new FileInfo(inputFilepath).Length > 0)
        {
            return File.ReadAllText(inputFilepath);
        }

        if (debug) return "";

        try
        {
            var input = InputService.FetchInput(Year, Day).Result;
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
        + $" - Part1 => {Part1}\n"
        + $" - Part2 => {Part2}";

    protected abstract string SolvePartOne();
    protected abstract string SolvePartTwo();
}
