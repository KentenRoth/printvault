# PrintVault

PrintVault is a backend system for organizing and managing 3D printing files (`.3mf`).  
It indexes files, stores metadata, and provides structured organization through categories and tags.

The goal of PrintVault is to make large collections of 3D print files searchable, structured, and easy to manage — whether stored locally, on a NAS, or in a containerized environment.

---

## 🎯 Vision

3D printing workflows often generate large libraries of sliced files that quickly become difficult to track. PrintVault is designed to provide:

- Structured organization
- Rich metadata storage
- Flexible tagging
- Fast lookup and filtering
- A scalable foundation for future UI integration

---

## 🧠 Core Concepts

**PrintFile**  
Represents a `.3mf` file and its associated metadata, including description, category, material, estimated print time, and thumbnail reference.

**Category**  
Provides structured grouping for print files (e.g., Functional, Miniatures, Tools).

**Tag**  
Allows flexible, user-defined labeling for search and filtering.

**PrintFileTag**  
Defines the many-to-many relationship between files and tags.

---

## 🏗 Architecture

PrintVault is built using:

- .NET 8
- ASP.NET Core
- Entity Framework Core
- PostgreSQL

The backend is designed with scalability and clean architecture principles in mind, allowing future expansion into background processing, metadata extraction services, and a frontend application.

---

## 📌 Long-Term Direction

Planned evolution of the project includes:

- Automatic `.3mf` ingestion
- Metadata and thumbnail extraction
- Search and filtering APIs
- Multi-user support
- Frontend interface for browsing and managing files

---

PrintVault is being developed as a structured, extensible foundation for managing 3D printing assets.
