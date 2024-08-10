using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntuitAssignment.API.Models
{
    public class GetAllPlayersResponse
    {
        public IEnumerable<Player> Players { get; set; }
    }
}
