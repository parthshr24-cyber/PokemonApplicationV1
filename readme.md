# Pokemon Web API – Intermediate Layer Project

## 📌 Project Overview
This project is a **.NET 8 Web API** designed as an **intermediate layer** between clients and the public **PokéAPI**.

It follows **clean architecture principles** with proper separation of concerns:
- Controllers handle HTTP requests
- Manager layer contains business logic
- External services handle third-party API calls (PokéAPI + AI)
- Caching and logging are applied
- Unit tests validate business logic

The project also includes a **bonus AI integration** to demonstrate how an AI agent can enrich Pokémon data.

---

## 🏗 Architecture Overview

```
Client
  ↓
Controllers (Web API)
  ↓
Manager Layer (Business Logic)
  ↓
External Services
  ├── PokéAPI Service
  └── Azure OpenAI Service (Bonus)
```

---

## 🧩 Solution Structure

```
Pokemon.Api                → Web API (Controllers, Program.cs)
PokemonManager             → Manager layer (Business logic)
ExternalAPIService         → External API integrations (PokéAPI, AI)
PokemonManager.Tests       → NUnit test project
```

---

## 🛠 Tech Stack

- .NET 8 Web API
- C#
- PokéAPI (public API)
- Azure OpenAI (Bonus – AI integration)
- IMemoryCache (in-memory caching)
- ILogger<T> (built-in logging)
- NUnit + Moq (unit testing)

---

## 🚀 Features

- Get Pokémon list
- Get Pokémon details by name
- Business logic encapsulated in manager layer
- External API calls isolated in service layer
- In-memory caching to improve performance
- Structured logging
- AI-generated Pokémon description (bonus)

---

## 🧠 Caching Strategy

- Implemented using `IMemoryCache`
- Cache key pattern:
  ```
  pokemon_{name}
  ```
- Reduces repeated calls to external APIs
- Cache expiration can be configured

---

## 📝 Logging Strategy

- Uses built-in `ILogger<T>`
- Logs are written to:
  - Visual Studio Output window (Debug)
  - Console (when running via `dotnet run`)

Log levels used:
- Information – normal flow
- Warning – unexpected but recoverable issues
- Error – exceptions

---

## 🤖 AI Integration (Bonus Section)

The project integrates an **AI agent** using **Azure OpenAI** to generate short descriptions or stories about Pokémon.

### Why AI Integration?
- Demonstrates extensibility of the system
- Adds enrichment beyond raw PokéAPI data
- Shows real-world integration with AI services

### AI Flow

```
Controller → Manager → Azure OpenAI Service
```

If AI fails, the system safely falls back without breaking the API.

---

## 🧪 Unit Testing

- Framework: **NUnit**
- Mocking: **Moq**
- Tests focus on **Manager layer only**
- External dependencies are mocked

### Example Tested Scenarios
- Pokémon exists → returns data
- Pokémon not found → returns null
- Cache hit → external API not called

---

## ▶️ How to Run the Project

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022+

### Steps
1. Open solution in Visual Studio
2. Set `Pokemon.Api` as startup project
3. Update `appsettings.json`
4. Run the project
5. Open Swagger UI

---

## ⚙ Configuration

### appsettings.json

```json
{
  "PokeApi": {
    "BaseUrl": "https://pokeapi.co/api/v2/"
  },
  "AzureOpenAI": {
    "Endpoint": "https://<your-resource>.openai.azure.com/",
    "ApiKey": "<your-api-key>"
  }
}
```

---

## 📌 Assumptions & Limitations

- PokéAPI availability is assumed
- In-memory cache resets on application restart
- AI integration requires Azure OpenAI subscription

---

## 🔮 Future Improvements

- Add persistent caching (Redis)
- Add authentication & authorization
- Add pagination & filtering
- Improve AI prompts
- Add Docker support

---

## 👤 Author

**Parth Sharma**  
.NET Web API Developer

---

## ✅ Conclusion

This project demonstrates:
- Clean architecture
- Proper layering
- External API integration
- Caching & logging
- Unit testing
- AI extensibility

It is designed to be **production-oriented** while remaining simple and maintainable.

