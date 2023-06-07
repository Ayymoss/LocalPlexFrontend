using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using PlexFront.Models;

namespace PlexFront.Utilities;

public static class Utilities
{
    public static string GetVersionAsString()
    {
        return Assembly.GetCallingAssembly().GetName().Version?.ToString() ?? "Unknown";
    }

    public static async Task<Dictionary<string, Storage>?> GetUsedStoragePercentage()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return null;

        try
        {
            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = "-c \"df\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            var output = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();

            var spaceUsed = new Dictionary<string, Storage>();
            var lines = output.Split('\n');
            foreach (var line in lines)
            {
                // This is pretty fragile... but it works for now
                var parts = line.Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 6) continue;
                if (parts[0] != "10.10.1.5:/volume1/Content") continue;
                var total = double.Parse(parts[1]);
                var used = double.Parse(parts[2]);
                var percentage = (float)(used / total);
                spaceUsed.Add("media", new Storage(total, used, percentage));
            }

            return spaceUsed;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error getting storage info: {e}");
        }

        return null;
    }
}
