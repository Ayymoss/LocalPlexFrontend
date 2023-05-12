using System.Text.Json;

namespace PlexFront.Utilities;

public static class ExtensionMethods
{
    public static DateTimeOffset LongToDateTime(this long unixTime)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTime);
    }

    public static TimeSpan SecondsToTime(this long seconds) => TimeSpan.FromSeconds(seconds);

    public static string HumanizeDownloadSpeed(this long bytes)
    {
        const int scale = 1024;
        var bytesToMb = 8 / Math.Pow(scale, 2);
        return $"{bytes * bytesToMb:F2} Mbps";
    }

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
}
