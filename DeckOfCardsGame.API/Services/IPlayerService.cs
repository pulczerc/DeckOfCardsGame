using DeckOfCardsGame.API.Commands.Player;
using DeckOfCardsGame.DAL.Models;

namespace DeckOfCardsGame.API.Services
{
    /// <summary>
    /// Service interface for player-related operations.
    /// </summary>
    public interface IPlayerService
    {
        /// <summary>
        /// Gets a player by their ID.
        /// </summary>
        /// <param name="playerId">The ID of the player to retrieve.</param>
        /// <returns>The player with the specified ID, or null if not found.</returns>
        Player? GetPlayer(Guid playerId);

        /// <summary>
        /// Gets all players.
        /// </summary>
        /// <returns>A collection of all available players.</returns>
        IEnumerable<Player> GetAllPlayers();

        /// <summary>
        /// Gets the cards held by a player.
        /// </summary>
        /// <param name="playerId">The ID of the player whose cards to retrieve.</param>
        /// <returns>The cards held by the player, or null if the player is not found.</returns>
        IEnumerable<Card>? GetPlayerCards(Guid playerId);

        /// <summary>
        /// Creates a new player.
        /// </summary>
        /// <param name="createPlayerRequest">The details for creating a new player.</param>
        /// <returns>The newly created player, or null if a player with the same ID already exists.</returns>
        Player? CreatePlayer(CreatePlayerRequest createPlayerRequest);

        /// <summary>
        /// Removes a player by their ID.
        /// </summary>
        /// <param name="playerId">The ID of the player to be removed.</param>
        /// <returns>True if the player was removed successfully, false otherwise (e.g., player not found).</returns>
        bool RemovePlayer(Guid playerId);
    }
}