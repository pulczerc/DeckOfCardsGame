using DeckOfCardsGame.Common.Enums;

namespace DeckOfCardsGame.DAL.Models
{
    public class Deck
    {
        public static readonly int MaxNumberOfCards = 52;

        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public bool IsUsed { get; set; } = false;

        public List<Card> DeckCards { get; }
  

        private Deck()
        {
            DeckCards = new List<Card>();

            // Add 52 cards to the deck
            foreach (Suits suit in Enum.GetValues(typeof(Suits)))
            {
                foreach (FaceValues faceValue in Enum.GetValues(typeof(FaceValues)))
                {
                    DeckCards.Add(new Card((int)suit, (int)faceValue));
                }
            }
        }

        public static Deck CreateNewDeck()
        {
            return new Deck();
        }
    }
}
