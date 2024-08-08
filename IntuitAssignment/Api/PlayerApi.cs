using IntuitAssignment.API.DataFetcher;
using IntuitAssignment.DAL.Interfaces;

namespace IntuitAssignment.Api
{
    public class PlayerApi
    {
        private BaseballDataFetcher _baseballDf;
        private RetrosheetDataFetcher _retroSheetDf;
        private IPlayerDAL _playerDal;

        public PlayerApi(IPlayerDAL playerDal, BaseballDataFetcher BaseballDf, RetrosheetDataFetcher retroSheetDf)
        {
            _baseballDf = BaseballDf;
            _retroSheetDf = retroSheetDf;
            _playerDal = playerDal;
        }

        public async Task<IntuitAssignments.API.Models.Player> GetPlayer(string playerID)
        {
            var pDal = _playerDal.GetPlayerByID(playerID);

            var pApi = new IntuitAssignments.API.Models.Player()
            {
                PlayerID = pDal.PlayerID,
                Bats = pDal.Bats,
                BirthCity = pDal.BirthCity,
                BirthCountry = pDal.BirthCountry,
                BirthDay = pDal.BirthDay,
                BirthMonth = pDal.BirthMonth,
                BirthState = pDal.BirthState,
                DeathCity = pDal.DeathCity,
                DeathDay = pDal.DeathDay,
                DeathCountry = pDal.DeathCountry,
                BirthYear = pDal.BirthYear,
                DeathMonth = pDal.DeathMonth,
                DeathState = pDal.DeathState,
                DeathYear = pDal.DeathYear,
                Debut = pDal.Debut,
                FinalGame = pDal.FinalGame,
                Height = pDal.Height,
                NameFirst = pDal.NameFirst,
                NameGiven = pDal.NameGiven,
                NameLast = pDal.NameLast,
                Throws = pDal.Throws,
                Weight = pDal.Weight
            };

            pApi.BbrefMD = await _baseballDf.ScrapePlayerDetails(pDal.BbrefID);
            pApi.RetroMD = await _retroSheetDf.ScrapePlayerDetails(pDal.RetroID);

            return pApi;
        }
    }
}
