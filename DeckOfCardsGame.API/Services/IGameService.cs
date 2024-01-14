using DeckOfCardsGame.API.Queries.Game;
using DeckOfCardsGame.API.Queries.Player;
using DeckOfCardsGame.DAL.Models;

namespace DeckOfCardsGame.API.Services
{
    public interface IGameService
    {
        IEnumerable<Game> GetAllGames();
        Game? GetGameById(Guid gameId);
        List<Deck>? GetDecksInGame(Guid gameId);
        List<Player>? GetPlayersInGame(Guid gameId);
        IEnumerable<Card>? GetGameDeckCards(Guid gameId);
        Game? CreateGame();
        bool DeleteGame(Guid gameId);
        List<Deck>? AddDeckToGame(Guid gameId, Guid deckId);
        Player? AddPlayerToGame(Guid gameId, Guid playerId);
        bool RemovePlayerFromGame(Guid gameId, Guid playerId);
        List<Card>? ShuffleGameDeck(Guid gameId);
        List<Card>? DealCardsToPlayer(Guid gameId, Guid playerId, int numberOfCardsToDeal);
        List<PlayerHandValue>? GetPlayersWithHandValues(Guid gameId);
        List<RemainingCardsCountBySuit>? GetRemainingCardsCountBySuit(Guid gameId);
        Dictionary<string, int>? GetRemainingCardsCount(Guid gameId);
    }
}
