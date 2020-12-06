using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;

namespace AdventOfCode.Solutions
{

    abstract class ASolution
    {
        Lazy<string> _input;
        Lazy<(string, TimeSpan)> _part1, _part2;

        public int Day { get; }
        public int Year { get; }
        public string Title { get; }
        public string DebugInput { get; set; }
        public string Input => DebugInput ?? _input.Value ?? null;
        public (string answer, TimeSpan time) Part1 => _part1.Value;
        public (string answer, TimeSpan time) Part2 => _part2.Value;


        private protected ASolution(int day, int year, string title)
        {
            Day = day;
            Year = year;
            Title = title;
            _input = new Lazy<string>(LoadInput);
            _part1 = new Lazy<(string, TimeSpan)>(() => Solver(SolvePartOne));
            _part2 = new Lazy<(string, TimeSpan)>(() => Solver(SolvePartTwo));
        }

        public void Solve(int part = 0)
        {
            if(Input == null) return;

            bool doOutput = false;
            string output = $"--- Day {Day}: {Title} --- \n";
            if(DebugInput != null)
            {
                output += $"!!! DebugInput used: {DebugInput}\n";
            }

            output += $"+{new String('-', 8)}+{new String('-', 18)}+{new String('-', 12)}+\n";

            if(part != 2)
            {
                var (answer, time) = Part1;
                if(answer != "")
                {
                    output += $"| Part 1 | {answer.PadRight(16)} | {$"{time.TotalMilliseconds}ms".PadRight(10)} |\n";
                    doOutput = true;
                }
                else
                {
                    output += "| Part 1 | Unsolved\n";
                    if(part == 1) doOutput = true;
                }
            }
            if(part != 1)
            {
                var (answer, time) = Part2;
                if(answer != "")
                {
                    output += $"| Part 2 | {answer.PadRight(16)} | {$"{time.TotalMilliseconds}ms".PadRight(10)} |\n";
                    doOutput = true;
                }
                else
                {
                    output += "| Part 2 | Unsolved\n";
                    if(part == 2) doOutput = true;
                }
            }

            if(doOutput) Console.WriteLine(output);
        }

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
