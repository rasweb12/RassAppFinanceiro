# RassApp Financeiro

Ecossistema financeiro pessoal SaaS multi-tenant com suporte offline-first.

## Visão
- API .NET 10 central
- Web ASP.NET Core MVC
- Mobile .NET MAUI 10 (offline-first + sync)
- SQL Server (multi-tenant) + SQLite local
- Clean Architecture + DDD + Vertical Slice
- JWT + Refresh Token
- Futuro: IA financeira (Premium)

## Tecnologias
- Backend: .NET 10, ASP.NET Core, EF Core 9+, MediatR, FluentValidation, AutoMapper
- Mobile: .NET MAUI 10, CommunityToolkit.Mvvm, Refit, SQLite-net
- Infra: SQL Server, QuestPDF, ClosedXML

## Como rodar (local)
1. dotnet restore
2. cd src/RassApp.Finance.Infrastructure
3. dotnet ef database update --startup-project ../RassApp.Finance.Api
4. dotnet run --project ../RassApp.Finance.Api

## Roadmap
- Fase 1: Autenticação + Multi-tenant
- Fase 2: Entidades financeiras + CRUD
- Fase 3: Sincronização offline
- Fase 4: Mobile + Web UI
