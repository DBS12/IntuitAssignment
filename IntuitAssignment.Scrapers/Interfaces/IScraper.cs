using Newtonsoft.Json.Linq;

namespace IntuitAssignment.Scrapers.Interfaces
{
    public interface IScraper
    {
        Task<PlayerDetails> ScrapePlayerDetails(string uuid);
    }
}
