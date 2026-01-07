using ExternalAPIService.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using PokemonManager.Entity;
using PokemonManager.Interfaces;
using System.Text.Json;

namespace PokemonManager.Managers
{
    public class PokemonManagerClass : IPokemonManager
    {
        private readonly IPokeClientService _pokeApi;
        private readonly IMemoryCache _cache;
        private readonly IOpenAIClientService _openAIClientService;
        private readonly ILogger<PokemonManagerClass> _logger;

        public PokemonManagerClass(IPokeClientService pokeApi, IMemoryCache cache, IOpenAIClientService openAIClientService, ILogger<PokemonManagerClass> logger)
        {
            _pokeApi = pokeApi;
            _cache = cache;
            _openAIClientService = openAIClientService;
            _logger = logger;
        }

        /// <summary>
        /// Method to GetPokemonsList
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<List<Pokemon>> GetPokemonsAsync(int page)
        {
            try
            {
                _logger.LogInformation("Started Getting Pokemon list with details");
                List<Pokemon> pokemons = new();
                int offset = page == 0 ? 0 : (page - 1) * 10;
                var response = await _pokeApi.GetPokemonListAsync(offset);
                if (response != null && response.Results != null && response.Results.Count > 0)
                {
                    List<string> names = response.Results.Select(p => p.Name).ToList();
                    if (names != null)
                    {
                        foreach (string name in names)
                        {
                            var pokemon = await GetAndMapPokemonDetailsAsync(name);
                            if (pokemon != null)
                            {
                                pokemons.Add(pokemon);
                            }
                        }
                    }
                }
                _logger.LogInformation("Finally Got Pokemon list with details");
                return pokemons;
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error while getting pokemon lists");
                throw;
            }
        }

        /// <summary>
        /// Method To get and map the Pokemon details
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private async Task<Pokemon> GetAndMapPokemonDetailsAsync(string name)
        {
            Pokemon pokemon = new();
            PokemonServiceResponse pokemonServiceResponse = await GetPokemonServiceResponseAsync(name);
            if (pokemonServiceResponse != null)
            {
                var openAiResponse = await GetPokemonStoryFromAI(pokemonServiceResponse.Name);
                pokemon.Name = pokemonServiceResponse.Name;
                pokemon.Order = pokemonServiceResponse.Order;
                pokemon.Abilities = string.Join(',',pokemonServiceResponse.Abilities.Where(a => a.Ability != null).Select(a => a.Ability.Name).ToList());
                pokemon.Type = string.Join(',',pokemonServiceResponse.Types.Where(t => t.Type != null).Select(t => t.Type.Name).ToList());
                pokemon.PokemonStory = openAiResponse;
            }

            return pokemon;
        }

        /// <summary>
        /// Method which calls pokeapi service 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private async Task<PokemonServiceResponse> GetPokemonServiceResponseAsync(string name)
        {
            PokemonServiceResponse pokemonServiceResponse = new();
            var cacheKey = $"Pokemon_{name}".ToLower();
            pokemonServiceResponse = _cache.Get<PokemonServiceResponse>(cacheKey);
            if (pokemonServiceResponse == null)
            {
                _logger.LogInformation("Getting details from PokeAPi for: {Pokemon}", name);
                 pokemonServiceResponse = await _pokeApi.GetPokemonDataAsync(name);
                
                _cache.Set(cacheKey, pokemonServiceResponse);
                _logger.LogInformation("Got details from PokeAPi for: {Pokemon}", name);
            }
            return pokemonServiceResponse;
        }

        /// <summary>
        /// Method to Get Pokemon detail Response
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<PokemonEntity> GetPokemonDetailAsync(string name)
        {
            try
            {
                PokemonEntity pokemon = new();
                _logger.LogInformation("Searching pokemon Start: {Pokemon}", name);
                PokemonServiceResponse pokemonServiceResponse = await GetPokemonServiceResponseAsync(name);
                if (pokemonServiceResponse != null)
                {
                    var openAiResponse = await GetPokemonStoryFromAI(pokemonServiceResponse.Name);
                    pokemon.Name = pokemonServiceResponse.Name;
                    pokemon.img_url = pokemonServiceResponse.Sprites.back_default;
                    pokemon.PokemonStory = openAiResponse;
                }
                _logger.LogInformation("Searching pokemon Success End: {Pokemon}", name);
                return pokemon;
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error while searching pokemon");
                throw;
            }
        }

        private async Task<string> GetPokemonStoryFromAI(string pokemonName)
        {
            string response;
            var cacheKey = $"OpenAIResponse_Pokemon_{pokemonName}".ToLower();
            response = _cache.Get<string>(cacheKey);
            if (response == null)
            {
                string systemMessage = "You are a creative assistant who writes Pokémon stories.";
                string userMessage = $"Write a Pokémon story in max 500 characters about {pokemonName}.";
                _logger.LogInformation("Getting Story from AIChatclient for: {Pokemon}", pokemonName);
                response = await _openAIClientService.GenerateOpenAIResponseAsync(userMessage, systemMessage);
                _logger.LogInformation("Successfully got Story from AIChatclient for: {Pokemon}", pokemonName);
                _cache.Set(cacheKey, response);
            }
            return response;
        }
    }
}
