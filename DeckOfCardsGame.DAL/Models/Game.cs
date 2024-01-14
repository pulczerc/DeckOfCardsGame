using DeckOfCardsGame.Common.Extensions;

namespace DeckOfCardsGame.DAL.Models
{
    public class Game
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public List<Deck> Shoe { get; set; } = new();
        
        public List<Player> Players { get; set; } = new();
        
        public List<Card> GameDeckCards { get; set; } = new();

        public static Game CreateNewGame()
        {
            var newGame = new Game();
            return newGame;
        }
        public void AddDeck(Deck deck)
        {
            Shoe.Add(deck);
            AddDeckCards(deck);
        }
        public void Shuffle()
        {
            GameDeckCards.Shuffle();
        }

        public List<Card> DealCards(int number, Guid playerId)
        {
            var dealtCards = GameDeckCards.Take(number).ToList();
            //TODO: move to service
            if (PlayerData.Players.TryGetValue(playerId, out var player))
            {
                player.ReceiveCards(dealtCards);
            }

            GameDeckCards.RemoveRange(0, number);
            return dealtCards;
        }


        private void AddDeckCards(Deck deck)
        {
            GameDeckCards.AddRange(deck.DeckCards);
        }
    }
}
