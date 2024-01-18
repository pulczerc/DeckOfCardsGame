using DeckOfCardsGame.DAL;
using DeckOfCardsGame.DAL.Models;

namespace DeckOfCardsGame.API.Services
{
    /// <summary>
    /// Service class for deck-related operations.
    /// </summary>
    public class DeckService : IDeckService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeckService"/> class.
        /// </summary>
        public DeckService()
        {
        }

        /// <inheritdoc />
        public Deck? CreateNewDeck()
        {
            var newDeck = Deck.CreateNewDeck();

            return DeckData.Decks.TryAdd(newDeck.Id, newDeck) ? newDeck : null;
        }

        /// <inheritdoc />
        public IEnumerable<Deck> GetAllDecks()
        {
            return DeckData.Decks.Values.ToList();
        }

        /// <inheritdoc />
        public Deck? GetDeckById(Guid deckId)
        {
            return DeckData.Decks.TryGetValue(deckId, out var deck) ? deck : null;
        }

        /// <inheritdoc />
        public bool AddDeck(Deck newDeck)
        {
            return DeckData.Decks.TryAdd(newDeck.Id, newDeck);
        }
    }
}