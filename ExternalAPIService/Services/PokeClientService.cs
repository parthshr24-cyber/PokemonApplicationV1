using ExternalAPIService.Interfaces;
using System.Net.Http.Json;

namespace ExternalAPIService.Services
{
    public class PokeClientService : IPokeClientService
    {
        private readonly HttpClient _httpClient;

        public PokeClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Calling external Api to get Pokemon list
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async Task<dynamic> GetPokemonListAsync(int offset)
        {
            var result = await _httpClient.GetFromJsonAsync<dynamic>(
                $"pokemon?limit=10&&offset={offset}");
            return result;
        }

        /// <summary>
        /// Calling external Api to get Pokemon Details
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<dynamic> GetPokemonDataAsync(string name)
        {
            var result = await _httpClient.GetFromJsonAsync<dynamic>(
                $"pokemon/{name}");
            return result;
        }
    }
}
