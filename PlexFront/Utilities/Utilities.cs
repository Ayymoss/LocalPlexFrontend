using System.Reflection;

namespace PlexFront.Utilities;

public class Utilities
{
    public static string GetVersionAsString()
    {
        return Assembly.GetCallingAssembly().GetName().Version?.ToString() ?? "Unknown";
    }
}
