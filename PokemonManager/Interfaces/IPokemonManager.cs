using PokemonManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonManager.Interfaces
{
    public interface IPokemonManager
    {
        Task<List<Pokemon>> GetPokemonsAsync(int page);
        Task<PokemonEntity> GetPokemonDetailAsync(string name);
    }
}
