using System.Text.Json.Serialization;

namespace PlexFront.Models;

public class TorrentInfoContext
{
    public int Count { get; set; }   
    public List<TorrentInfo>? Torrents { get; set; }   
}
public class TorrentInfo
{
    [JsonPropertyName("added_on")] public long AddedOn { get; set; }

    [JsonPropertyName("amount_left")] public long AmountLeft { get; set; }

    [JsonPropertyName("auto_tmm")] public bool AutoTmm { get; set; }

    [JsonPropertyName("availability")] public double Availability { get; set; }

    [JsonPropertyName("category")] public string Category { get; set; }

    [JsonPropertyName("completed")] public long Completed { get; set; }

    [JsonPropertyName("completion_on")] public long CompletionOn { get; set; }

    [JsonPropertyName("dl_limit")] public int DlLimit { get; set; }

    [JsonPropertyName("dlspeed")] public long DownloadSpeed { get; set; }

    [JsonPropertyName("downloaded")] public long Downloaded { get; set; }

    [JsonPropertyName("downloaded_session")]
    public long DownloadedSession { get; set; }

    [JsonPropertyName("eta")] public long Eta { get; set; }

    [JsonPropertyName("f_l_piece_prio")] public bool FlPiecePrio { get; set; }

    [JsonPropertyName("force_start")] public bool ForceStart { get; set; }

    [JsonPropertyName("hash")] public string Hash { get; set; }

    [JsonPropertyName("last_activity")] public long LastActivity { get; set; }

    [JsonPropertyName("magnet_uri")] public string MagnetUri { get; set; }

    [JsonPropertyName("max_ratio")] public double MaxRatio { get; set; }

    [JsonPropertyName("max_seeding_time")] public long MaxSeedingTime { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("num_complete")] public int NumComplete { get; set; }

    [JsonPropertyName("num_incomplete")] public int NumIncomplete { get; set; }

    [JsonPropertyName("num_leechs")] public int NumLeechs { get; set; }

    [JsonPropertyName("num_seeds")] public int NumSeeds { get; set; }

    [JsonPropertyName("priority")] public int Priority { get; set; }

    [JsonPropertyName("progress")] public double Progress { get; set; }

    [JsonPropertyName("ratio")] public float Ratio { get; set; }

    [JsonPropertyName("ratio_limit")] public int RatioLimit { get; set; }

    [JsonPropertyName("save_path")] public string SavePath { get; set; }

    [JsonPropertyName("seeding_time_limit")]
    public int SeedingTimeLimit { get; set; }

    [JsonPropertyName("seen_complete")] public long SeenComplete { get; set; }

    [JsonPropertyName("seq_dl")] public bool SeqDl { get; set; }

    [JsonPropertyName("size")] public long Size { get; set; }

    [JsonPropertyName("state")] public string State { get; set; }

    [JsonPropertyName("super_seeding")] public bool SuperSeeding { get; set; }

    [JsonPropertyName("tags")] public string Tags { get; set; }

    [JsonPropertyName("time_active")] public long TimeActive { get; set; }

    [JsonPropertyName("total_size")] public long TotalSize { get; set; }

    [JsonPropertyName("tracker")] public string Tracker { get; set; }

    [JsonPropertyName("up_limit")] public int UpLimit { get; set; }

    [JsonPropertyName("uploaded")] public long Uploaded { get; set; }

    [JsonPropertyName("uploaded_session")] public long UploadedSession { get; set; }

    [JsonPropertyName("upspeed")] public long UpSpeed { get; set; }
}
