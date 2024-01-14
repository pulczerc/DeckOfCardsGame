using System.Collections.Concurrent;
using DeckOfCardsGame.DAL.Models;

namespace DeckOfCardsGame.DAL
{
    public static class DeckData
    {
        public static List<Deck> TempDecks { get; } = new List<Deck>
        {
            Deck.CreateNewDeck(),
            Deck.CreateNewDeck(),
            Deck.CreateNewDeck()
        };

        public static ConcurrentDictionary<Guid, Deck> Decks { get; } = new ConcurrentDictionary<Guid, Deck>
        {
            [TempDecks[0].Id] = TempDecks[0],
            [TempDecks[1].Id] = TempDecks[1],
            [TempDecks[2].Id] = TempDecks[2]
        };
    }
}
