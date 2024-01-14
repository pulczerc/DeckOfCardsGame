using DeckOfCardsGame.API.Commands.Player;
using DeckOfCardsGame.DAL.Models;

namespace DeckOfCardsGame.API.Services
{
    public interface IPlayerService
    {
        Player? GetPlayer(Guid playerId);
        IEnumerable<Player> GetAllPlayers();
        IEnumerable<Card>? GetPlayerCards(Guid playerId);
        Player? CreatePlayer(CreatePlayerRequest createPlayerRequest);
        bool RemovePlayer(Guid playerId);
    }
}
