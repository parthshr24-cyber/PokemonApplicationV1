using PokemonManager.Managers;
using PokemonManager.Interfaces;
using ExternalAPIService.Interfaces;
using ExternalAPIService.Services;
using Azure.AI.OpenAI;
using System.ClientModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IPokeClientService, PokeClientService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["PokeApi:BaseUrl"]);
});
builder.Services.AddSingleton(sp =>
{
    var client = new AzureOpenAIClient(
        new Uri(builder.Configuration["AzureOpenAI:Endpoint"]),
        new ApiKeyCredential(builder.Configuration["AzureOpenAI:ApiKey"]));

    // Return ChatClient for DI
    return client.GetChatClient(builder.Configuration["AzureOpenAI:DeploymentName"]);
});

builder.Services.AddMemoryCache();
builder.Services.AddScoped<IOpenAIClientService,OpenAIClientService>();
builder.Services.AddScoped<IPokemonManager, PokemonManagerClass>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
