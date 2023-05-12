using MudBlazor;
using PlexFront.Interfaces;
using PlexFront.Models;
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
        _api = RestClient.For<IQbitTorrentApi>(config.ApiEndPoint);
    }

    public async Task<TorrentInfoContext?> FetchTorrentData(Pagination pagination)
    {
        try
        {
            var loginData = new Dictionary<string, string>
            {
                {"username", _config.Username},
                {"password", _config.Password}
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
                query = query.Where(search => search.Name.Contains(pagination.SearchString));
            }

            query = pagination.SortLabel switch
            {
                "Name" => query.OrderByDirection((SortDirection)pagination.SortDirection!, key => key.Name),
                "DownloadSpeed" => query.OrderByDirection((SortDirection)pagination.SortDirection!,
                    key => key.DownloadSpeed),
                "Progress" => query.OrderByDirection((SortDirection)pagination.SortDirection!, key => key.Progress),
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
