using DeckOfCardsGame.API.Commands.Game;
using DeckOfCardsGame.API.Queries.Game;
using DeckOfCardsGame.API.Queries.Player;
using DeckOfCardsGame.API.Services;
using DeckOfCardsGame.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeckOfCardsGame.API.Controllers
{
    /// <summary>
    /// GameController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController( IGameService gameService)
        {
            _gameService = gameService;
        }


        [HttpGet("all-games")]
        public ActionResult<IEnumerable<Game>> GetAllGames()
        {
            return _gameService.GetAllGames().ToList();
        }

        [HttpGet("{gameId}")]
        public ActionResult<Game> GetGameById([FromRoute] Guid gameId)
        {
            var game = _gameService.GetGameById(gameId);
            return game != null ? game : NotFound("Game not found");
        }

        [HttpGet("{gameId}/decks")]
        public ActionResult<List<Deck>> GetDecksInGame([FromRoute] Guid gameId)
        {
            var decks = _gameService.GetDecksInGame(gameId);
            return decks != null ? decks : NotFound("Game not found");
        }

        [HttpGet("{gameId}/players")]
        public ActionResult<List<Player>> GetPlayersInGame([FromRoute] Guid gameId)
        {
            var players = _gameService.GetPlayersInGame(gameId);
            return players != null ? players : NotFound("Game not found");
        }

        [HttpGet("{gameId}/game-deck-cards")]
        public ActionResult<IEnumerable<Card>> GetGameDeckCards([FromRoute] Guid gameId)
        {
            var gameDeckCards = _gameService.GetGameDeckCards(gameId);
            return gameDeckCards != null ? gameDeckCards.ToList() : NotFound("Game not found");
        }

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

        [HttpDelete("{gameId}")]
        public ActionResult DeleteGame([FromRoute] Guid gameId)
        {
            return _gameService.DeleteGame(gameId)
                ? NoContent()
                : NotFound("Game not found");
        }

        [HttpPost("{gameId}/decks/{deckId}")]
        public ActionResult<List<Deck>> AddDeckToGame([FromRoute] Guid gameId, [FromRoute] Guid deckId)
        {
            var decks = _gameService.AddDeckToGame(gameId, deckId);
            return decks != null ? decks : NotFound();
        }

        [HttpPost("{gameId}/player/{playerId}")]
        public ActionResult<Player> AddPlayerToGame([FromRoute] Guid gameId, [FromRoute] Guid playerId)
        {
            var player = _gameService.AddPlayerToGame(gameId, playerId);
            return player != null ? player : NotFound("Player not found");
        }

        [HttpDelete("{gameId}/player/{playerId}")]
        public ActionResult RemovePlayerFromGame([FromRoute] Guid gameId, [FromRoute] Guid playerId)
        {
            return _gameService.RemovePlayerFromGame(gameId, playerId)
                ? NoContent()
                : NotFound("Player not found or game not found");
        }

        [HttpPost("{gameId}/shuffle")]
        public ActionResult<List<Card>> ShuffleGameDeck([FromRoute] Guid gameId)
        {
            var shuffledCards = _gameService.ShuffleGameDeck(gameId);
            return shuffledCards != null ? shuffledCards : NotFound("Game not found");
        }

        [HttpPost("{gameId}/deal-cards")]
        public ActionResult<List<Card>> DealCardsToPlayer([FromRoute] Guid gameId, [FromBody] DealCardsRequest request)
        {
            var dealtCards = _gameService.DealCardsToPlayer(gameId, request.PlayerId, request.NumberOfCardsToDeal);
            return dealtCards != null
                ? dealtCards
                : BadRequest("Unable to deal cards. Check if the game, player, and number of cards claim are valid.");
        }

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

        [HttpGet("{gameId}/remaining-cards-by-suit")]
        public ActionResult<List<RemainingCardsCountBySuit>> GetRemainingCardsCountBySuit([FromRoute] Guid gameId)
        {
            var remainingCardsBySuit = _gameService.GetRemainingCardsCountBySuit(gameId);
            return remainingCardsBySuit != null ? remainingCardsBySuit : NotFound("Game not found");
        }

        [HttpGet("{gameId}/remaining-cards-count")]
        public ActionResult<Dictionary<string, int>> GetRemainingCardsCount([FromRoute] Guid gameId)
        {
            var remainingCardsCount = _gameService.GetRemainingCardsCount(gameId);
            return remainingCardsCount != null ? remainingCardsCount : NotFound("Game not found");
        }
    }
}
