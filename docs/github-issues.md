# GitHub Issues Plan

This file contains the initial issue backlog for the June delivery cycle.

Target effort: 35-55 hours.

Recommended board columns:

- Backlog
- Ready
- In Progress
- Review
- Done

Recommended labels:

- `epic`
- `foundation`
- `architecture`
- `ai`
- `audit`
- `documents`
- `rag`
- `ui`
- `testing`
- `docs`
- `agentic-workflow`
- `priority-high`
- `priority-medium`
- `priority-low`

## Epics

### EPIC 1 - Project Foundation

Labels: `epic`, `foundation`, `priority-high`

Description:
Set up the .NET solution structure and the minimum runnable application baseline.

Acceptance criteria:

- solution has `src` and `tests` projects;
- `dotnet build` succeeds;
- web app starts locally;
- basic health check exists;
- README has basic run instructions.

### EPIC 2 - AI Provider Integration

Labels: `epic`, `ai`, `priority-high`

Description:
Create the AI provider abstraction and prove the workflow can run with mock, recorded, and live provider modes.

Acceptance criteria:

- `IAiProvider` exists in the application layer;
- mock provider is deterministic;
- recorded provider can replay a response;
- Anthropic provider is available behind configuration;
- provider behavior has focused tests.

### EPIC 3 - Credit Risk Workflow

Labels: `epic`, `ui`, `documents`, `rag`, `priority-high`

Description:
Build the end-to-end credit risk analysis workflow.

Acceptance criteria:

- analyst can create a credit risk case;
- analyst can upload PDF/Excel documents;
- system extracts usable document content;
- system retrieves fictional policies;
- system generates and displays a structured assessment.

### EPIC 4 - Audit, Testing, and Demo Readiness

Labels: `epic`, `audit`, `testing`, `docs`, `priority-high`

Description:
Make the workflow auditable, testable, and portfolio-ready.

Acceptance criteria:

- AI request metadata is audited;
- audit history is visible in the UI;
- core workflow has unit/integration tests;
- recorded mode supports deterministic demo;
- README and docs explain the architecture and tradeoffs.

### EPIC 5 - Agentic Development Workflow

Labels: `epic`, `agentic-workflow`, `docs`, `priority-medium`

Description:
Define and gradually automate the project workflow for AI-assisted development.

Acceptance criteria:

- issue template or task format is documented;
- candidate skills are defined;
- hook strategy is documented;
- quality gates are introduced once the solution is stable.

## Issues

### 1. Create .NET solution project structure

Labels: `foundation`, `architecture`, `priority-high`

Estimate: 2h

Description:
Create the initial `src` and `tests` structure for the planned clean architecture layout.

Tasks:

- create `RiskLensAI.Domain`;
- create `RiskLensAI.Application`;
- create `RiskLensAI.Infrastructure`;
- create `RiskLensAI.Web`;
- create initial test projects;
- wire project references.

Acceptance criteria:

- solution contains all planned projects;
- dependencies point inward only;
- `dotnet build` succeeds.

### 2. Add Blazor Server shell

Labels: `foundation`, `ui`, `priority-high`

Estimate: 2h

Description:
Create the first runnable Blazor Server experience for the internal credit analyst UI.

Tasks:

- create dashboard page;
- create basic layout/navigation;
- add placeholder pages for cases, analysis, and audit;
- keep UI simple and work-focused.

Acceptance criteria:

- app starts locally;
- dashboard loads;
- navigation links work;
- no business logic is embedded in UI components.

### 3. Add health check and basic app configuration

Labels: `foundation`, `priority-high`

Estimate: 1h

Description:
Add the minimum operational baseline for local development.

Tasks:

- add health check endpoint;
- configure typed options for app settings;
- update README run instructions.

Acceptance criteria:

- health endpoint returns healthy status;
- configuration binds from `appsettings` and environment variables;
- README explains how to run the app locally.

### 4. Configure Serilog

Labels: `foundation`, `audit`, `priority-medium`

Estimate: 1.5h

Description:
Add structured logging early so later audit and provider work is easier to observe.

Tasks:

- configure Serilog in the web project;
- add console logging for local development;
- add basic request logging.

Acceptance criteria:

- application emits structured logs;
- logs include request path and status code;
- no secrets are logged.

### 5. Add local development identity

Labels: `foundation`, `ui`, `priority-medium`

Estimate: 2h

Description:
Add a simple local identity mode before introducing Entra ID.

Tasks:

- create local user settings;
- expose a fake signed-in `CreditAnalyst`;
- document Entra ID as deferred.

Acceptance criteria:

- UI can display current analyst identity;
- protected workflow pages can assume a `CreditAnalyst` role;
- implementation is easy to replace later.

### 6. Define AI request and response models

Labels: `ai`, `architecture`, `priority-high`

Estimate: 2h

Description:
Create application-owned AI request/response models independent from vendor SDKs.

Tasks:

- define `AiRequest`;
- define `AiMessage`;
- define `AiResponse`;
- define `AiUsage`;
- define provider metadata model.

Acceptance criteria:

- models live in the application layer;
- models do not depend on Anthropic or other vendor types;
- basic model tests or serialization tests exist if useful.

### 7. Create `IAiProvider` abstraction

Labels: `ai`, `architecture`, `priority-high`

Estimate: 1h

Description:
Create the core AI provider interface used by the application workflow.

Tasks:

- add `IAiProvider`;
- include cancellation support;
- document expected behavior.

Acceptance criteria:

- interface is minimal;
- no infrastructure dependency leaks into application layer;
- usage pattern is documented in architecture docs.

### 8. Implement `MockAiProvider`

Labels: `ai`, `testing`, `priority-high`

Estimate: 2h

Description:
Implement deterministic AI responses for local development and early UI work.

Tasks:

- create mock provider implementation;
- return a structured credit risk assessment response;
- add tests.

Acceptance criteria:

- provider works without network access;
- response is stable;
- tests verify deterministic behavior.

### 9. Implement AI provider configuration

Labels: `ai`, `foundation`, `priority-high`

Estimate: 1.5h

Description:
Add provider selection through configuration.

Tasks:

- bind provider settings;
- support `Mock`, `Recorded`, and `Anthropic` provider names;
- fail clearly for unknown provider names.

Acceptance criteria:

- provider can be selected from environment variables;
- invalid config fails with a useful error;
- default local mode is `Mock`.

### 10. Design AI audit record model

Labels: `audit`, `ai`, `architecture`, `priority-high`

Estimate: 2h

Description:
Define the metadata that must be captured for AI requests and responses.

Tasks:

- create audit entity/model;
- include provider, model, prompt version, latency, token usage, success/failure, and request hash;
- explicitly exclude secrets and raw sensitive document content.

Acceptance criteria:

- audit model is documented;
- audit record can represent success and failure;
- sensitive data exclusions are clear.

### 11. Implement basic audit logger

Labels: `audit`, `ai`, `priority-high`

Estimate: 2.5h

Description:
Implement the service that records AI request metadata.

Tasks:

- create `IAuditLogger`;
- implement initial persistence or in-memory version depending on database readiness;
- log provider call success/failure.

Acceptance criteria:

- AI provider calls create audit records;
- failure cases are auditable;
- tests cover success and failure metadata.

### 12. Implement `RecordedAiProvider`

Labels: `ai`, `testing`, `priority-high`

Estimate: 3h

Description:
Implement replayable AI responses for demos and deterministic tests.

Tasks:

- define cassette file format;
- load recorded responses from configured path;
- match requests by stable key or request hash;
- add sample recording.

Acceptance criteria:

- recorded mode works without live AI calls;
- missing recordings fail clearly;
- tests prove replay behavior.

### 13. Implement `AnthropicDirectAiProvider`

Labels: `ai`, `priority-medium`

Estimate: 4h

Description:
Add the real Anthropic integration behind the app-owned provider abstraction.

Tasks:

- create provider implementation;
- map app request/response models to Anthropic API;
- add timeout handling;
- avoid logging secrets;
- document required environment variable.

Acceptance criteria:

- provider is only active when configured;
- missing API key fails clearly;
- provider can be tested with mocked HTTP or isolated adapter tests.

### 14. Add EF Core and PostgreSQL baseline

Labels: `foundation`, `priority-high`

Estimate: 3h

Description:
Add the persistence baseline for cases, documents, assessments, and audit records.

Tasks:

- add EF Core packages;
- add PostgreSQL provider;
- create DbContext;
- create first migration;
- add local connection string docs.

Acceptance criteria:

- database schema can be created locally;
- app can connect to PostgreSQL;
- persistence layer lives in Infrastructure.

### 15. Create credit risk case model and use case

Labels: `ui`, `architecture`, `priority-high`

Estimate: 3h

Description:
Add the first business entity and application use case for creating a case.

Tasks:

- create `CreditRiskCase`;
- add create case command/use case;
- add validation;
- add tests.

Acceptance criteria:

- case can be created through application layer;
- invalid input is rejected;
- tests cover core creation behavior.

### 16. Build case creation UI

Labels: `ui`, `priority-high`

Estimate: 2.5h

Description:
Create the Blazor page for starting a credit risk case.

Tasks:

- add form;
- call application use case;
- show validation feedback;
- redirect to case detail page.

Acceptance criteria:

- analyst can create a case from the UI;
- validation errors are visible;
- created case can be opened.

### 17. Add PDF parsing service

Labels: `documents`, `priority-high`

Estimate: 3h

Description:
Extract text from uploaded PDF documents.

Tasks:

- create `IDocumentParser` contract;
- implement PDF parser with PdfPig;
- handle empty or unreadable PDFs;
- add tests with sample PDF.

Acceptance criteria:

- PDF text extraction works for sample document;
- parser errors are handled cleanly;
- tests cover success and failure cases.

### 18. Add Excel parsing service

Labels: `documents`, `priority-high`

Estimate: 3h

Description:
Extract structured table data from uploaded Excel files.

Tasks:

- implement Excel parser with EPPlus;
- extract useful rows/cells for financial context;
- handle empty or invalid files;
- add tests with sample spreadsheet.

Acceptance criteria:

- Excel extraction works for sample file;
- parser errors are handled cleanly;
- tests cover success and failure cases.

### 19. Build document upload flow

Labels: `documents`, `ui`, `priority-high`

Estimate: 3h

Description:
Allow analysts to upload PDF and Excel files to a credit risk case.

Tasks:

- add upload UI;
- validate file type and size;
- store metadata and parsed output;
- show uploaded documents on case detail page.

Acceptance criteria:

- analyst can upload supported documents;
- invalid files are rejected with clear feedback;
- parsed output is associated with the case.

### 20. Add fictional credit policy corpus

Labels: `rag`, `docs`, `priority-high`

Estimate: 2h

Description:
Create a small set of fictional policy documents used by the RAG workflow.

Tasks:

- create policy markdown or seed data;
- include lending criteria, risk factors, and escalation guidance;
- keep content clearly fictional.

Acceptance criteria:

- policy documents are available locally;
- policies can be loaded by the app;
- README or docs mention that policies are fictional.

### 21. Implement simple policy retrieval

Labels: `rag`, `priority-high`

Estimate: 3h

Description:
Retrieve relevant fictional policies for a credit risk case.

Tasks:

- create `IPolicyRetriever`;
- implement simple keyword or lightweight scoring;
- return policy references with snippets;
- add tests.

Acceptance criteria:

- retrieval returns relevant policy references for sample cases;
- no vector database is required for the first version;
- tests cover ranking basics.

### 22. Build structured credit risk assessment workflow

Labels: `ai`, `ui`, `rag`, `priority-high`

Estimate: 4h

Description:
Orchestrate document context, policy retrieval, AI provider call, and structured assessment storage.

Tasks:

- create analysis use case;
- build prompt/request from case, documents, and policies;
- parse/store structured assessment;
- create audit record.

Acceptance criteria:

- case can be analyzed end to end;
- result includes risk rating, rationale, key risks, and policy references;
- workflow works with `MockAiProvider`.

### 23. Build assessment result UI

Labels: `ui`, `priority-high`

Estimate: 3h

Description:
Display the generated credit risk assessment clearly for the analyst.

Tasks:

- show risk rating;
- show rationale;
- show key risk factors;
- show referenced policies;
- show provider metadata when useful.

Acceptance criteria:

- analyst can review the full result;
- layout is readable and work-focused;
- missing/failed assessment states are handled.

### 24. Build audit history UI

Labels: `audit`, `ui`, `priority-medium`

Estimate: 2.5h

Description:
Expose audit history for AI calls and workflow actions.

Tasks:

- create audit page;
- show provider, model, status, latency, timestamp, and case id;
- add case-level audit view if time permits.

Acceptance criteria:

- audit history is visible from the UI;
- failures are distinguishable from successes;
- sensitive prompt/document content is not displayed by default.

### 25. Add core application tests

Labels: `testing`, `priority-high`

Estimate: 4h

Description:
Cover the main application use cases with focused tests.

Tasks:

- test case creation;
- test validation;
- test analysis orchestration;
- test audit behavior;
- test policy retrieval basics.

Acceptance criteria:

- tests cover the main workflow behavior;
- tests run without live AI calls;
- `dotnet test` succeeds.

### 26. Add provider integration tests

Labels: `testing`, `ai`, `priority-medium`

Estimate: 3h

Description:
Test AI provider behavior without relying on live provider calls.

Tasks:

- add Mock provider tests;
- add Recorded provider tests;
- add Anthropic adapter tests with WireMock.Net or mocked HTTP;
- test failure cases.

Acceptance criteria:

- provider tests are deterministic;
- no test requires a real API key;
- failure behavior is covered.

### 27. Add Docker Compose for local PostgreSQL

Labels: `foundation`, `priority-medium`

Estimate: 2h

Description:
Make local database setup repeatable.

Tasks:

- add `docker-compose.yml`;
- configure PostgreSQL service;
- document start/stop commands;
- optionally include pgvector extension if simple.

Acceptance criteria:

- local PostgreSQL starts with one command;
- connection string matches `.env.example`;
- README documents the flow.

### 28. Add Mermaid architecture diagrams

Labels: `docs`, `architecture`, `priority-medium`

Estimate: 2h

Description:
Add visual architecture diagrams for portfolio readability.

Tasks:

- add solution architecture diagram;
- add AI provider flow diagram;
- add credit risk workflow diagram.

Acceptance criteria:

- diagrams are included in docs;
- README links to relevant diagrams;
- diagrams match actual implementation.

### 29. Define agentic task template

Labels: `agentic-workflow`, `docs`, `priority-medium`

Estimate: 1.5h

Description:
Create a reusable task format for Codex/Claude Code work blocks.

Tasks:

- document task prompt template;
- include scope, constraints, expected files, tests, and acceptance criteria;
- add examples for provider and UI tasks.

Acceptance criteria:

- template is easy to copy into future agent sessions;
- template aligns with roadmap issues;
- docs explain when to use it.

### 30. Define project skills plan

Labels: `agentic-workflow`, `docs`, `priority-low`

Estimate: 2h

Description:
Plan the local skills that may be created once architecture stabilizes.

Tasks:

- define candidate skills;
- define when each skill becomes useful;
- define what each skill must include;
- avoid implementing skills too early.

Acceptance criteria:

- plan is documented;
- skill boundaries are clear;
- no unnecessary automation is introduced before project structure stabilizes.

### 31. Add build/test quality gates

Labels: `agentic-workflow`, `testing`, `priority-medium`

Estimate: 2h

Description:
Add lightweight quality gates after the solution structure is stable.

Tasks:

- document manual commands first;
- add script or hook for build/test if useful;
- ensure hooks do not block fast iteration unnecessarily.

Acceptance criteria:

- standard verification commands are documented;
- quality gate can run locally;
- setup is simple to disable or bypass for emergencies.

### 32. Prepare portfolio README and demo script

Labels: `docs`, `priority-high`

Estimate: 4h

Description:
Finalize the project presentation for portfolio review.

Tasks:

- update README with final setup and demo flow;
- document architecture decisions;
- add demo script;
- list deferred features and tradeoffs.

Acceptance criteria:

- reviewer can understand the project quickly;
- demo can run with recorded provider mode;
- README clearly explains the enterprise AI integration angle.

## Suggested June Milestones

### Week 1 - Foundation and AI Boundary

Focus:

- issues 1-9;
- basic app running;
- provider abstraction ready;
- mock provider usable.

### Week 2 - Audit and Persistence

Focus:

- issues 10-15;
- audit model;
- database baseline;
- first business use case.

### Week 3 - Documents and RAG

Focus:

- issues 16-22;
- upload/parsing;
- fictional policies;
- simple retrieval;
- end-to-end analysis with mock provider.

### Week 4 - UI, Tests, and Demo

Focus:

- issues 23-32;
- audit page;
- tests;
- diagrams;
- agentic workflow docs;
- portfolio demo readiness.
