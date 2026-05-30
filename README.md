# WorkflowBridge

> AI-assisted credit risk workflow built with .NET, Blazor Server, auditable AI providers, document ingestion, and a portfolio-ready architecture.

Active planning phase. The previous Stripe/Notion/Slack automation scope has been replaced by a .NET enterprise AI integration project.

## Product Intent

WorkflowBridge demonstrates how to integrate AI into a .NET business application in a way that is controlled, testable, auditable, and explainable.

The v1 workflow focuses on one banking-style use case:

1. A credit analyst creates a credit risk case.
2. The analyst uploads supporting PDF and Excel documents.
3. The system extracts document text and structured financial data.
4. A simple RAG layer retrieves relevant fictional credit policies.
5. An AI provider produces a structured credit risk assessment.
6. The application stores request metadata, response metadata, and audit history.
7. The analyst reviews the result in a Blazor Server UI.

## V1 Scope

### Must Have

- .NET solution with clear project boundaries.
- Blazor Server web application.
- Credit risk workflow demo.
- AI provider abstraction.
- Mock provider for local development.
- Recorded provider for deterministic demos and tests.
- Direct Anthropic provider for real AI calls.
- PDF and Excel parsing.
- Simple fictional policy retrieval.
- Audit logging for AI requests and business actions.
- Focused unit/integration tests.
- Portfolio README and architecture documentation.

### Strong Nice To Have

- EF Core with PostgreSQL.
- pgvector-backed retrieval, if it stays simple.
- Polly retries/timeouts around provider calls.
- WireMock.Net tests for external provider behavior.
- OpenTelemetry basics.

### Deferred

- Complete Bedrock provider.
- Complete Azure OpenAI provider.
- Advanced Entra ID configuration.
- Production-grade cloud deployment.
- Complex admin dashboard.
- Multi-tenant architecture.
- Advanced approval workflow.

## Recommended Architecture

```text
src/
  WorkflowBridge.Domain
  WorkflowBridge.Application
  WorkflowBridge.Infrastructure
  WorkflowBridge.Web

tests/
  WorkflowBridge.Application.Tests
  WorkflowBridge.Infrastructure.Tests
```

See [docs/architecture.md](docs/architecture.md) for the current design plan.

## Initial Technical Stack

- .NET 10 target, or latest stable installed SDK if .NET 10 is unavailable locally.
- ASP.NET Core and Blazor Server.
- EF Core and PostgreSQL.
- Serilog.
- FluentValidation.
- PdfPig.
- EPPlus.
- xUnit.
- NSubstitute.
- WireMock.Net.
- Anthropic SDK or a small direct HTTP adapter.
- Optional Microsoft.Extensions.AI adapter once the core provider boundary is stable.

See [docs/technology-stack.md](docs/technology-stack.md) for the detailed technology plan.

## Delivery Plan

The project should be delivered as a strict vertical slice before expanding provider or platform support.

1. Foundation: solution structure, health check, logging, empty Blazor shell, test projects.
2. AI integration viability: provider abstraction, mock provider, recorded provider, Anthropic provider, audit model.
3. Business workflow demo: credit risk case, upload/parsing, simple RAG, structured assessment, audit page, README/demo material.

See [docs/roadmap.md](docs/roadmap.md) for the detailed phase plan.

## Portfolio Message

This project is not intended to impress through feature count. It should demonstrate one complete enterprise AI workflow with:

- clean .NET architecture;
- stable AI integration boundaries;
- repeatable demos;
- auditable request/response behavior;
- meaningful tests;
- clear documentation.

## License

MIT - see [LICENSE](LICENSE).
