using PlexFront.Models;
using RestEase;

namespace PlexFront.Interfaces;

public interface IQbitTorrentApi
{
    [Post("/api/v2/auth/login")]
    Task Authenticate([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> data);

    [Get("/api/v2/torrents/info")]
    Task<HttpResponseMessage> GetTorrents();
}
