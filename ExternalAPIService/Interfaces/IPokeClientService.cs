using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalAPIService.Interfaces
{
    public interface IPokeClientService
    {
        Task<dynamic> GetPokemonListAsync(int offset);
        Task<dynamic> GetPokemonDataAsync(string name);
    }
}
