using ExternalAPIService.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PokemonManager.Managers;
using System.Text.Json;

namespace PokemonManager.test.PokemonManagerTests
{
    [TestFixture]
    public class PokemonManagerTest
    {
        private Mock<IPokeClientService> _pokeApiMock;
        private Mock<IMemoryCache> _memoryCache;
        private Mock<IOpenAIClientService> _aiServiceMock;
        private Mock<ILogger<PokemonManagerClass>> _loggerMock;
        private PokemonManagerClass _manager;

        [SetUp]
        public void Setup()
        {
            _pokeApiMock = new Mock<IPokeClientService>();
            _memoryCache = new Mock<IMemoryCache>();
            _aiServiceMock = new Mock<IOpenAIClientService>();
            _loggerMock = new Mock<ILogger<PokemonManagerClass>>();

            _manager = new PokemonManagerClass(
                _pokeApiMock.Object,
                _memoryCache.Object,
                _aiServiceMock.Object,
                _loggerMock.Object
            );
        }

        [Test]
        public async Task GetPokemonDetailAsync_Test()
        {
            var json = """
             {
                 "id": 25,
                 "name": "pikachu",
                 "order": 35,
                 "abilities": [
                     { "ability": { "name": "static" } },
                     { "ability": { "name": "lightning-rod" } }
                 ],
                 "types": [
                     { "type": { "name": "electric" } }
                 ],
                 "sprites": {
                     "front_default": "https://example.com/pokemon/25/front.png",
                     "back_default": "https://example.com/pokemon/25/back.png"
                 }
             }
             """;

            JsonElement jsonElement = CreateJsonElement(json);

            _pokeApiMock
                .Setup(x => x.GetPokemonDataAsync("pikachu"))
                .ReturnsAsync(jsonElement);
            _memoryCache.Setup(x => x.CreateEntry(It.IsAny<object>())).Returns(Mock.Of<ICacheEntry>);
            string systemMessage = "You are a creative assistant who writes Pokémon stories.";
            string userMessage = $"Write a Pokémon story in max 500 characters about pikachu.";
            _aiServiceMock
                .Setup(x => x.GenerateOpenAIResponseAsync(userMessage, systemMessage))
                .ReturnsAsync("Electric mouse pokemon");

            var result = await _manager.GetPokemonDetailAsync("pikachu");

            ClassicAssert.IsNotNull(result);
        }

        [Test]
        public async Task GetPokemonsAsync_Test()
        {
            var jsonList = """
             {
                 "count": 1350,
             "next": "https://pokeapi.co/api/v2/pokemon?offset=10&limit=10",
             "previous": null,
             "results": [
                 {
                     "name": "pikachu",
                     "url": "https://pokeapi.co/api/v2/pokemon/1/"
                 }                
             ]
             }
             """;

            var json = """
             {
                 "id": 25,
                 "name": "pikachu",
                 "order": 35,
                 "abilities": [
                     { "ability": { "name": "static" } },
                     { "ability": { "name": "lightning-rod" } }
                 ],
                 "types": [
                     { "type": { "name": "electric" } }
                 ],
                 "sprites": {
                     "front_default": "https://example.com/pokemon/25/front.png",
                     "back_default": "https://example.com/pokemon/25/back.png"
                 }
             }
             """;

            JsonElement jsonElement = CreateJsonElement(json);
            JsonElement jsonListElement = CreateJsonElement(jsonList);

            _pokeApiMock
                .Setup(x => x.GetPokemonListAsync(0))
                .ReturnsAsync(jsonListElement);
            _pokeApiMock
                .Setup(x => x.GetPokemonDataAsync("pikachu"))
                .ReturnsAsync(jsonElement);
            _memoryCache.Setup(x => x.CreateEntry(It.IsAny<object>())).Returns(Mock.Of<ICacheEntry>);
            string systemMessage = "You are a creative assistant who writes Pokémon stories.";
            string userMessage = $"Write a Pokémon story in max 500 characters about pikachu.";
            _aiServiceMock
                .Setup(x => x.GenerateOpenAIResponseAsync(userMessage, systemMessage))
                .ReturnsAsync("Electric mouse pokemon");

            var result = await _manager.GetPokemonsAsync(1);

            ClassicAssert.IsNotNull(result);
            ClassicAssert.IsTrue(result.Count > 0);
        }

        private static JsonElement CreateJsonElement(string json)
        {
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.Clone();
        }
    }
}
