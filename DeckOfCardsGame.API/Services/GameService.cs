using DeckOfCardsGame.DAL.Models;
using DeckOfCardsGame.DAL;
using DeckOfCardsGame.API.Queries.Player;
using DeckOfCardsGame.API.Queries.Game;

namespace DeckOfCardsGame.API.Services
{
    /// <summary>
    /// Service for game-related operations.
    /// </summary>
    public class GameService : IGameService
    {
        /// <inheritdoc />
        public IEnumerable<Game> GetAllGames()
        {
            return GameData.Games.Values.ToList();
        }

        /// <inheritdoc />
        public Game? GetGameById(Guid gameId)
        {
            return GameData.Games.TryGetValue(gameId, out var game) ? game : null;
        }

        /// <inheritdoc />
        public List<Deck>? GetDecksInGame(Guid gameId)
        {
            return GameData.Games.TryGetValue(gameId, out var game) ? game.Shoe : null;
        }

        /// <inheritdoc />
        public List<Player>? GetPlayersInGame(Guid gameId)
        {
            return GameData.Games.TryGetValue(gameId, out var game) ? game.Players : null;
        }

        /// <inheritdoc />
        public IEnumerable<Card>? GetGameDeckCards(Guid gameId)
        {
            return GameData.Games.TryGetValue(gameId, out var game) ? game.GameDeckCards : null;
        }

        /// <inheritdoc />
        public Game? CreateGame()
        {
            var game = Game.CreateNewGame();
            return GameData.Games.TryAdd(game.Id, game) ? game : null;
        }

        /// <inheritdoc />
        public bool DeleteGame(Guid gameId)
        {
            return GameData.Games.TryRemove(gameId, out _);
        }

        /// <inheritdoc />
        public List<Deck>? AddDeckToGame(Guid gameId, Guid deckId)
        {
            //Game not found
            if (!GameData.Games.TryGetValue(gameId, out var game)) return null;

            //Deck Id not found
            if (!DeckData.Decks.ContainsKey(deckId))
            {
                return null;
            }

            //Deck already added
            if (game.Shoe.Exists(d => d.Id == deckId))
            {
                return null;
            }
            
            //Deck not found
            if (!DeckData.Decks.TryGetValue(deckId, out var deck)) return null;

            //Deck is already in use
            if (deck.IsUsed)
            {
                return null;
            }

            game.AddDeck(deck);
            deck.IsUsed = true;

            return game.Shoe;
        }

        /// <inheritdoc />
        public Player? AddPlayerToGame(Guid gameId, Guid playerId)
        {
            //Game not found
            if (!GameData.Games.TryGetValue(gameId, out var game)) return null;

            //Player Id not found
            if (!PlayerData.Players.ContainsKey(playerId))
            {
                return null;
            }

            //Player already seats in this game
            if (game.Players.Exists(p => p.PlayerId == playerId))
            {
                return null;
            }

            var player = PlayerData.Players[playerId];
            game.Players.Add(player);

            return player;
        }

        /// <inheritdoc />
        public bool RemovePlayerFromGame(Guid gameId, Guid playerId)
        {
            //Game not found
            if (!GameData.Games.TryGetValue(gameId, out var game)) return false;

            var playerToRemove = game.Players.FirstOrDefault(p => p.PlayerId == playerId);
            
            //Player not found in the game
            if (playerToRemove == null) return false;

            playerToRemove.OnHandCards.Clear();
            game.Players.Remove(playerToRemove);
            
            return true;
        }

        /// <inheritdoc />
        public List<Card>? ShuffleGameDeck(Guid gameId)
        {
            //Game not found
            if (!GameData.Games.TryGetValue(gameId, out var game)) return null;

            game.Shuffle();

            return game.GameDeckCards;
        }

        /// <inheritdoc />
        public List<Card>? DealCardsToPlayer(Guid gameId, Guid playerId, int numberOfCardsToDeal)
        {
            //Game not found
            if (!GameData.Games.TryGetValue(gameId, out var game)) return null;

            //Player Id not found
            if (!PlayerData.Players.ContainsKey(playerId) || !game.Players.Exists(player => player.PlayerId == playerId))
            {
                return null;
            }

            //Invalid numberOfCardsToDeal. Must be a positive integer. || Insufficient cards in the game deck to deal the specified number.
            if (numberOfCardsToDeal <= 0 || numberOfCardsToDeal > game.GameDeckCards.Count)
            {
                return null;
            }

            var dealtCards = game.DealCards(numberOfCardsToDeal, playerId);
            
            return dealtCards.Count > 0 ? dealtCards : null;
        }
        
        /// <inheritdoc />
        public List<PlayerHandValue>? GetPlayersWithHandValues(Guid gameId)
        {
            if (!GameData.Games.TryGetValue(gameId, out var game)) return null;

            var playersHandValues = game.Players
                .Select(player => new PlayerHandValue
                {
                    PlayerId = player.PlayerId,
                    PlayerName = player.PlayerName,
                    HandValue = player.CalculateHandValue()
                })
                .OrderByDescending(player => player.HandValue)
                .ToList();

            return playersHandValues;
        }

        /// <inheritdoc />
        public List<RemainingCardsCountBySuit>? GetRemainingCardsCountBySuit(Guid gameId)
        {
            if (!GameData.Games.TryGetValue(gameId, out var game)) return null;

            var remainingCardsBySuit = game.GameDeckCards
                .GroupBy(card => card.Suit)
                .Select(group => new RemainingCardsCountBySuit { Suit = group.Key.ToString(), CardLeft = group.Count() })
                .OrderBy(x => x.Suit)
                .ToList();

            return remainingCardsBySuit;
        }

        /// <inheritdoc />
        public Dictionary<string, int>? GetRemainingCardsCount(Guid gameId)
        {
            if (!GameData.Games.TryGetValue(gameId, out var game)) return null;

            var remainingCardsCount = game.GameDeckCards
                .GroupBy(card => new { card.Suit, card.FaceValue })
                .OrderBy(group => group.Key.Suit)
                .ThenByDescending(group => (int)group.Key.FaceValue)
                .ToDictionary(
                    group => $"{group.Key.Suit} - {group.Key.FaceValue}",
                    group => group.Count()
                );

            return remainingCardsCount;
        }
    }
}
