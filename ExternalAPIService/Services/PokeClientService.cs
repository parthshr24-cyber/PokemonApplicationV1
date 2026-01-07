using ExternalAPIService.Interfaces;
using Microsoft.Extensions.Logging;
using PokemonManager.Entity;
using System.Net.Http.Json;

namespace ExternalAPIService.Services
{
    public class PokeClientService : IPokeClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PokeClientService> _logger;

        public PokeClientService(HttpClient httpClient, ILogger<PokeClientService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// Calling external Api to get Pokemon list
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async Task<PokemonListResponse> GetPokemonListAsync(int offset)
        {
            try
            {
                _logger.LogInformation("Started calling External List api for Pokemon");
                return await _httpClient.GetFromJsonAsync<PokemonListResponse>(
                    $"pokemon?limit=10&&offset={offset}");
            }
            catch(Exception ex) {
                _logger.LogError(ex, "Error while calling External List api for Pokemon");
                return null;
            }
        }

        /// <summary>
        /// Calling external Api to get Pokemon Details
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<PokemonServiceResponse> GetPokemonDataAsync(string name)
        {
            try
            {
                _logger.LogInformation("Started calling External details api for Pokemon");
                return await _httpClient.GetFromJsonAsync<PokemonServiceResponse>(
                    $"pokemon/{name}");
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error while calling External details api for Pokemon");
                return null;
            }
        }
    }
}
