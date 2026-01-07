# Pokémon Web API – Assignment Submission

## 📌 Overview
This repository contains a **.NET 8 Web API solution** implemented as an **intermediate layer** between client applications and external services.

The project demonstrates a **clean, layered architecture** with explicit separation between:
- API layer
- Business logic
- External integrations
- Shared domain entities

The solution is intentionally pragmatic and aligned with the actual codebase (no assumed or idealized patterns).

---

## 🏗 Architecture Style

The solution follows a **Layered (N‑Tier) Architecture**.

```
Client
  ↓
Web API (Controllers)
  ↓
Manager / Application Layer
  ↓
External Integration Layer
```

A shared **Entities layer** is used to exchange models across layers.

---

## 🧩 Solution Structure

```
PokemonApplicationV1-master
│
├── PokemonApplication        → ASP.NET Core Web API (Controllers, Program.cs)
├── PokemonManager            → Manager / Business Logic layer
├── ExternalAPIService         → External API integrations (PokéAPI, AI)
├── Project.Entities           → Shared domain/entities & DTOs
├── PokemonManager.test        → NUnit test project
├── readme.md                  → Project documentation
```

---

## 🔹 Layer Responsibilities

### 1️⃣ PokemonApplication (API Layer)
- Application entry point
- Contains controllers only
- Handles HTTP requests and responses
- Configures dependency injection in `Program.cs`

No business logic is implemented in controllers.

---

### 2️⃣ PokemonManager (Manager / Application Layer)
- Central location for business logic
- Coordinates calls to external services
- Applies caching logic
- Handles fallback behavior when external services fail

This layer acts as the **orchestrator** of application flow.

---

### 3️⃣ ExternalAPIService (Integration Layer)
- Responsible for communication with third‑party APIs
- Contains:
  - PokéAPI client logic
  - Optional AI client logic
- Does not contain business rules

External failures are isolated from the rest of the application.

---

### 4️⃣ Project.Entities (Shared Models)
- Contains entity models / DTOs used across layers
- Prevents tight coupling between layers
- Acts as a common contract between Manager and External layers

---

### 5️⃣ PokemonManager.test (Test Layer)
- Unit tests for the Manager layer
- External services are mocked
- Focuses on verifying business behavior, not infrastructure

---

## 🛠 Technology Stack

- **.NET 8**
- **ASP.NET Core Web API**
- **IMemoryCache** for caching
- **ILogger<T>** for logging
- **NUnit** for unit testing
- **Moq** for mocking dependencies

---

## 🚀 Implemented Features

- Pokémon data retrieval via external API
- Business logic encapsulated in Manager layer
- In‑memory caching to avoid repeated external calls
- Logging using built‑in .NET logging abstractions
- Optional AI integration with graceful fallback

---

## 🧠 Caching Strategy

- Implemented using `IMemoryCache`
- Cache logic resides in the **Manager layer**
- Prevents redundant calls to external APIs
- Cache keys are based on Pokémon identifiers

---

## 📝 Logging Strategy

- Uses built‑in `ILogger<T>`
- Logs:
  - Application flow
  - Warnings when optional services are unavailable
  - Errors without crashing the API

Logs can be viewed in:
- Visual Studio Output window
- Console when running via `dotnet run`

---

## 🤖 AI Integration (Bonus)

- AI integration is **optional and non‑critical**
- Defensive configuration prevents startup or runtime failures
- If AI configuration is missing or invalid:
  - API continues to function normally
  - Manager layer applies fallback behavior

This ensures **graceful degradation**.

---

## 🧪 Unit Testing

- Tests are written using **NUnit**
- Focus exclusively on the Manager layer
- External dependencies are mocked

### Covered Scenarios
- Successful data retrieval
- Cache hit vs cache miss
- External API failure handling
- AI disabled / fallback behavior

---

## ▶️ How to Run the Project

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022+ or .NET CLI

### Steps

```bash
git clone <repository-url>
cd PokemonApplicationV1-master

dotnet restore
dotnet run
```

Swagger UI:
```
https://localhost:<port>/swagger
```

---

## ⚙ Configuration

Configuration is managed via `appsettings.json`.

Example:

```json
{
  "PokeApi": {
    "BaseUrl": "https://pokeapi.co/api/v2/"
  }
}
```

AI‑related configuration is optional and safely ignored if invalid.

---

## 📌 Architectural Notes

- Layered architecture chosen for clarity and maintainability
- Business logic isolated from infrastructure
- Shared entities used instead of leaking external models
- External integrations treated as replaceable components

---

## 👤 Author

**Parth Sharma**

---

## ✅ Conclusion

This project represents a clean and maintainable .NET Web API solution with:
- Clear separation of concerns
- Centralized business logic
- Safe external API integration
- Testable design

The architecture matches the current implementation and is suitable for assignment submission and technical discussion.

