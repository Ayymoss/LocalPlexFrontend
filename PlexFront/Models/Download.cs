namespace PlexFront.Models;

public class DownloadContext
{
    public int Count { get; set; }
    public List<Download>? Downloads { get; set; }
}

public class Download
{
    public string Name { get; set; }
    public string Category { get; set; }
    public long DownloadSpeed { get; set; }
    public double Progress { get; set; }
    public long Eta { get; set; }
    public string State { get; set; }
    public long AmountLeft { get; set; }
    public double Availability { get; set; }
    public long TotalSize { get; set; }
    public long AddedOn { get; set; }
    public long CompletionOn { get; set; }
    public float Ratio { get; set; }
    public double MaxRatio { get; set; }
}
