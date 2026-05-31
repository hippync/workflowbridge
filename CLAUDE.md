# RiskLensAI — Agent Instructions

This file contains permanent instructions for coding agents (Claude Code, Codex, etc.) working in this repository.

Read this file before making any changes. Do not modify it unless explicitly asked.

## Project Summary

RiskLensAI is a .NET enterprise AI integration portfolio project.

V1 delivers one complete workflow: a credit risk analyst creates a case, uploads documents, runs an AI analysis, and reviews the audited result in a Blazor Server UI.

Keep v1 focused on the credit risk workflow. Do not add unrelated features.

## Solution Structure

```text
RiskLensAI.sln
src/
  RiskLensAI.Domain/         — enterprise concepts, no infrastructure dependencies
  RiskLensAI.Application/    — use cases, interfaces, DTOs, validation, orchestration
  RiskLensAI.Infrastructure/ — external SDKs, providers, parsers, persistence
  RiskLensAI.Web/            — Blazor UI, endpoints, auth stub, DI composition
tests/
  RiskLensAI.Application.Tests/
  RiskLensAI.Infrastructure.Tests/
```

## Architecture Rules

These rules are non-negotiable. Violating them breaks the architecture.

### Dependency direction

Dependencies must point inward only:

```
Web → Application → Domain
Infrastructure → Application → Domain
```

`Domain` must never reference `Infrastructure`, `Web`, EF Core, Blazor, Anthropic SDK, Bedrock SDK, or Azure OpenAI SDK.

`Application` must never reference `Infrastructure`, EF Core, Blazor, Anthropic SDK, Bedrock SDK, or Azure OpenAI SDK.

### Project responsibilities

**Domain** owns:
- entities and value objects (`CreditRiskCase`, `CreditRiskAssessment`, `DocumentUpload`, `AiAuditRecord`, etc.)
- enums (`RiskRating`, `ProviderType`, `DocumentType`, etc.)
- no external dependencies

**Application** owns:
- use cases and commands (`CreateCreditRiskCase`, `AnalyzeCreditRiskCase`, etc.)
- interfaces (`IAiProvider`, `IDocumentParser`, `IPolicyRetriever`, `IAuditLogger`)
- DTOs and request/response models (`AiRequest`, `AiResponse`, `AiMessage`, `AiUsage`)
- FluentValidation validators
- orchestration logic

**Infrastructure** owns:
- AI provider implementations (`MockAiProvider`, `RecordedAiProvider`, `AnthropicDirectAiProvider`)
- EF Core `DbContext` and repositories
- PostgreSQL persistence
- PDF parser (PdfPig)
- Excel parser (EPPlus)
- policy retrieval implementation
- Serilog and OpenTelemetry wiring

**Web** owns:
- Blazor Server pages and components
- API/health endpoints
- local development identity stub
- dependency injection composition (`Program.cs`)
- auth configuration seam

### AI provider boundary

All AI calls must go through `IAiProvider` defined in Application.

```csharp
public interface IAiProvider
{
    Task<AiResponse> CompleteAsync(AiRequest request, CancellationToken cancellationToken);
}
```

No provider-specific SDK types (`AnthropicClient`, `BedrockRuntimeClient`, etc.) may appear in Application or Domain.

Provider implementations live exclusively in Infrastructure.

### Logging rules

Do not log:
- API keys or secrets
- raw sensitive uploaded document contents
- full AI prompts by default

Safe to log:
- request hashes
- provider name and model name
- latency and token usage counts
- success/failure status
- error category

## Quality Gates

Run these commands to verify changes before reporting a task complete.

```bash
dotnet restore RiskLensAI.sln
dotnet build RiskLensAI.sln --no-restore -m:1
dotnet test RiskLensAI.sln --no-build -m:1
```

All three must succeed. If the local .NET SDK is not available, state that clearly rather than assuming success.

## Testing Rules

Tests must not require live AI provider calls (no real Anthropic API key, no real HTTP calls to external AI endpoints).

Use `MockAiProvider` or `RecordedAiProvider` for all test scenarios.

Use NSubstitute for interface mocking.

Use WireMock.Net for HTTP-level provider adapter tests.

## Agent Workflow Rules

1. Work on one issue at a time.
2. Start with the smallest testable vertical slice.
3. Use TDD when behavior is clear and the interface is stable.
4. Do not expand scope beyond the current issue.
5. Do not create abstractions not required by the current task.
6. Do not introduce new NuGet packages without stating the reason and confirming it is not already available.
7. Stop before committing unless the user explicitly asks you to commit.
8. When done, summarize: files changed, behavior added, verification commands run and their outcome.

## Package Constraints

Do not add new NuGet packages without justification.

Packages already planned for the project (add only when the relevant issue is in scope):

- `Serilog.AspNetCore`
- `FluentValidation.AspNetCore`
- `PdfPig`
- `EPPlus`
- `xunit`, `xunit.runner.visualstudio`
- `NSubstitute`
- `WireMock.Net`
- `Microsoft.EntityFrameworkCore`, `Npgsql.EntityFrameworkCore.PostgreSQL`
- Anthropic SDK or a small direct HTTP adapter

## V1 Scope Boundary

V1 is the credit risk workflow only. Do not add:
- Bedrock provider
- Azure OpenAI provider
- Entra ID
- multi-tenancy
- complex admin dashboards
- production cloud deployment
- advanced approval workflows
- pgvector (defer unless the retrieval step stays simple)

## Key Files

| File | Purpose |
|------|---------|
| `docs/architecture.md` | Project architecture plan |
| `docs/roadmap.md` | Phased delivery plan and agentic workflow stages |
| `docs/agentic-workflow.md` | Agent task format and workflow guidance |
| `docs/quality-gates.md` | Quality gate commands and criteria |
| `docs/github-issues.md` | Full issue backlog |
