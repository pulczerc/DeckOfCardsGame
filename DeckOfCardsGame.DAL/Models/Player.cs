namespace DeckOfCardsGame.DAL.Models
{
    public class Player
    {
        public Guid PlayerId { get; set; } = Guid.NewGuid();

        public string PlayerName { get; set; } = "Anonymous";

        public List<Card> OnHandCards { get; set; } = new();


        public override string ToString()
        {
            return $"{PlayerName} ({PlayerId})";
        }

        public int CalculateHandValue()
        {
            // All cards have a numeric value equivalent to their face value
            return OnHandCards.Sum(card => (int)card.FaceValue);
        }

        public void ReceiveCards(IEnumerable<Card> cards)
        {
            OnHandCards.AddRange(cards);
        }
    }
}
