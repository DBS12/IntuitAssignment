using IntuitAssignments.DAL.Models;

namespace IntuitAssignment.DAL.Interfaces
{
    public interface IPlayerDAL
    {
        Player GetPlayerByID(string playerID);

        IEnumerable<Player> GetAllPlayers(int limit, int page);

        void InsertPlayers(IEnumerable<Player> players);
    }
}
