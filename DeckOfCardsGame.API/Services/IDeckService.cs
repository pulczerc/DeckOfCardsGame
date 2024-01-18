using DeckOfCardsGame.DAL.Models;

namespace DeckOfCardsGame.API.Services
{
    /// <summary>
    /// Service interface for deck-related operations.
    /// </summary>
    public interface IDeckService
    {
        /// <summary>
        /// Creates a new deck.
        /// </summary>
        /// <returns>The newly created deck, or null if a deck with the same ID already exists.</returns>
        Deck? CreateNewDeck();

        /// <summary>
        /// Gets all decks.
        /// </summary>
        /// <returns>A collection of all available decks.</returns>
        IEnumerable<Deck> GetAllDecks();

        /// <summary>
        /// Gets a specific deck by its ID.
        /// </summary>
        /// <param name="deckId">The ID of the deck to retrieve.</param>
        /// <returns>The deck with the specified ID, or null if not found.</returns>
        Deck? GetDeckById(Guid deckId);

        /// <summary>
        /// Adds a new deck.
        /// </summary>
        /// <param name="newDeck">The deck to be added.</param>
        /// <returns>True if the deck was added successfully, false otherwise (e.g., a deck with the same ID already exists).</returns>
        bool AddDeck(Deck newDeck);

    }
}