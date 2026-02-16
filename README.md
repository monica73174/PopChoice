# PopChoice .NET – AI Movie Recommender

A .NET port of the PopChoice solo project from Scrimba's AI Engineering Path.  
Uses **embeddings** + **vector search** + **RAG** (Retrieval-Augmented Generation) to deliver personalized movie recommendations powered by an LLM (e.g., OpenAI, Groq, or local Ollama).

![Demo GIF](https://via.placeholder.com/800x400?text=Add+Your+GIF+Here)  
*(Replace with a real GIF: Record a short demo in Loom/ScreenToGif showing a query like "funny 80s sci-fi movies" → results → LLM recs)*

## Features
- Semantic search over movie data using vector embeddings
- RAG pipeline to generate reasoned, hallucination-reduced recommendations
- Streaming responses from LLM
- [Add any extras: multi-LLM support, auth, etc.]

## Tech Stack
- **Backend**: ASP.NET Core (Minimal APIs or MVC)
- **AI**: Semantic Kernel (or direct OpenAI API calls)
- **Embeddings**: OpenAI / alternatives
- **Vector Store**: [e.g., PostgreSQL + pgvector, Chroma, in-memory]
- **Frontend**: [React + Vite / Blazor / Razor Pages]
- **Testing**: xUnit (via PopChoice.Test)

## Setup & Run Locally
1. Clone the repo:
