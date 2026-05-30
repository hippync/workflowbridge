# Roadmap

## Delivery Principle

Ship one strong vertical slice before expanding the platform.

The portfolio value comes from showing a complete, auditable AI workflow in .NET, not from adding every possible provider or cloud feature.

The initial GitHub issue backlog is defined in [github-issues.md](github-issues.md).

## Phase 1 - Foundation

Goal: the solution compiles, runs, and has the right structure.

Expected work:

- create `src` and `tests` project structure;
- add Blazor Server web app;
- add application/domain/infrastructure projects;
- add health check endpoint;
- configure Serilog;
- add initial test projects;
- update README with run instructions once code exists.

Exit criteria:

- `dotnet build` succeeds;
- test projects run;
- web app starts locally;
- empty dashboard loads.

## Phase 2 - AI Integration Viability

Goal: prove AI integration can be controlled and tested.

Expected work:

- add `IAiProvider`;
- add request/response models;
- implement `MockAiProvider`;
- implement `RecordedAiProvider`;
- implement `AnthropicDirectAiProvider`;
- add provider selection configuration;
- add AI audit model;
- add tests around provider behavior and audit logging.

Exit criteria:

- local demos can run with mock responses;
- recorded demos can run without live AI calls;
- live Anthropic path is available when configured;
- audit records are created for AI requests.

## Phase 3 - Business Workflow Demo

Goal: turn the AI integration into a credible banking-style workflow.

Expected work:

- create credit risk case model;
- add case creation UI;
- add PDF upload and parsing;
- add Excel upload and parsing;
- add fictional policy corpus;
- add simple retrieval;
- generate structured credit risk assessment;
- display assessment and policy references;
- add audit history page.

Exit criteria:

- user can create a case, upload docs, run analysis, and review the result;
- audit history is visible;
- the flow works with recorded provider mode;
- main workflow has focused tests.

## Phase 4 - Portfolio Polish

Goal: make the project easy to evaluate.

Expected work:

- complete architecture documentation;
- add Mermaid diagrams;
- add setup instructions;
- add sample documents;
- add demo script;
- record Loom demo;
- create GitHub issues from the roadmap.

Exit criteria:

- evaluator can understand the project in under five minutes;
- evaluator can run the demo locally with mock or recorded AI;
- README clearly explains design tradeoffs and future work.

## Agentic Development Workflow

Goal: use AI coding agents as controlled accelerators without letting them redefine the product.

The workflow should be introduced progressively.

### Stage 1 - Manual Agent-Assisted Delivery

Use Codex or Claude Code for small, scoped tasks:

- create or update one feature at a time;
- keep each task tied to a roadmap item or GitHub issue;
- define expected files, behavior, and acceptance criteria before implementation;
- run build/tests manually before considering the task complete.

Recommended task format:

```text
Implement <small feature>.

Scope:
- files/modules allowed to change
- expected behavior
- tests to add/update
- docs to update

Acceptance criteria:
- dotnet build succeeds
- relevant tests pass
- README/docs updated if behavior changes
```

### Stage 2 - Project Skills

Add local skills once the initial project structure is stable.

Candidate skills:

- `risklensai-architecture`: project boundaries, dependency rules, naming conventions.
- `risklensai-ai-provider`: how to add AI providers, audit records, recorded responses, and provider tests.
- `risklensai-blazor-ui`: UI conventions for internal credit analyst workflows.
- `risklensai-docs`: documentation style, Mermaid diagrams, roadmap updates, and portfolio messaging.

Skills should encode stable conventions, not temporary implementation ideas.

### Stage 3 - Hooks and Quality Gates

Add hooks only after the solution builds consistently.

Candidate hooks:

- after C# changes: run `dotnet format` or `dotnet build`;
- before commit: run `dotnet test`;
- after documentation changes: check Markdown links;
- after AI provider changes: verify provider tests and audit behavior exist.

Hooks should catch regressions without slowing iteration too much.

### Stage 4 - Agent Roles

Use separate agent roles only when the codebase becomes large enough to justify them.

Useful roles:

- implementation agent for scoped issues;
- test agent for coverage and regression tests;
- reviewer agent for architecture boundaries and risk;
- docs agent for README, roadmap, ADRs, and demo material.

The product scope, business workflow, and final tradeoffs stay owned by the developer.

## Explicitly Deferred

- Bedrock provider;
- Azure OpenAI provider;
- advanced Entra ID/RBAC;
- production deployment;
- advanced OpenTelemetry dashboards;
- complex approval workflow;
- multi-tenancy.
