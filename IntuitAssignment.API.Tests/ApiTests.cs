using IntuitAssignment.Api;
using IntuitAssignment.DAL.Interfaces;
using IntuitAssignments.DAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IntuitAssignment.API.Tests
{
    [TestClass]
    public class ApiTests
    {
        private Mock<IPlayerDAL> _dal;
        private PlayerApi _api;

        [TestInitialize]
        public async Task TestInitialize()
        {
            _dal = new Mock<IPlayerDAL>();
            _api = new PlayerApi(_dal.Object, new Scrapers.BaseballDataFetcher(), new Scrapers.RetrosheetDataFetcher());
        }

        [TestMethod]
        public async Task TestGetPlayerByID_PlayerNotExists_ReturnNull()
        {
            _dal.Setup(dal => dal.GetPlayerByID(It.IsAny<string>())).ReturnsAsync((Player)null);
            var p = await _api.GetPlayer("123");
            Assert.IsNull(p);
        }

        [TestMethod]
        public async Task TestGetPlayerByID_FindPlayer_SuccessReturn()
        {
            _dal.Setup(dal => dal.GetPlayerByID(It.IsAny<string>())).ReturnsAsync(new Player() { PlayerID = "123", RetroID = "1", BbrefID = "1" });
            var p = await _api.GetPlayer("123");
            Assert.Equals("123", p.PlayerID);
        }

        [TestMethod]
        public async Task TestGetAllPlayer_FindPlayers_SuccessReturn()
        {
            _dal.Setup(dal => dal.GetAllPlayers(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<Player>() { new Player() { PlayerID = "1", RetroID = "1", BbrefID = "1" } });
            var ps = await _api.GetAllPlayers(10, 1);
            Assert.Equals(1, ps.Count());
            Assert.Equals("1", ps.Single().PlayerID);
        }
    }
}