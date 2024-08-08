using HtmlAgilityPack;
using IntuitAssignment.API.DataFetcher.Interfaces;
using System.Text.RegularExpressions;

namespace IntuitAssignment.API.DataFetcher
{
    public class RetrosheetDataFetcher : IDataFetcher
    {
        public async Task<PlayerDetails> ScrapePlayerDetails(string uuid)
        {
            char firstLetter = char.ToUpper(uuid[0]);
            string url = $"https://www.retrosheet.org/boxesetc/{firstLetter}/P{uuid}.htm";

            return await ScrapePlayerMetadata(url);
        }

        public async Task<PlayerDetails> ScrapePlayerMetadata(string url)
        {
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var playerMetadata = new PlayerDetails();

            // Locate the table row containing the metadata
            var metadataRowNode = htmlDocument.DocumentNode.SelectSingleNode("//table/tr[4]/td");

            if (metadataRowNode != null)
            {
                var metadataText = metadataRowNode.InnerText;

                // Extract bats, throws, height, and weight using regex
                playerMetadata.Bats = ExtractDetail(metadataText, @"Bat:\s*([^\s]+)");
                playerMetadata.Throws = ExtractDetail(metadataText, @"Throw:\s*([^\s]+)");
                playerMetadata.Height = ExtractDetail(metadataText, @"Height:\s*([^\s]+(?:\s[^\s]+)?)");
                playerMetadata.Weight = ExtractDetail(metadataText, @"Weight:\s*(\d+)");
            }

            return playerMetadata;
        }

        private static string ExtractDetail(string text, string pattern)
        {
            var match = Regex.Match(text, pattern);
            if (match.Success)
            {
                return match.Groups[1].Value.Trim();
            }

            return string.Empty;
        }
    }
}
