using DeckOfCardsGame.API.Services;
using DeckOfCardsGame.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeckOfCardsGame.API.Controllers
{
    /// <summary>
    /// Controller for managing deck-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        private readonly IDeckService _deckService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeckController"/> class.
        /// </summary>
        /// <param name="deckService">The service for deck operations.</param>
        public DeckController(IDeckService deckService)
        {
            ArgumentNullException.ThrowIfNull(deckService);

            _deckService = deckService; 
        }

        /// <summary>
        /// Gets all decks.
        /// </summary>
        /// <returns>A list of all available decks.</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Deck>))]
        [ProducesResponseType(404, Type = typeof(string))]
        public ActionResult<IEnumerable<Deck>> GetAllDecks()
        {
            return _deckService.GetAllDecks().ToList();
        }

        /// <summary>
        /// Gets a specific deck by its ID.
        /// </summary>
        /// <param name="deckId">The ID of the deck to retrieve.</param>
        /// <returns>The deck with the specified ID.</returns>
        [HttpGet("{deckId}")]
        [ProducesResponseType(200, Type = typeof(Deck))]
        [ProducesResponseType(404, Type = typeof(string))]
        public ActionResult<Deck> GetDeckById([FromRoute] Guid deckId)
        {
            var deck = _deckService.GetDeckById(deckId);
            if (deck == null)
            {
                return NotFound("Deck not found");
            }

            return deck;
        }

        /// <summary>
        /// Creates a new deck.
        /// </summary>
        /// <returns>The newly created deck.</returns>
        [HttpPost("decks")]
        [ProducesResponseType(201, Type = typeof(Deck))]
        [ProducesResponseType(400, Type = typeof(string))]
        public ActionResult<Deck> CreateDeck()
        {
            var deck = _deckService.CreateNewDeck();
            if (deck == null)
            {
                return BadRequest("Deck with the same ID already exists.");
            }

            return CreatedAtAction(nameof(GetDeckById), new { deckId = deck.Id }, deck);
        }
    }
}
