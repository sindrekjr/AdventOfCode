using System.Net;

namespace AdventOfCode.Solutions;

public static class InputService
{
    private static readonly HttpClientHandler _handler = new()
    {
        CookieContainer = GetCookieContainer(),
        UseCookies = true,
    };

    private static readonly HttpClient _client = new(_handler)
    {
        BaseAddress = new Uri("https://adventofcode.com/"),
    };

    public static async Task<string> FetchTitle(int year, int day)
    {
        if (!IsPuzzleAvailable(year, day))
        {
            throw new InvalidOperationException("Too early to access puzzle");
        }

        var response = await _client.GetAsync($"{year}/day/{day}");
        var html = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

        var titlePrefix = $"Day {day}: ";
        var titleIndexStart = html.IndexOf(titlePrefix) + titlePrefix.Length;
        return new string([.. html.Skip(titleIndexStart).TakeWhile(ch => ch != '-')]).Trim();
    }

    public static async Task<string> FetchInput(int year, int day)
    {
        if (!IsPuzzleAvailable(year, day))
        {
            throw new InvalidOperationException("Too early to get puzzle input");
        }

        var response = await _client.GetAsync($"{year}/day/{day}/input");
        return await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
    }

    private static CookieContainer GetCookieContainer()
    {
        var container = new CookieContainer();
        container.Add(new Cookie
        {
            Name = "session",
            Domain = ".adventofcode.com",
            Value = Config.Get().Cookie.Replace("session=", ""),
        });

        return container;
    }

    private static bool IsPuzzleAvailable(int year, int day) =>
        TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "EST") >= new DateTime(year, 12, day);
}
