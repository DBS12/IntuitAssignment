using IntuitAssignment.DAL.Interfaces;
using IntuitAssignments.DAL.Models;

namespace IntuitAssignment.DAL
{
    public class PlayersDAL : IPlayerDAL
    {
        List<Player> _players = new List<Player>();

        public PlayersDAL()
        {

        }

        // Adds a player to the repository
        public void AddPlayer(Player player)
        {

        }

        // Get a player by their ID
        public Player GetPlayerByID(string playerID)
        {
            return null;
        }

        // Get all players with paging
        public IEnumerable<Player> GetAllPlayers(int limit, int page)
        {
            return null;
        }

        public void InsertPlayers(IEnumerable<Player> players)
        {
            _players.AddRange(players);
        }
    }
}
