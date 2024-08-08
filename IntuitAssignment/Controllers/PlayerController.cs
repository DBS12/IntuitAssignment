using IntuitAssignment.Api;
using IntuitAssignments.API.Models;
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
        public Player GetPlayerByID()
        {
            return null;
        }

        [HttpGet]
        [Route("getAllPlayers")]
        public Player GetAllPlayers(int limit, int page)
        {
            return null;
        }
    }
}