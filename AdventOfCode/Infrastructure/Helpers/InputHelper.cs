

using System;
using System.IO;
using System.Net;

namespace AdventOfCode.Infrastructure.Helpers
{
    static class InputHelper
    {
        public static string LoadInput(int year, int day)
        {
            string INPUT_FILEPATH = GetDayPath(year, day) + "/input";
            string INPUT_URL = GetAocInputUrl(year, day);
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
                    if(CURRENT_EST < new DateTime(year, 12, day)) throw new InvalidOperationException();

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
                        Console.WriteLine($"Day {day}: Error code 400 when attempting to retrieve puzzle input through the web client. Your session cookie is probably not recognized.");
                    }
                    else if(statusCode == HttpStatusCode.NotFound)
                    {
                        Console.WriteLine($"Day {day}: Error code 404 when attempting to retrieve puzzle input through the web client. The puzzle is probably not available yet.");
                    }
                    else
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                catch(InvalidOperationException)
                {
                    Console.WriteLine($"Day {day}: Cannot fetch puzzle input before given date (Eastern Standard Time).");
                }
            }
            return input;
        }

        public static string LoadDebugInput(int year, int day)
        {
            string INPUT_FILEPATH = GetDayPath(year, day) + "/debug";
            return (File.Exists(INPUT_FILEPATH) && new FileInfo(INPUT_FILEPATH).Length > 0)
                ? File.ReadAllText(INPUT_FILEPATH)
                : "";
        }
        
        static string GetDayPath(int year, int day)
            => Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $"../../../Solutions/Year{year}/Day{day.ToString("D2")}"));

        static string GetAocInputUrl(int year, int day)
            => $"https://adventofcode.com/{year}/day/{day}/input";
    }
}