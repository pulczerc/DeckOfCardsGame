using DeckOfCardsGame.Common.Enums;

namespace DeckOfCardsGame.DAL.Models
{
    public class Card
    {
        public Suits Suit { get; set; }
        public FaceValues FaceValue { get; set; }

        public Card(int suit, int faceValue)
        {
            this.Suit = (Suits)suit;
            this.FaceValue = (FaceValues)faceValue;
        }
        public string Code =>
            FaceValue is >= FaceValues.Two and <= FaceValues.Ten 
                ? $"{Suit.ToString()[..1]}{(int)FaceValue}" // For numbers
                : $"{Suit.ToString()[..1]}{FaceValue.ToString()[..1]}"; // For Ace, Jack, Queen, and King

        public override string ToString()
        {
            return $"{FaceValue} of {Suit}";
        }
    }
}
