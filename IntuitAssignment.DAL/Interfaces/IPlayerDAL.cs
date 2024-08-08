using IntuitAssignments.DAL.Models;

namespace IntuitAssignment.DAL.Interfaces
{
    public interface IPlayerDAL
    {
        Task<Player> GetPlayerByID(string playerID);

        Task<IEnumerable<Player>> GetAllPlayers(int limit, int page);

        Task<bool> InsertPlayers(IEnumerable<Player> players, CancellationToken ct, int retry = 1);
    }
}
