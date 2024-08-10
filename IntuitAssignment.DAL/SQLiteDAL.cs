using IntuitAssignment.DAL.Interfaces;
using IntuitAssignments.DAL.Models;

namespace IntuitAssignment.DAL
{
    // This is un implemented dal, which can be implemented and injected instead of the InMemPlayersDAL
    public class SQLiteDAL : IPlayerDAL
    {
        public Task<IEnumerable<Player>> GetAllPlayers(int limit, int page)
        {
            throw new NotImplementedException();
        }

        public Task<Player> GetPlayerByID(string playerID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertPlayers(IEnumerable<Player> players, CancellationToken ct, int retry = 1)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertPlayers(Player player, CancellationToken ct, int retry = 1)
        {
            throw new NotImplementedException();
        }
    }
}
