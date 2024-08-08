using IntuitAssignment.API.DataFetcher;

namespace IntuitAssignment.Api
{
    public class PlayerApi
    {
        private BaseballDataFetcher _baseballDf;
        private RetrosheetDataFetcher _retroSheetDf;

        public PlayerApi(BaseballDataFetcher BaseballDf, RetrosheetDataFetcher retroSheetDf)
        {
            _baseballDf = BaseballDf;
            _retroSheetDf = retroSheetDf;
        }
    }
}
