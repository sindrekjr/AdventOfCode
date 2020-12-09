using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace AdventOfCode.Solutions
{

    abstract class ASolution
    {
        Lazy<string> _input, _debugInput;
        Lazy<(string, TimeSpan)> _part1, _part2;

        public int Day { get; }
        public int Year { get; }
        public string Title { get; }
        public string Input => Debug && !string.IsNullOrEmpty(DebugInput) ? DebugInput : _input.Value ?? null;
        public (string answer, TimeSpan time) Part1 => _part1.Value;
        public (string answer, TimeSpan time) Part2 => _part2.Value;

        public string DebugInput => _debugInput.Value ?? null;
        public bool Debug { get; set; }

        private protected ASolution(int day, int year, string title, bool useDebugInput = false)
        {
            Day = day;
            Year = year;
            Title = title;
            Debug = useDebugInput;
            _input = new Lazy<string>(LoadInput);
            _debugInput = new Lazy<string>(LoadDebugInput);
            _part1 = new Lazy<(string, TimeSpan)>(() => Solver(SolvePartOne));
            _part2 = new Lazy<(string, TimeSpan)>(() => Solver(SolvePartTwo));
        }

        public void Solve(int part = 0)
        {
            if(Input == null) return;

            Console.WriteLine();
            Console.WriteLine(FormatTitle());

            if(Debug)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(string.IsNullOrEmpty(DebugInput)
                    ? "!! Debug mode active with no DebugInput defined"
                    : "!! Debugmode active, using DebugInput");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            if(part != 2)
            {
                var (answer, time) = Part1;
                if (part == 1 || !string.IsNullOrEmpty(answer)) Console.WriteLine(FormatPart(1, answer, time));
            }

            if(part != 1)
            {
                var (answer, time) = Part2;
                if (part == 2 || !string.IsNullOrEmpty(answer)) Console.WriteLine(FormatPart(2, answer, time));
            }
        }

        string FormatTitle() => $"Day {Day}: {Title}";

        string FormatPart(int part, string answer, TimeSpan time)
            => $"  - Part{part} => " + (string.IsNullOrEmpty(answer) ? "Unsolved" : $"{answer} ({time.TotalMilliseconds}ms)");

        string LoadInput()
        {
            string INPUT_FILEPATH = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $"../../../Solutions/Year{Year}/Day{Day.ToString("D2")}/input"));
            string INPUT_URL = $"https://adventofcode.com/{Year}/day/{Day}/input";
            string input = "";

            if(File.Exists(INPUT_FILEPATH) && new FileInfo(INPUT_FILEPATH).Length > 0)
            {
                input = File.ReadAllText(INPUT_FILEPATH);
            }
            else
            {
                try
                {
                    DateTime CURRENT_EST = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc).AddHours(-5);
                    if(CURRENT_EST < new DateTime(Year, 12, Day)) throw new InvalidOperationException();

                    using(var client = new WebClient())
                    {
                        client.Headers.Add(HttpRequestHeader.Cookie, Program.Config.Cookie);
                        input = client.DownloadString(INPUT_URL).Trim();
                        File.WriteAllText(INPUT_FILEPATH, input);
                    }
                }
                catch(WebException e)
                {
                    var statusCode = ((HttpWebResponse)e.Response).StatusCode;
                    if(statusCode == HttpStatusCode.BadRequest)
                    {
                        Console.WriteLine($"Day {Day}: Error code 400 when attempting to retrieve puzzle input through the web client. Your session cookie is probably not recognized.");
                    }
                    else if(statusCode == HttpStatusCode.NotFound)
                    {
                        Console.WriteLine($"Day {Day}: Error code 404 when attempting to retrieve puzzle input through the web client. The puzzle is probably not available yet.");
                    }
                    else
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                catch(InvalidOperationException)
                {
                    Console.WriteLine($"Day {Day}: Cannot fetch puzzle input before given date (Eastern Standard Time).");
                }
            }
            return input;
        }

        string LoadDebugInput()
        {
            string INPUT_FILEPATH = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $"../../../Solutions/Year{Year}/Day{Day.ToString("D2")}/debug"));
            return (File.Exists(INPUT_FILEPATH) && new FileInfo(INPUT_FILEPATH).Length > 0)
                ? File.ReadAllText(INPUT_FILEPATH)
                : "";
        }

        (string, TimeSpan) Solver(Func<string> SolverFunction)
        {
            try
            {
                var then = DateTime.Now;
                var result = SolverFunction();
                var now = DateTime.Now;
                return string.IsNullOrEmpty(result) ? (string.Empty, TimeSpan.Zero) : (result, (now - then));
            }
            catch(Exception) {
                if(Debugger.IsAttached)
                {
                    Debugger.Break();
                    return (string.Empty, TimeSpan.Zero);
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
