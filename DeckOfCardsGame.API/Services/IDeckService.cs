using DeckOfCardsGame.DAL.Models;

namespace DeckOfCardsGame.API.Services
{
    public interface IDeckService
    {
        Deck? CreateNewDeck();
        IEnumerable<Deck> GetAllDecks();
        Deck? GetDeckById(Guid deckId);
        bool AddDeck(Deck newDeck);
        // Add other methods as needed
    }
}
