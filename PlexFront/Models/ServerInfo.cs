namespace PlexFront.Models;

public record Storage(double Total, double Used, float Percentage);

public class ServerInfo
{
    public Dictionary<string, Storage>? DiskUsage { get; set; }
}
