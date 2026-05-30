# Architecture Plan

## Goal

Design WorkflowBridge as a small but credible enterprise .NET application that proves AI can be integrated safely into a business workflow.

The architecture should optimize for:

- testability;
- provider replaceability;
- auditability;
- deterministic demos;
- clear project boundaries;
- fast MVP delivery.

## Proposed Solution Structure

```text
WorkflowBridge.sln
src/
  WorkflowBridge.Domain/
  WorkflowBridge.Application/
  WorkflowBridge.Infrastructure/
  WorkflowBridge.Web/
tests/
  WorkflowBridge.Application.Tests/
  WorkflowBridge.Infrastructure.Tests/
```

## Project Responsibilities

### WorkflowBridge.Domain

Contains enterprise concepts with no infrastructure dependencies.

Initial candidates:

- `CreditRiskCase`
- `CreditRiskAssessment`
- `DocumentUpload`
- `PolicyReference`
- `AiAuditRecord`
- value objects and enums for case status, risk rating, provider type, and document type

### WorkflowBridge.Application

Contains use cases, interfaces, validators, DTOs, and orchestration.

Initial candidates:

- `CreateCreditRiskCase`
- `UploadCreditDocument`
- `AnalyzeCreditRiskCase`
- `GetAuditHistory`
- `IAiProvider`
- `IDocumentParser`
- `IPolicyRetriever`
- `IAuditLogger`
- FluentValidation validators

### WorkflowBridge.Infrastructure

Contains external systems and technical implementations.

Initial candidates:

- EF Core DbContext and repositories
- PostgreSQL persistence
- PDF parser with PdfPig
- Excel parser with EPPlus
- `MockAiProvider`
- `RecordedAiProvider`
- `AnthropicDirectAiProvider`
- simple policy retrieval implementation
- Serilog/OpenTelemetry wiring

### WorkflowBridge.Web

Contains the Blazor Server UI and app composition.

Initial candidates:

- dashboard
- case creation page
- document upload page
- case analysis page
- audit history page
- local development authentication stub first
- Entra ID integration later

## AI Provider Boundary

The AI provider abstraction is the most important boundary in the project.

Initial interface shape:

```csharp
public interface IAiProvider
{
    Task<AiResponse> CompleteAsync(AiRequest request, CancellationToken cancellationToken);
}
```

Initial model concepts:

- `AiRequest`
- `AiMessage`
- `AiResponse`
- `AiUsage`
- `AiProviderMetadata`
- `AiResponseFormat`

Provider implementations:

- `MockAiProvider`: deterministic local responses.
- `RecordedAiProvider`: replays captured request/response pairs for demos and tests.
- `AnthropicDirectAiProvider`: real provider call for integration viability.

## Credit Risk Workflow

Target vertical slice:

```text
Create Case
  -> Upload PDF/Excel
  -> Parse Documents
  -> Retrieve Fictional Policies
  -> Build AI Request
  -> Execute Provider
  -> Store Assessment + Audit Record
  -> Display Result
```

## Data Model Draft

Initial tables/entities:

- `CreditRiskCases`
- `CreditDocuments`
- `CreditRiskAssessments`
- `PolicyDocuments`
- `AiAuditRecords`

Keep the first schema intentionally small. Add fields only when the vertical slice needs them.

## Authentication Strategy

V1 should start with a local development identity model:

- fake signed-in user;
- single `CreditAnalyst` role;
- clear seam for replacing this with Entra ID later.

Entra ID is valuable for the portfolio, but it should not block the AI workflow.

## Retrieval Strategy

Start with simple retrieval over fictional policy documents:

- store policy files in the repository or database seed data;
- chunk them simply;
- score with keyword matching or basic embeddings only if setup remains manageable.

pgvector can be added after the full workflow works end to end.

## Audit Strategy

Audit records should capture enough metadata to prove control without storing unnecessary secrets.

Suggested fields:

- case id;
- user id or local user name;
- provider name;
- request hash;
- model name;
- prompt version;
- latency;
- token usage when available;
- success/failure;
- error category;
- created timestamp.

Avoid logging API keys, raw secrets, or sensitive uploaded document contents.

## Key Design Decisions

1. Build one end-to-end workflow before adding provider breadth.
2. Keep the AI provider interface owned by the application, not by a vendor SDK.
3. Use recorded responses to make demos and tests deterministic.
4. Prefer local development auth first; document Entra ID as a planned hardening step.
5. Keep RAG simple until the workflow is useful.
