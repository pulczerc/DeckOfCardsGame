namespace DeckOfCardsGame.API.Queries.Player
{
    public class PlayerHandValue
    {
        public Guid PlayerId { get; set; }
        public string? PlayerName { get; set; }
        public int HandValue { get; set; }
    }
}
