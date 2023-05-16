using MudBlazor;
using PlexFront.Interfaces;
using PlexFront.Models;
using PlexFront.Models.qBitApi;
using PlexFront.Utilities;
using RestEase;

namespace PlexFront.Services;

public class QBitTorrentService
{
    private readonly Configuration _config;
    private readonly ILogger<QBitTorrentService> _logger;
    private readonly IQbitTorrentApi _api;

    public QBitTorrentService(Configuration config, ILogger<QBitTorrentService> logger)
    {
        _config = config;
        _logger = logger;
        _api = RestClient.For<IQbitTorrentApi>(config.QBitApiEndPoint);
    }

    public async Task<ServerInfo?> FetchServerState()
    {
        var loginData = new Dictionary<string, string>
        {
            {"username", _config.QBitUsername},
            {"password", _config.QBitPassword}
        };

        await _api.Authenticate(loginData);

        var responseMessage = await _api.GetSystemState();
        var server = await responseMessage.DeserializeHttpResponseContentAsync<ServerStateInfo>();
        var diskUsage = new Dictionary<string, Storage>();

        // Get storage from qBit (can't do via df since qBit isn't localhost)
        if (server?.ServerState is not null)
        {
            var used = _config.LocalDiskSize - server.ServerState.FreeSpaceOnDisk;
            var percentage = used / (float)_config.LocalDiskSize;
            var storage = new Storage(_config.LocalDiskSize, used, percentage);
            diskUsage.Add("root", storage);
        }

        // Get relevant drives from DF
        var storageFromDf = await Utilities.Utilities.GetUsedStoragePercentage();
        if (storageFromDf != null)
        {
            foreach (var localStorage in storageFromDf)
            {
                diskUsage.Add(localStorage.Key, localStorage.Value);
            }
        }

        var serverInfo = new ServerInfo
        {
            DiskUsage = diskUsage
        };

        return serverInfo;
    }

    public async Task<TorrentInfoContext?> FetchTorrentData(Pagination pagination)
    {
        try
        {
            var loginData = new Dictionary<string, string>
            {
                {"username", _config.QBitUsername},
                {"password", _config.QBitPassword}
            };

            await _api.Authenticate(loginData);

            var responseMessage = await _api.GetTorrents();
            var torrents = await responseMessage.DeserializeHttpResponseContentAsync<List<TorrentInfo>>();
            if (torrents is null) return null;

            var query = torrents
                .Where(x => x.Category.ToLower() is "tv-sonarr" or "movies-radarr")
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.SearchString))
            {
                query = query.Where(search => search.Name.ToLower()
                    .Contains(pagination.SearchString));
            }

            query = pagination.SortLabel switch
            {
                "Name" => query.OrderByDirection((SortDirection)pagination.SortDirection!, key => key.Name),
                "DownloadSpeed" => query.OrderByDirection((SortDirection)pagination.SortDirection!,
                    key => key.DownloadSpeed),
                "Progress" => query.OrderByDirection((SortDirection)pagination.SortDirection!, key => key.Progress),
                "AmountLeft" => query.OrderByDirection((SortDirection)pagination.SortDirection!, key => key.AmountLeft),
                "ETA" => query.OrderByDirection((SortDirection)pagination.SortDirection!, key => key.Eta),
                "State" => query.OrderByDirection((SortDirection)pagination.SortDirection!, key => key.State),
                "Category" => query.OrderByDirection((SortDirection)pagination.SortDirection!, key => key.Category),
                "TotalSize" => query.OrderByDirection((SortDirection)pagination.SortDirection!, key => key.TotalSize),
                "Availability" => query.OrderByDirection((SortDirection)pagination.SortDirection!,
                    key => key.Availability),
                "Added" => query.OrderByDirection((SortDirection)pagination.SortDirection!, key => key.AddedOn),
                _ => query
            };

            var dataSize = query.Count();

            var pagedData = query
                .Skip(pagination.Page!.Value * pagination.PageSize!.Value)
                .Take(pagination.PageSize.Value)
                .Select(torrent => new TorrentInfo
                {
                    Name = torrent.Name,
                    DownloadSpeed = torrent.DownloadSpeed,
                    Progress = torrent.Progress,
                    Eta = torrent.Eta,
                    State = torrent.State,
                    Category = torrent.Category,
                    AmountLeft = torrent.AmountLeft,
                    Availability = torrent.Availability,
                    TotalSize = torrent.TotalSize,
                    AddedOn = torrent.AddedOn
                }).ToList();

            var context = new TorrentInfoContext
            {
                Count = dataSize,
                Torrents = pagedData
            };

            return context;
        }
        catch (Exception e)
        {
            _logger.LogCritical("Failed to get torrents from API {Error}", e);
        }

        return null;
    }
}
