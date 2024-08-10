using IntuitAssignment.Api;
using IntuitAssignment.API.Models;
using IntuitAssignments.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntuitAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerApi _playerApi;
        private readonly ILogger<PlayerController> _logger;

        public PlayerController(ILogger<PlayerController> logger, PlayerApi playerApi)
        {
            _logger = logger;
            _playerApi = playerApi;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayerByID(string id)
        {
            var player = await _playerApi.GetPlayer(id);

            if (player == null)
                return NotFound();

            return Ok(new GetPlayerResponse()
            {
                Player = player
            });
        }

        [HttpGet]
        [Route("getAllPlayers")]
        public async Task<IActionResult> GetAllPlayers(int limit, int page)
        {
            var players = await _playerApi.GetAllPlayers(limit, page);

            if (players == null)
                return NotFound();

            return Ok(new GetAllPlayersResponse()
            {
                Players = players.ToList()
            });
        }
    }
}