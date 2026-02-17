# PopChoice .NET – AI Movie Recommender

A .NET port of the PopChoice solo project from Scrimba's AI Engineering Path.  
Uses **embeddings** + **vector search** + **RAG** (Retrieval-Augmented Generation) to deliver personalized movie recommendations powered by an LLM (e.g., OpenAI, Groq, or local Ollama).


## Features
- Semantic search over movie data using vector embeddings
- RAG pipeline to generate reasoned, hallucination-reduced recommendations
- Streaming responses from LLM
- [Add any extras: multi-LLM support, auth, etc.]

## Tech Stack
- **Backend**: ASP.NET Core (Minimal APIs or MVC)
- **AI**: Semantic Kernel (or direct OpenAI API calls)
- **Embeddings**: OpenAI
- **Vector Store**: [e.g., PostgreSQL + pgvector, Chroma, in-memory]
- **Frontend**: [Razor Pages]
- **Testing**: xUnit (via PopChoice.Test)

## Setup & Run Locally
1. Clone the repo:

2. Restore & build:
   dotnet restore
   dotnet build

3. Set API keys (in `appsettings.json`, user secrets, or env vars):
- `OpenAI:ApiKey=sk-...` (or equivalent for Groq/etc.)

4. Run backend:
