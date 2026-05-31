---
name: Agent-Ready Issue
about: A scoped, implementation-ready issue suitable for AI coding agent (Claude Code / Codex) execution
title: ""
labels: ""
assignees: ""
---

<!--
Use this template when the issue is ready to hand off to an AI coding agent.
All fields are required. Vague issues produce vague code.
-->

## Summary

<!-- One or two sentences. What is being built and why. -->

## Scope — files and modules this issue may touch

<!--
List every file path or project that the agent is allowed to modify.
The agent must not touch anything outside this list.
-->

- `src/RiskLensAI.???/`

## Behavior to implement

<!--
Describe exactly what the code should do. Be specific enough that a test can verify it.
-->

- 

## Tests to add or update

<!--
Name the test class or scenario. At minimum one test per piece of behavior.
Tests must not require live AI calls, real database connections, or external HTTP calls.
-->

- 

## Docs to update

<!--
List docs files to update if behavior changes. Write "None" if not applicable.
-->

- 

## Acceptance criteria

<!--
These must all pass before the task is considered complete.
-->

- [ ] `dotnet restore RiskLensAI.sln` succeeds
- [ ] `dotnet build RiskLensAI.sln --no-restore -m:1` succeeds
- [ ] `dotnet test RiskLensAI.sln --no-build -m:1` succeeds
- [ ] 
- [ ] 

## Constraints

- Do not modify files outside the scope list.
- Do not add NuGet packages without stating the reason in a comment on this issue.
- Do not commit — stop and summarize when done.
- Domain must not reference Infrastructure, Web, EF Core, Blazor, or any AI SDK.
- No provider-specific SDK types in Application or Domain.
- Tests must not require live AI provider calls.

## References

<!-- Link related issues, docs, or architecture sections. -->

- Architecture: [docs/architecture.md](../../docs/architecture.md)
- Agentic workflow: [docs/agentic-workflow.md](../../docs/agentic-workflow.md)
- Quality gates: [docs/quality-gates.md](../../docs/quality-gates.md)
