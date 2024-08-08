using IntuitAssignment.Scrapers;
using IntuitAssignment.DAL.Interfaces;
using IntuitAssignments.DAL.Models;

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

        public async Task<API.Models.Player> GetPlayer(string playerID)
        {
            var pDal = await _playerDal.GetPlayerByID(playerID);

            if (pDal == null)
            {
                return null;
            }

            return await ConvertPlayer(pDal);
        }


        public async Task<IEnumerable<API.Models.Player>> GetAllPlayers(int limit, int page)
        {
            var players = await _playerDal.GetAllPlayers(limit, page);
            var playerTasks = players.Select(async pDal => await ConvertPlayer(pDal));

            var convertedPlayers = await Task.WhenAll(playerTasks);

            return convertedPlayers;
        }

        public async Task<API.Models.Player> ConvertPlayer(Player pDal)
        {
            var pApi = new API.Models.Player()
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
