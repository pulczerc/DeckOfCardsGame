using DeckOfCardsGame.API.Services;
using DeckOfCardsGame.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeckOfCardsGame.API.Controllers
{
    /// <summary>
    /// DeckController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        private readonly IDeckService _deckService;

        public DeckController(IDeckService deckService)
        {
            ArgumentNullException.ThrowIfNull(deckService);

            _deckService = deckService; 
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Deck>))]
        [ProducesResponseType(404, Type = typeof(string))]

        public ActionResult<IEnumerable<Deck>> GetAllDecks()
        {
            return _deckService.GetAllDecks().ToList();
        }

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
