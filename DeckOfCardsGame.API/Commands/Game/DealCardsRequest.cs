namespace DeckOfCardsGame.API.Commands.Game
{
    public class DealCardsRequest
    {
        public Guid PlayerId{ get; set; }

        public int NumberOfCardsToDeal{ get; set; }
    }
}
