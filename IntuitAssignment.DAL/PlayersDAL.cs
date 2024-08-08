using IntuitAssignment.DAL.Interfaces;
using IntuitAssignments.DAL.Models;

namespace IntuitAssignment.DAL
{
    public class PlayersDAL : IPlayerDAL
    {
        Dictionary<string, Player> _players = new Dictionary<string, Player>();

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
            return _players[playerID];
        }

        // Get all players with paging
        public IEnumerable<Player> GetAllPlayers(int limit, int page)
        {
            return null;
        }

        public void InsertPlayers(IEnumerable<Player> players)
        {
            foreach (var item in players)
            {
                _players[item.PlayerID] = item;
            }
        }
    }
}
