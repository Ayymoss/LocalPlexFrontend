using System.Text.Json.Serialization;

namespace PlexFront.Models.qBitApi;


public class ServerStateInfo
{
    [JsonPropertyName("server_state")]
    public ServerStateContext ServerState { get; set; }
}

public class ServerStateContext
{
    [JsonPropertyName("alltime_dl")]
    public long AlltimeDl { get; set; }

    [JsonPropertyName("alltime_ul")]
    public long AlltimeUl { get; set; }

    [JsonPropertyName("average_time_queue")]
    public int AverageTimeQueue { get; set; }

    [JsonPropertyName("connection_status")]
    public string ConnectionStatus { get; set; }

    [JsonPropertyName("dht_nodes")]
    public int DhtNodes { get; set; }

    [JsonPropertyName("dl_info_data")]
    public long DlInfoData { get; set; }

    [JsonPropertyName("dl_info_speed")]
    public int DlInfoSpeed { get; set; }

    [JsonPropertyName("dl_rate_limit")]
    public int DlRateLimit { get; set; }

    [JsonPropertyName("free_space_on_disk")]
    public long FreeSpaceOnDisk { get; set; }

    [JsonPropertyName("global_ratio")]
    public string GlobalRatio { get; set; }

    [JsonPropertyName("queued_io_jobs")]
    public int QueuedIoJobs { get; set; }

    [JsonPropertyName("queueing")]
    public bool Queueing { get; set; }

    [JsonPropertyName("read_cache_hits")]
    public string ReadCacheHits { get; set; }

    [JsonPropertyName("read_cache_overload")]
    public string ReadCacheOverload { get; set; }

    [JsonPropertyName("refresh_interval")]
    public int RefreshInterval { get; set; }

    [JsonPropertyName("total_buffers_size")]
    public int TotalBuffersSize { get; set; }

    [JsonPropertyName("total_peer_connections")]
    public int TotalPeerConnections { get; set; }

    [JsonPropertyName("total_queued_size")]
    public int TotalQueuedSize { get; set; }

    [JsonPropertyName("total_wasted_session")]
    public int TotalWastedSession { get; set; }

    [JsonPropertyName("up_info_data")]
    public long UpInfoData { get; set; }

    [JsonPropertyName("up_info_speed")]
    public int UpInfoSpeed { get; set; }

    [JsonPropertyName("up_rate_limit")]
    public int UpRateLimit { get; set; }

    [JsonPropertyName("use_alt_speed_limits")]
    public bool UseAltSpeedLimits { get; set; }

    [JsonPropertyName("write_cache_overload")]
    public string WriteCacheOverload { get; set; }
}


