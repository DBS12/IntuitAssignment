using IntuitAssignment.DAL.Interfaces;
using IntuitAssignment.Utils;
using IntuitAssignments.DAL.Models;
using System.Collections.Concurrent;

namespace IntuitAssignment.DAL
{
    public class InMemPlayersDAL : IPlayerDAL
    {
        // In memory DB -> _players used to fetch data in O(1) for player by ID - This data structure simulate the real DB
        ConcurrentDictionary<string, Player> _players = new ConcurrentDictionary<string, Player>();

        // In memory DB -> _playersRepo used to fetch ordered data with limit/offset
        List<Player> _playersRepo = new List<Player>();

        LRUCache<string, Player> lruCache = new LRUCache<string, Player>(5000);

        public InMemPlayersDAL()
        {

        }

        public Task<Player> GetPlayerByID(string playerID)
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

            return Task.FromResult(player);
        }

        public Task<IEnumerable<Player>> GetAllPlayers(int limit, int page)
        {
            return Task.FromResult(_playersRepo.Take(limit).Skip(page * limit));
        }

        public async Task<bool> InsertPlayers(IEnumerable<Player> players, CancellationToken ct, int retry = 1)
        {
            var succeded = TryInsert(players);
            while (--retry > 0 && !succeded)
            {
                // Wait some time until next try
                await Task.Delay(1000);
                succeded = TryInsert(players);
            }

            return succeded;
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
