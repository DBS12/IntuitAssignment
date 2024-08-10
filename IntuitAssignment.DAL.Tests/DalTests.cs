using IntuitAssignment.DAL.Interfaces;
using IntuitAssignments.DAL.Models;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntuitAssignment.DAL.Tests
{
    [TestClass]
    public class DalTests
    {
        private IPlayerDAL _dal;

        [ClassInitialize]
        public async Task ClassInitializer()
        {
            _dal = new InMemPlayersDAL();
            await _dal.InsertPlayers(CreatePlayer("1"), new CancellationToken());
            await _dal.InsertPlayers(CreatePlayer("2"), new CancellationToken());
            await _dal.InsertPlayers(CreatePlayer("3"), new CancellationToken());
            await _dal.InsertPlayers(CreatePlayer("4"), new CancellationToken());
            await _dal.InsertPlayers(CreatePlayer("5"), new CancellationToken());
            await _dal.InsertPlayers(CreatePlayer("6"), new CancellationToken());
            await _dal.InsertPlayers(CreatePlayer("7"), new CancellationToken());
            await _dal.InsertPlayers(CreatePlayer("8"), new CancellationToken());
            await _dal.InsertPlayers(CreatePlayer("9"), new CancellationToken());
            await _dal.InsertPlayers(CreatePlayer("10"), new CancellationToken());
        }

        [TestInitialize]
        public async Task TestInitialize()
        {

        }

        [TestMethod]
        public async Task TestGetPlayerByID_PlayerNotExists_ReturnNull()
        {
            var p = await _dal.GetPlayerByID("test");
            Assert.IsNull(p);
        }

        [TestMethod]
        public async Task TestGetPlayerByID_PlayerExists_ReturnCorrectPlayer()
        {
            var p = await _dal.GetPlayerByID("1");
            Assert.IsNotNull(p);
            Assert.Equals("1", p.PlayerID);
        }

        [DataRow(10, 1, 10)]
        [DataRow(7, 2, 3)]
        [DataRow(2, 1, 2)]
        [DataRow(2, 9, 0)]
        [TestMethod]
        public async Task TestAllPlayers_PaginationResults(int limit, int page, int expectedResults)
        {
            var players = await _dal.GetAllPlayers(limit, page);

            Assert.Equals(expectedResults, players.Count());
        }

        public Player CreatePlayer(string id)
        {
            return new Player()
            {
                PlayerID = id
            };
        }
    }
}