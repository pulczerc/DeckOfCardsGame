using System.Collections.Concurrent;
using DeckOfCardsGame.DAL.Models;

namespace DeckOfCardsGame.DAL
{
    public static class PlayerData
    {
        public static List<Player> TempPlayers = new(new []
            {
                new Player() { PlayerId = Guid.NewGuid(), PlayerName = "Cs1"},
                new Player() { PlayerId = Guid.NewGuid(), PlayerName = "Cs2"},
                new Player() { PlayerId = Guid.NewGuid(), PlayerName = "Cs3"}
            }
        );

        public static ConcurrentDictionary<Guid, Player> Players = new( new []
        {
            KeyValuePair.Create(TempPlayers[0].PlayerId, TempPlayers[0]),
            KeyValuePair.Create(TempPlayers[1].PlayerId, TempPlayers[1]),
            KeyValuePair.Create(TempPlayers[2].PlayerId, TempPlayers[2])
        });
    }
}
