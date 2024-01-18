using DeckOfCardsGame.API.Commands.Game;
using DeckOfCardsGame.API.Queries.Game;
using DeckOfCardsGame.API.Queries.Player;
using DeckOfCardsGame.API.Services;
using DeckOfCardsGame.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeckOfCardsGame.API.Controllers
{
    /// <summary>
    /// Controller for managing game-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameController"/> class.
        /// </summary>
        /// <param name="gameService">The service for game operations.</param>
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Gets all games.
        /// </summary>
        /// <returns>A list of all available games.</returns>
        [HttpGet("all-games")]
        public ActionResult<IEnumerable<Game>> GetAllGames()
        {
            return _gameService.GetAllGames().ToList();
        }

        /// <summary>
        /// Gets a specific game by its ID.
        /// </summary>
        /// <param name="gameId">The ID of the game to retrieve.</param>
        /// <returns>The game with the specified ID.</returns>
        [HttpGet("{gameId}")]
        public ActionResult<Game> GetGameById([FromRoute] Guid gameId)
        {
            var game = _gameService.GetGameById(gameId);
            return game != null ? game : NotFound("Game not found");
        }

        /// <summary>
        /// Gets the decks in a specific game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <returns>The list of decks in the specified game.</returns>
        [HttpGet("{gameId}/decks")]
        public ActionResult<List<Deck>> GetDecksInGame([FromRoute] Guid gameId)
        {
            var decks = _gameService.GetDecksInGame(gameId);
            return decks != null ? decks : NotFound("Game not found");
        }

        /// <summary>
        /// Gets the players in a specific game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <returns>The list of players in the specified game.</returns>
        [HttpGet("{gameId}/players")]
        public ActionResult<List<Player>> GetPlayersInGame([FromRoute] Guid gameId)
        {
            var players = _gameService.GetPlayersInGame(gameId);
            return players != null ? players : NotFound("Game not found");
        }

        /// <summary>
        /// Gets the cards in the game deck of a specific game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <returns>The cards in the game deck of the specified game.</returns>
        [HttpGet("{gameId}/game-deck-cards")]
        public ActionResult<IEnumerable<Card>> GetGameDeckCards([FromRoute] Guid gameId)
        {
            var gameDeckCards = _gameService.GetGameDeckCards(gameId);
            return gameDeckCards != null ? gameDeckCards.ToList() : NotFound("Game not found");
        }

        /// <summary>
        /// Creates a new game.
        /// </summary>
        /// <returns>The newly created game.</returns>
        [HttpPost]
        public ActionResult<Game> CreateGame()
        {
            var game = _gameService.CreateGame();
            if (game != null)
            {
                return CreatedAtAction(nameof(GetGameById), new { gameId = game.Id }, game);
            }

            return BadRequest("Game with the same ID already exists.");
        }

        /// <summary>
        /// Deletes a specific game by its ID.
        /// </summary>
        /// <param name="gameId">The ID of the game to be deleted.</param>
        /// <returns>No content if successful, otherwise, returns not found.</returns>
        [HttpDelete("{gameId}")]
        public ActionResult DeleteGame([FromRoute] Guid gameId)
        {
            return _gameService.DeleteGame(gameId)
                ? NoContent()
                : NotFound("Game not found");
        }

        /// <summary>
        /// Adds a deck to a specific game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <param name="deckId">The ID of the deck to be added.</param>
        /// <returns>The list of decks in the specified game.</returns>
        [HttpPost("{gameId}/decks/{deckId}")]
        public ActionResult<List<Deck>> AddDeckToGame([FromRoute] Guid gameId, [FromRoute] Guid deckId)
        {
            var decks = _gameService.AddDeckToGame(gameId, deckId);
            return decks != null ? decks : NotFound();
        }

        /// <summary>
        /// Adds a player to a specific game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <param name="playerId">The ID of the player to be added.</param>
        /// <returns>The added player.</returns>
        [HttpPost("{gameId}/player/{playerId}")]
        public ActionResult<Player> AddPlayerToGame([FromRoute] Guid gameId, [FromRoute] Guid playerId)
        {
            var player = _gameService.AddPlayerToGame(gameId, playerId);
            return player != null ? player : NotFound("Player not found");
        }

        /// <summary>
        /// Removes a player from a specific game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <param name="playerId">The ID of the player to be removed.</param>
        /// <returns>No content if successful, otherwise, returns not found.</returns>
        [HttpDelete("{gameId}/player/{playerId}")]
        public ActionResult RemovePlayerFromGame([FromRoute] Guid gameId, [FromRoute] Guid playerId)
        {
            return _gameService.RemovePlayerFromGame(gameId, playerId)
                ? NoContent()
                : NotFound("Player not found or game not found");
        }

        /// <summary>
        /// Shuffles the deck of a specific game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <returns>The shuffled cards.</returns>
        [HttpPost("{gameId}/shuffle")]
        public ActionResult<List<Card>> ShuffleGameDeck([FromRoute] Guid gameId)
        {
            var shuffledCards = _gameService.ShuffleGameDeck(gameId);
            return shuffledCards != null ? shuffledCards : NotFound("Game not found");
        }

        /// <summary>
        /// Deals cards to a player in a specific game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <param name="request">The request specifying the player and the number of cards to deal.</param>
        /// <returns>The dealt cards.</returns>
        [HttpPost("{gameId}/deal-cards")]
        public ActionResult<List<Card>> DealCardsToPlayer([FromRoute] Guid gameId, [FromBody] DealCardsRequest request)
        {
            var dealtCards = _gameService.DealCardsToPlayer(gameId, request.PlayerId, request.NumberOfCardsToDeal);
            return dealtCards != null
                ? dealtCards
                : BadRequest("Unable to deal cards. Check if the game, player, and number of cards claim are valid.");
        }

        /// <summary>
        /// Gets the hand values of players in a specific game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <returns>The list of player hand values.</returns>
        [HttpGet("{gameId}/players-hand-values")]
        public ActionResult<List<PlayerHandValue>> GetPlayersWithHandValues([FromRoute] Guid gameId)
        {
            var playersHandValues = _gameService.GetPlayersWithHandValues(gameId);
            if (playersHandValues != null)
            {
                return playersHandValues;
            }

            return NotFound("Game not found");
        }

        /// <summary>
        /// Gets the remaining cards count by suit in a specific game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <returns>The list of remaining cards count by suit.</returns>
        [HttpGet("{gameId}/remaining-cards-by-suit")]
        public ActionResult<List<RemainingCardsCountBySuit>> GetRemainingCardsCountBySuit([FromRoute] Guid gameId)
        {
            var remainingCardsBySuit = _gameService.GetRemainingCardsCountBySuit(gameId);
            return remainingCardsBySuit != null ? remainingCardsBySuit : NotFound("Game not found");
        }

        /// <summary>
        /// Gets the remaining cards count in a specific game grouped by suit and face value.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <returns>The dictionary containing the remaining cards count.</returns>
        [HttpGet("{gameId}/remaining-cards-count")]
        public ActionResult<Dictionary<string, int>> GetRemainingCardsCount([FromRoute] Guid gameId)
        {
            var remainingCardsCount = _gameService.GetRemainingCardsCount(gameId);
            return remainingCardsCount != null ? remainingCardsCount : NotFound("Game not found");
        }
    }
}
