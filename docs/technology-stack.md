# Technology Stack

## Goal

This document defines the technologies planned for WorkflowBridge v1.

The stack should support the main portfolio message: an enterprise-style .NET application that integrates AI in a controlled, testable, auditable way.

## Runtime and Framework

| Area | Technology | Usage |
| --- | --- | --- |
| Runtime | .NET 10 target, or latest stable installed SDK for local setup | Main application runtime |
| Web framework | ASP.NET Core | Hosting, dependency injection, configuration, health checks |
| UI | Blazor Server | Internal credit analyst workflow UI |
| API style | Minimal API where useful | Health checks and small backend endpoints |

## Application Architecture

| Area | Technology / Pattern | Usage |
| --- | --- | --- |
| Solution structure | Domain, Application, Infrastructure, Web | Clear separation of responsibilities |
| Validation | FluentValidation | Request and command validation |
| Resilience | Polly | Timeouts, retries, and circuit-breaker behavior around external AI calls |
| Configuration | ASP.NET Core Options pattern | Provider selection and typed settings |

## AI Integration

| Area | Technology | Usage |
| --- | --- | --- |
| Provider abstraction | Custom `IAiProvider` | Own the app boundary instead of depending directly on one vendor SDK |
| Local development | `MockAiProvider` | Deterministic local responses |
| Demo/testing | `RecordedAiProvider` | Replay curated AI responses without spending tokens |
| Live provider | Anthropic provider | Real model integration path |
| Optional adapter | Microsoft.Extensions.AI | Possible future adapter once the internal provider boundary is stable |

## Data and Persistence

| Area | Technology | Usage |
| --- | --- | --- |
| Database | PostgreSQL | Primary relational database |
| ORM | EF Core | Persistence and migrations |
| Vector search | pgvector | Optional v1 enhancement for policy retrieval if setup remains simple |
| Local containers | Docker Desktop | PostgreSQL and supporting local services |

## Document Processing

| Area | Technology | Usage |
| --- | --- | --- |
| PDF parsing | PdfPig | Extract text from uploaded credit documents |
| Excel parsing | EPPlus | Extract tabular financial data from spreadsheets |
| Upload handling | ASP.NET Core file upload support | Store and process case documents |

## Observability and Audit

| Area | Technology | Usage |
| --- | --- | --- |
| Logging | Serilog | Structured application logs |
| Audit logging | Application-owned audit records | Track business actions and AI request metadata |
| Telemetry | OpenTelemetry basics | Optional tracing/metrics once core workflow is stable |

## Testing

| Area | Technology | Usage |
| --- | --- | --- |
| Test framework | xUnit | Unit and integration tests |
| Mocking | NSubstitute | Mock application interfaces |
| HTTP provider tests | WireMock.Net | Stub external AI provider behavior |
| Deterministic AI tests | Recorded provider cassettes | Stable tests without live provider calls |

## Development Tools

| Area | Tool | Usage |
| --- | --- | --- |
| IDE | Visual Studio 2022 or Rider | Main .NET development environment |
| Agent | Codex / Claude Code | Implementation acceleration and review support |
| API testing | Bruno or Postman | Manual endpoint testing when APIs are added |
| Version control | Git and GitHub | Source control, issues, project board |
| Diagrams | Mermaid | Architecture diagrams in documentation |
| Demo | Loom | Final portfolio walkthrough |

## Agentic Workflow Tooling

| Area | Tooling | Usage |
| --- | --- | --- |
| Coding agent | Codex or Claude Code | Execute small implementation blocks, fixes, tests, and documentation updates |
| Project skills | Local repository skills | Encode stable project conventions once the architecture is established |
| Quality hooks | Git hooks or task runner scripts | Run build, test, format, and documentation checks at useful points |
| Review workflow | Agent-assisted review | Detect architecture drift, missing tests, and documentation gaps |

The agentic workflow should remain issue-driven. Each agent task needs a narrow scope and acceptance criteria.

## Deferred Technologies

These are intentionally not required for the first vertical slice:

- full Azure OpenAI provider;
- full AWS Bedrock provider;
- advanced Entra ID setup;
- advanced OpenTelemetry dashboarding;
- production cloud deployment;
- multi-tenant infrastructure.

## Selection Principles

1. Prefer technologies that directly support the credit risk AI workflow.
2. Keep provider-specific SDKs behind application-owned interfaces.
3. Use recorded AI responses so demos and tests are stable.
4. Add infrastructure complexity only after the vertical slice works end to end.
5. Keep the v1 stack credible for enterprise .NET without turning the project into a production platform.
