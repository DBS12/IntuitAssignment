using Newtonsoft.Json.Linq;

namespace IntuitAssignment.Scrapers.Interfaces
{
    public interface IDataFetcher
    {
        Task<PlayerDetails> ScrapePlayerDetails(string uuid);
    }
}
