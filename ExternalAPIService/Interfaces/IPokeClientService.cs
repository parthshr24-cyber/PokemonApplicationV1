using PokemonManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalAPIService.Interfaces
{
    public interface IPokeClientService
    {
        Task<PokemonListResponse> GetPokemonListAsync(int offset);
        Task<PokemonServiceResponse> GetPokemonDataAsync(string name);
    }
}
