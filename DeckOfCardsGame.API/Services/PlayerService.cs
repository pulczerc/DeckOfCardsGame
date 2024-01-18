using DeckOfCardsGame.DAL.Models;
using DeckOfCardsGame.DAL;
using DeckOfCardsGame.API.Commands.Player;

namespace DeckOfCardsGame.API.Services
{
    /// <summary>
    /// Service for player-related operations.
    /// </summary>
    public class PlayerService : IPlayerService
    {
        /// <inheritdoc />
        public Player? GetPlayer(Guid playerId)
        {
            return PlayerData.Players.TryGetValue(playerId, out var player) ? player : null;
        }

        /// <inheritdoc />
        public IEnumerable<Player> GetAllPlayers()
        {
            return PlayerData.Players.Values;
        }

        /// <inheritdoc />
        public IEnumerable<Card>? GetPlayerCards(Guid playerId)
        {
            return PlayerData.Players.TryGetValue(playerId, out var player) ? player.OnHandCards : null;
        }

        /// <inheritdoc />
        public Player? CreatePlayer(CreatePlayerRequest createPlayerRequest)
        {
            var newPlayer = new Player() { PlayerName = createPlayerRequest.PlayerName ?? "Anonymous" };

            return PlayerData.Players.TryAdd(newPlayer.PlayerId, newPlayer) ? newPlayer : null;
        }

        /// <inheritdoc />
        public bool RemovePlayer(Guid playerId)
        {
            return PlayerData.Players.TryRemove(playerId, out _);
        }
    }
}