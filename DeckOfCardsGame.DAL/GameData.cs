using System.Collections.Concurrent;
using DeckOfCardsGame.DAL.Models;

namespace DeckOfCardsGame.DAL
{
    public static class GameData
    {
        public static ConcurrentDictionary<Guid, Game> Games = new();
    }
}
