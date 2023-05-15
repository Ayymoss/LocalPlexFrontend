using System.Text.Json;
using MudBlazor;

namespace PlexFront.Utilities;

public static class ExtensionMethods
{
    public static DateTimeOffset LongToDateTime(this long unixTime)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTime);
    }

    public static TimeSpan SecondsToTime(this long seconds) => TimeSpan.FromSeconds(seconds);

    public static async Task<TResponse?> DeserializeHttpResponseContentAsync<TResponse>(
        this HttpResponseMessage response) where TResponse : class
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        if (!response.IsSuccessStatusCode) return null;
        var json = await response.Content.ReadAsStringAsync();
        return string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<TResponse>(json, jsonSerializerOptions);
    }

    public static string TruncateWithEllipsis(this string input, Breakpoint breakpoint)
    {
        if (string.IsNullOrEmpty(input)) return input;

        var characterLength = breakpoint switch
        {
            Breakpoint.Xs => 24,
            Breakpoint.Sm => 16,
            Breakpoint.Md => 24,
            Breakpoint.Lg => 32,
            Breakpoint.Xl => 40,
            Breakpoint.Xxl => 48,
            _ => 24
        };

        return input.Length <= characterLength ? input : input[..(characterLength - 3)] + "...";
    }

    public static List<string> GetSplitLines(this string input)
    {
        var result = new List<string>();
        var index = 0;
        const int maxLineLength = 42;
        while (index < input.Length)
        {
            result.Add(index + maxLineLength < input.Length
                ? input.Substring(index, maxLineLength)
                : input[index..]);
            index += maxLineLength;
        }

        return result;
    }
}
