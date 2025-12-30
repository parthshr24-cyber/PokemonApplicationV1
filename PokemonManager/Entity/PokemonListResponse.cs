namespace PokemonManager.Entity
{
    public class PokemonListResponse
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<PokemonItem> Results { get; set; }
    }

    public class PokemonItem
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
