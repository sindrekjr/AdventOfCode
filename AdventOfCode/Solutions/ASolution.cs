using System;
using System.Collections.Generic;
using System.Diagnostics;
using AdventOfCode.Infrastructure.Exceptions;
using AdventOfCode.Infrastructure.Helpers;
using AdventOfCode.Infrastructure.Models;

namespace AdventOfCode.Solutions
{
    abstract class ASolution
    {
        Lazy<string> _input, _debugInput;
        Lazy<SolutionResult> _part1, _part2;

        public int Day { get; }
        public int Year { get; }
        public string Title { get; }
        public string Input => Debug ? DebugInput : _input.Value ?? null;
        public SolutionResult Part1 => _part1.Value;
        public SolutionResult Part2 => _part2.Value;

        public string DebugInput => _debugInput.Value ?? null;
        public bool Debug { get; set; }

        private protected ASolution(int day, int year, string title, bool useDebugInput = false)
        {
            Day = day;
            Year = year;
            Title = title;
            Debug = useDebugInput;
            _input = new Lazy<string>(() => InputHelper.LoadInput(Year, Day));
            _debugInput = new Lazy<string>(() => InputHelper.LoadDebugInput(Year, Day));
            _part1 = new Lazy<SolutionResult>(() => Solver(SolvePartOne));
            _part2 = new Lazy<SolutionResult>(() => Solver(SolvePartTwo));
        }

        public IEnumerable<SolutionResult> Solve(int part = 0)
        {
            if (part != 2 && (part == 1 || !string.IsNullOrEmpty(Part1.Answer)))
            {
                yield return Part1;
            }

            if (part != 1 && (part == 2 || !string.IsNullOrEmpty(Part2.Answer)))
            {
                yield return Part2;
            }
        }

        public override string ToString()
            => $"{FormatHelper.FormatTitle(Day, Title)}\n"
                + (Debug ? FormatHelper.FormatDebug(DebugInput) + "\n" : "")
                + $"{FormatHelper.FormatPart(1, Part1)}\n"
                + $"{FormatHelper.FormatPart(2, Part2)}\n";

        SolutionResult Solver(Func<string> SolverFunction)
        {
            if (Debug)
            {
                if (string.IsNullOrEmpty(DebugInput))
                {
                    throw new InputException("DebugInput is null or empty");
                }
            }
            else if (string.IsNullOrEmpty(Input))
            {
                throw new InputException("Input is null or empty");
            }

            try
            {
                var then = DateTime.Now;
                var result = SolverFunction();
                var now = DateTime.Now;
                return string.IsNullOrEmpty(result) ? new SolutionResult() : new SolutionResult { Answer = result, Time = now - then };
            }
            catch (Exception)
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                    return new SolutionResult();
                }
                else
                {
                    throw;
                }
            }
        }

        protected abstract string SolvePartOne();
        protected abstract string SolvePartTwo();
    }
}
