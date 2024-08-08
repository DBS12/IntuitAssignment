using Newtonsoft.Json.Linq;

namespace IntuitAssignment.API.DataFetcher.Interfaces
{
    public interface IDataFetcher
    {
        Task<PlayerDetails> ScrapePlayerDetails(string uuid);
    }
}
