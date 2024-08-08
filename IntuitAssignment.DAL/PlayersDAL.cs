using IntuitAssignment.DAL.Interfaces;
using IntuitAssignment.Utils;
using IntuitAssignments.DAL.Models;
using System.Collections.Concurrent;

namespace IntuitAssignment.DAL
{
    public class PlayersDAL : IPlayerDAL
    {
        // In memory DB -> _players used to fetch data in O(1) for player by ID - This data structure simulate the real DB
        ConcurrentDictionary<string, Player> _players = new ConcurrentDictionary<string, Player>();

        // In memory DB -> _playersRepo used to fetch ordered data with limit/offset
        List<Player> _playersRepo = new List<Player>();

        LRUCache<string, Player> lruCache = new LRUCache<string, Player>(5000);

        public PlayersDAL()
        {

        }

        public Player GetPlayerByID(string playerID)
        {
            Player player = lruCache.Get(playerID);
            if (player == null)
            {
                player = _players.ContainsKey(playerID) ? _players[playerID] : null;
                if (player != null)
                {
                    lruCache.Put(playerID, player);
                }
            }

            return player;
        }

        public IEnumerable<Player> GetAllPlayers(int limit, int page)
        {
            return _playersRepo.Take(limit).Skip(page * limit);
        }

        public void InsertPlayers(IEnumerable<Player> players, int retry = 1)
        {
            while (retry-- > 0 && !TryInsert(players)) ;
        }

        public bool TryInsert(IEnumerable<Player> players)
        {
            try
            {
                // Assume we have here single MySQL/PG/RedisDB insertion command for all players
                _playersRepo.AddRange(players);
                foreach (var p in players)
                {
                    _players[p.PlayerID] = p;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
