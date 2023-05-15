using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace PlexFront.Utilities;

public class Utilities
{
    public static string GetVersionAsString()
    {
        return Assembly.GetCallingAssembly().GetName().Version?.ToString() ?? "Unknown";
    }

    public static async Task<(double Total, double Used)?> GetUsedStoragePercentage()
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
            
            var lines = output.Split('\n');
            foreach (var line in lines)
            {
                if (!line.Contains("/media")) continue;

                var parts = line.Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
                var total = double.Parse(parts[1]);
                var used = double.Parse(parts[2]);
                return (total, used);
            }
        }
        catch
        {
            return null;
        }

        return null;
    }
}
