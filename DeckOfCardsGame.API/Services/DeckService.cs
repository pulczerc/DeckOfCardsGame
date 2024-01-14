using DeckOfCardsGame.DAL;
using DeckOfCardsGame.DAL.Models;

namespace DeckOfCardsGame.API.Services
{
    /// <summary>
    /// DeckService
    /// </summary>
    public class DeckService : IDeckService
    {
        public DeckService()
        {
        }

        public Deck? CreateNewDeck()
        {
            var newDeck = Deck.CreateNewDeck();
            
            return DeckData.Decks.TryAdd(newDeck.Id, newDeck) ? newDeck : null;
        }

        public IEnumerable<Deck> GetAllDecks()
        {
            return DeckData.Decks.Values.ToList();
        }

        public Deck? GetDeckById(Guid deckId)
        {
            return DeckData.Decks.TryGetValue(deckId, out var deck) ? deck : null;
        }

        public bool AddDeck(Deck newDeck)
        {
            return DeckData.Decks.TryAdd(newDeck.Id, newDeck);
        }
    }

}
