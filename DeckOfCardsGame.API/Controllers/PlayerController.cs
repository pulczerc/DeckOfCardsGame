using Microsoft.AspNetCore.Mvc;
using DeckOfCardsGame.DAL.Models;
using DeckOfCardsGame.API.Services;
using DeckOfCardsGame.API.Commands.Player;

namespace DeckOfCardsGame.API.Controllers
{
    /// <summary>
    /// Controller for managing player-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerController"/> class.
        /// </summary>
        /// <param name="playerService">The service for player operations.</param>
        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }


        /// <summary>
        /// Get a player by player ID.
        /// </summary>
        /// <param name="playerId">The ID of the player.</param>
        [HttpGet("{playerId}")]
        [ProducesResponseType(200, Type = typeof(Player))]
        [ProducesResponseType(404, Type = typeof(string))]
        public ActionResult<Player> GetPlayer(Guid playerId)
        {
            var player = _playerService.GetPlayer(playerId);
            if (player != null)
            {
                return player;
            }

            return NotFound("Player not found");
        }

        /// <summary>
        /// Get all players.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Player>))]
        public ActionResult<IEnumerable<Player>> GetAllPlayers()
        {
            return _playerService.GetAllPlayers().ToList();
        }

        /// <summary>
        /// Get the cards held by a player.
        /// </summary>
        /// <param name="playerId">The ID of the player.</param>
        [HttpGet("{playerId}/cards")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Card>))]
        [ProducesResponseType(404, Type = typeof(string))]
        public ActionResult<IEnumerable<Card>> GetPlayerCards(Guid playerId)
        {
            var cards = _playerService.GetPlayerCards(playerId);
            if (cards != null)
            {
                return cards.ToList();
            }

            return NotFound("Player not found");
        }

        /// <summary>
        /// Create a new player.
        /// </summary>
        /// <param name="createPlayerRequest">The player details for creation.</param>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Player))]
        [ProducesResponseType(400, Type = typeof(string))]
        public ActionResult<Player> CreatePlayer([FromBody] CreatePlayerRequest createPlayerRequest)
        {
            var newPlayer = _playerService.CreatePlayer(createPlayerRequest);
            if (newPlayer != null)
            {
                return CreatedAtAction(nameof(GetPlayer), new { playerId = newPlayer.PlayerId }, newPlayer);
            }

            return BadRequest("Player with the same ID already exists.");
        }

        /// <summary>
        /// Remove a player by player ID.
        /// </summary>
        /// <param name="playerId">The ID of the player to be removed.</param>
        [HttpDelete("{playerId}/remove")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public ActionResult RemovePlayer(Guid playerId)
        {
            if (_playerService.RemovePlayer(playerId))
            {
                return NoContent(); // HTTP 204 No Content
            }

            return NotFound("Player not found");
        }
    }
}
