Voici un **prompt type** que tu peux réutiliser pour chaque issue avec Claude Code.

---

```markdown
You are working on RiskLensAI, a .NET enterprise AI integration portfolio project.

## Current workflow

We follow this workflow:

Issue
→ Architect plan
→ Human approval
→ Implementation with tests
→ Build/Test
→ Reviewer
→ Fixes if needed
→ Build/Test again
→ Commit only when explicitly asked

## Current issue

[PASTE THE ISSUE HERE]

## Step 1 — Architect mode

Use `.claude/agents/architect.md`.

Do not write code yet.
Do not modify files.

Your task is to produce an implementation plan only.

Include:

1. Recommended plan
2. Files to touch
3. Tests to add
4. Architecture boundaries to protect
5. Risks or scope creep to avoid

Keep the plan small enough for one issue.
```

Après qu’il te donne le plan et que tu l’approuves, tu continues avec :

````markdown
I approve the plan.

## Step 2 — Implementation mode

Implement the approved plan.

Rules:

- Work only on this issue.
- Do not expand the scope.
- Use TDD where behavior is clear.
- Add or update tests when needed.
- Do not add new NuGet packages unless absolutely required.
- Do not modify unrelated files.
- Do not rename projects.
- Do not change architecture boundaries.
- Do not commit.

Project rules:

- Domain must not reference Infrastructure, Web, EF Core, Blazor, Anthropic, Bedrock, or Azure OpenAI.
- Application owns use cases, interfaces, DTOs, validation, and orchestration.
- Infrastructure owns external providers, persistence, document parsers, SDK integrations, and technical implementations.
- Web owns Blazor UI, endpoints, auth composition, and DI composition.
- All AI calls must go through `IAiProvider`.
- No provider-specific SDK types may leak into Application or Domain.
- Tests must not require real AI provider calls.
- Do not log secrets, API keys, raw sensitive document contents, or full prompts by default.

At the end, run:

```bash
dotnet restore RiskLensAI.sln
dotnet build RiskLensAI.sln --no-restore -m:1
dotnet test RiskLensAI.sln --no-build -m:1
````

Then summarize:

1. Files changed
2. Tests added or updated
3. Build/test results
4. Any follow-up needed

````

Après l’implémentation, tu lances le reviewer :

```markdown
## Step 3 — Reviewer mode

Use `.claude/agents/reviewer.md` to review the current diff.

Do not modify code.

Output exactly the top 3 issues.

Prioritize:

- correctness
- architecture boundaries
- security
- auth/authz
- secret handling
- prompt injection risk
- test coverage
- async misuse
- dependency injection antipatterns
- unnecessary scope creep

Use this format:

1. 🔴 / 🟡 / 🟢 — Issue title
   - Problem:
   - Why it matters:
   - Suggested fix:
````

Si le reviewer trouve des problèmes :

````markdown
## Step 4 — Fixes

Fix only the reviewer issues marked 🔴.

If there are no 🔴 issues, fix only small 🟡 issues that are directly related to the current issue.

Rules:

- Do not refactor unrelated code.
- Do not add new scope.
- Do not modify unrelated files.
- Do not commit.

After fixing, run:

```bash
dotnet build RiskLensAI.sln --no-restore -m:1
dotnet test RiskLensAI.sln --no-build -m:1
````

Then summarize:

1. Fixes made
2. Files changed
3. Build/test results

````

Finalement, quand tout est bon :

```markdown
## Step 5 — Commit prep

Prepare a concise commit summary.

Do not commit yet.

Include:

1. Suggested commit message
2. Files changed
3. What the issue delivered
4. Verification commands run
````

---

## Version courte à retenir

```text
Issue → Architect → I approve → Implement + Tests → Build/Test → Reviewer → Fix → Build/Test → Commit
```

Pour ton prochain ticket, utilise ce flow sur :

```text
Issue 6 + 7 — Define AI request/response models and create IAiProvider abstraction
```
