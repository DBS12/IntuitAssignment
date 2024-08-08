using HtmlAgilityPack;
using IntuitAssignment.Scrapers.Interfaces;

namespace IntuitAssignment.Scrapers
{
    public class BaseballDataFetcher : IDataFetcher
    {
        private string url = $"https://www.baseball-reference.com/players/";

        public async Task<PlayerDetails> ScrapePlayerDetails(string uuid)
        {
            var fullUrl = string.Concat(url, uuid[0], "/", uuid, ".shtml");
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(fullUrl);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var playerDetails = new PlayerDetails();

            var positionNode = htmlDocument.DocumentNode.SelectSingleNode("//strong[text()='Position:']/parent::p");
            playerDetails.Position = positionNode?.SelectSingleNode(".//text()[2]")?.InnerText.Trim();

            var heightWeightNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='meta']/div/p/span[1]");
            if (heightWeightNode != null)
            {
                var heightSpan = heightWeightNode.InnerText.Trim();
                var weightSpan = heightWeightNode.SelectSingleNode("../span[2]").InnerText.Trim();

                playerDetails.Height = heightSpan;
                playerDetails.Weight = weightSpan;
            }

            var batsThrowsNode = htmlDocument.DocumentNode.SelectSingleNode("//p[contains(.,'Bats')]");
            if (batsThrowsNode != null)
            {
                var batsText = batsThrowsNode.InnerText;
                playerDetails.Bats = ExtractDetail(batsText, "Bats");
                playerDetails.Throws = ExtractDetail(batsText, "Throws");
            }

            return playerDetails;
        }

        private static string ExtractDetail(string infoText, string detailName)
        {
            var startIndex = infoText.IndexOf(detailName);
            if (startIndex >= 0)
            {
                startIndex += detailName.Length + 1;
                var endIndex = infoText.IndexOf("\n", startIndex);
                if (endIndex == -1)
                {
                    endIndex = infoText.Length;
                }

                return infoText.Substring(startIndex, endIndex - startIndex).Trim();
            }

            return string.Empty;
        }
    }
}

