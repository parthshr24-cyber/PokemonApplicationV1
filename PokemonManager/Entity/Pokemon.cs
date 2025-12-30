namespace PokemonManager.Entity
{
    public class Pokemon
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public string Abilities { get; set; }
        public string Type { get; set; }

        public string PokemonStory { get; set; }
    }
    public class PokemonEntity
    {
        public string Name { get; set; }
        public string img_url { get; set; }
        public string PokemonStory { get; set; }

    }
    public class PokemonServiceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public Sprites Sprites { get; set; }
        public List<PokemonAbility> Abilities { get; set; }
        public List<PokemonType> Types { get; set; }
    }
    public class Sprites 
    { 
       public string back_default { get; set; }
    }
    public class PokemonAbility
    {
        public CommonEntity Ability { get; set; }
    }
    public class PokemonType
    {
        public CommonEntity Type { get; set; }
    }
    public class CommonEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
