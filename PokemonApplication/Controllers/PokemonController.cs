using Microsoft.AspNetCore.Mvc;
using PokemonManager.Interfaces;

namespace PokemonApplication.Controllers
{
    [ApiController]
    [Route("pokemon")]
    public class PokemonController : Controller
    {
        private readonly IPokemonManager _manager;

        public PokemonController(IPokemonManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Get list of Pokemons
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet("list/pokemons")]
        public async Task<IActionResult> Get([FromQuery] int page = 1)
        {
            return Ok(await _manager.GetPokemonsAsync(page));
        }

        /// <summary>
        /// Get details of specific Pokemon
        /// </summary>
        /// <param name="nameOrId"></param>
        /// <returns></returns>
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string nameOrId)
        {
            return Ok(await _manager.GetPokemonDetailAsync(nameOrId));
        }
    }
}
