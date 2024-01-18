using DeckOfCardsGame.API.Queries.Game;
using DeckOfCardsGame.API.Queries.Player;
using DeckOfCardsGame.DAL.Models;
using System;
using System.Collections.Generic;

namespace DeckOfCardsGame.API.Services
{
    /// <summary>
    /// Service for game-related operations.
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Gets all games.
        /// </summary>
        IEnumerable<Game> GetAllGames();

        /// <summary>
        /// Gets a game by its ID.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <returns>The game if found; otherwise, null.</returns>
        Game? GetGameById(Guid gameId);

        /// <summary>
        /// Gets the decks in a game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <returns>The list of decks in the game if found; otherwise, null.</returns>
        List<Deck>? GetDecksInGame(Guid gameId);

        /// <summary>
        /// Gets the players in a game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <returns>The list of players in the game if found; otherwise, null.</returns>
        List<Player>? GetPlayersInGame(Guid gameId);

        /// <summary>
        /// Gets the cards in the game deck.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <returns>The cards in the game deck if found; otherwise, null.</returns>
        IEnumerable<Card>? GetGameDeckCards(Guid gameId);

        /// <summary>
        /// Creates a new game.
        /// </summary>
        /// <returns>The created game if successful; otherwise, null.</returns>
        Game? CreateGame();

        /// <summary>
        /// Deletes a game by its ID.
        /// </summary>
        /// <param name="gameId">The ID of the game to delete.</param>
        /// <returns>True if the game is deleted; otherwise, false.</returns>
        bool DeleteGame(Guid gameId);

        /// <summary>
        /// Adds a deck to a game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <param name="deckId">The ID of the deck to add.</param>
        /// <returns>The updated list of decks in the game if successful; otherwise, null.</returns>
        List<Deck>? AddDeckToGame(Guid gameId, Guid deckId);

        /// <summary>
        /// Adds a player to a game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <param name="playerId">The ID of the player to add.</param>
        /// <returns>The added player if successful; otherwise, null.</returns>
        Player? AddPlayerToGame(Guid gameId, Guid playerId);

        /// <summary>
        /// Removes a player from a game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <param name="playerId">The ID of the player to remove.</param>
        /// <returns>True if the player is removed; otherwise, false.</returns>
        bool RemovePlayerFromGame(Guid gameId, Guid playerId);

        /// <summary>
        /// Shuffles the deck in a game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <returns>The shuffled list of cards if successful; otherwise, null.</returns>
        List<Card>? ShuffleGameDeck(Guid gameId);

        /// <summary>
        /// Deals cards to a player in a game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <param name="playerId">The ID of the player.</param>
        /// <param name="numberOfCardsToDeal">The number of cards to deal.</param>
        /// <returns>The dealt cards if successful; otherwise, null.</returns>
        List<Card>? DealCardsToPlayer(Guid gameId, Guid playerId, int numberOfCardsToDeal);

        /// <summary>
        /// Gets players with their hand values in a game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <returns>The list of players with hand values if successful; otherwise, null.</returns>
        List<PlayerHandValue>? GetPlayersWithHandValues(Guid gameId);

        /// <summary>
        /// Gets the remaining cards count by suit in a game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <returns>The list of remaining cards count by suit if successful; otherwise, null.</returns>
        List<RemainingCardsCountBySuit>? GetRemainingCardsCountBySuit(Guid gameId);

        /// <summary>
        /// Gets the remaining cards count in a game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <returns>The dictionary of remaining cards count if successful; otherwise, null.</returns>
        Dictionary<string, int>? GetRemainingCardsCount(Guid gameId);
    }
}
