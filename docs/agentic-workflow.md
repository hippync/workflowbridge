# Agentic Workflow

This document defines how AI coding agents (Claude Code, Codex, etc.) work within this repository.

The goal is to use agents as controlled accelerators — not to let them redefine product scope.

See also: [quality-gates.md](quality-gates.md), [roadmap.md](roadmap.md), [CLAUDE.md](../CLAUDE.md).

## Core Principle

One issue. One slice. Verify before moving on.

Agents work best when the scope is tight, the acceptance criteria are explicit, and the verification step is mandatory.

## Task Format

Use this format when assigning work to an agent. Every field is required.

```text
## Task: <short title>

**Issue:** #<number> — <issue title>

**Scope — files and modules this task may touch:**
- <project>/<path>
- <project>/<path>

**Behavior to implement:**
- <bullet describing one piece of behavior>
- <bullet describing one piece of behavior>

**Tests to add or update:**
- <test class or scenario>
- <test class or scenario>

**Docs to update (if behavior changes):**
- <file>

**Acceptance criteria:**
- [ ] `dotnet build RiskLensAI.sln --no-restore -m:1` succeeds
- [ ] `dotnet test RiskLensAI.sln --no-build -m:1` succeeds
- [ ] <specific behavior criterion>
- [ ] <specific behavior criterion>

**Constraints:**
- Do not touch files outside the scope list above.
- Do not add NuGet packages without stating the reason.
- Do not commit.
- Stop and report when done.
```

## Example: Adding a Use Case

```text
## Task: Add CreateCreditRiskCase use case

**Issue:** #15 — Create credit risk case model and use case

**Scope — files and modules this task may touch:**
- src/RiskLensAI.Domain/
- src/RiskLensAI.Application/
- tests/RiskLensAI.Application.Tests/

**Behavior to implement:**
- Add `CreditRiskCase` entity to Domain with Id, AnalystId, Title, Status, and CreatedAt.
- Add `CreateCreditRiskCaseCommand` DTO and `ICreateCreditRiskCaseUseCase` interface in Application.
- Add a `CreateCreditRiskCaseHandler` implementation that validates input and returns the new case id.
- Add a `CaseStatus` enum with `Draft`, `UnderReview`, and `Closed` values.

**Tests to add or update:**
- `CreateCreditRiskCaseHandlerTests` — covers valid input, missing title, empty analyst id.

**Docs to update:**
- None required for this slice.

**Acceptance criteria:**
- [ ] `dotnet build RiskLensAI.sln --no-restore -m:1` succeeds
- [ ] `dotnet test RiskLensAI.sln --no-build -m:1` succeeds
- [ ] Case cannot be created with an empty title.
- [ ] Case cannot be created with an empty analyst id.
- [ ] Created case has `Draft` status.

**Constraints:**
- Do not touch Infrastructure or Web.
- Do not add NuGet packages.
- Do not commit.
```

## Example: Adding a Provider Implementation

```text
## Task: Implement MockAiProvider

**Issue:** #8 — Implement MockAiProvider

**Scope — files and modules this task may touch:**
- src/RiskLensAI.Infrastructure/
- tests/RiskLensAI.Infrastructure.Tests/

**Behavior to implement:**
- Add `MockAiProvider` in Infrastructure implementing `IAiProvider`.
- Return a deterministic `AiResponse` with a hardcoded credit risk assessment JSON payload.
- Do not make network calls.

**Tests to add or update:**
- `MockAiProviderTests` — verifies response is non-null, has expected risk rating field, and is stable across repeated calls.

**Docs to update:**
- None required.

**Acceptance criteria:**
- [ ] `dotnet build RiskLensAI.sln --no-restore -m:1` succeeds
- [ ] `dotnet test RiskLensAI.sln --no-build -m:1` succeeds
- [ ] Response is identical on repeated calls with the same input.
- [ ] No network call is made.
- [ ] No Anthropic or vendor SDK types appear in Application or Domain.

**Constraints:**
- Do not modify Application or Domain.
- Do not add NuGet packages.
- Do not commit.
```

## Agent Behavior Rules

These rules apply to all agents working in this repository. They are duplicated in [CLAUDE.md](../CLAUDE.md).

1. Work on one issue at a time.
2. Start with the smallest testable vertical slice.
3. Use TDD when behavior is clear and the interface is stable.
4. Do not expand scope beyond the current issue.
5. Do not create abstractions not required by the current task.
6. Do not introduce new NuGet packages without stating the reason.
7. Stop before committing unless explicitly asked to commit.
8. Summarize: files changed, behavior added, verification commands run and results.

## Verification Step

After every task, run and report the output of:

```bash
dotnet restore RiskLensAI.sln
dotnet build RiskLensAI.sln --no-restore -m:1
dotnet test RiskLensAI.sln --no-build -m:1
```

If the .NET SDK is not available locally, state that clearly. Do not report success without running the commands.

## Workflow Stages

The agentic workflow matures in stages. See [roadmap.md](roadmap.md) for the full plan.

| Stage | Description |
|-------|-------------|
| 1 | Manual agent-assisted delivery — one task at a time, developer reviews each output |
| 2 | Project skills — stable conventions encoded as reusable agent skills |
| 3 | Hooks and quality gates — automated build/test checks after changes |
| 4 | Agent roles — separate agents for implementation, testing, review, and docs |

Start at Stage 1. Advance only when the prior stage is stable.

## What Stays Human-Owned

Agents assist with implementation. The following stay owned by the developer:

- product scope and v1 feature decisions
- architecture boundary decisions
- final review before merging
- demo and portfolio presentation choices
- decisions about which issues to pick up next
