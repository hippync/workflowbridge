# Quality Gates

This document defines the quality gates for RiskLensAI.

All agents and contributors must pass these gates before reporting a task complete.

## Standard Commands

Run all three in order. Each must succeed before proceeding to the next.

```bash
dotnet restore RiskLensAI.sln
dotnet build RiskLensAI.sln --no-restore -m:1
dotnet test RiskLensAI.sln --no-build -m:1
```

The `-m:1` flag keeps builds sequential. This avoids noisy parallel build behavior while the solution is small.

## What Each Gate Checks

| Command | Checks |
|---------|--------|
| `dotnet restore` | All NuGet packages resolve. No missing dependencies. |
| `dotnet build` | Solution compiles with no errors. No unresolved references. |
| `dotnet test` | All tests pass. No test requires a live AI provider or real external service. |

## Failure Handling

If `dotnet restore` fails:
- check that the .NET SDK version matches `global.json`
- check that new NuGet packages are available on nuget.org
- do not proceed to build

If `dotnet build` fails:
- fix the compile error before continuing
- do not suppress warnings with `#pragma` or `--no-warn` unless already established practice

If `dotnet test` fails:
- identify and fix the failing test
- do not mark a task complete with failing tests
- do not skip tests with `[Skip]` without a documented reason

## SDK Version

The repository pins the SDK via `global.json`. The required SDK version is .NET 10.0.300 or a compatible .NET 10 SDK.

If the local environment does not have the correct SDK, install it or run:

```powershell
.\dotnet-install.ps1
```

The `.dotnet/` directory is excluded from version control. It is a local SDK installation cache only.

## Test Rules

Tests must not require:
- a live Anthropic API key
- a live database connection
- real HTTP calls to external AI providers

Use `MockAiProvider`, `RecordedAiProvider`, or NSubstitute mocks for all provider behavior.

Use WireMock.Net for HTTP-level adapter tests.

## Running the App Locally

After the gates pass, verify the app starts:

```bash
dotnet run --project src/RiskLensAI.Web/RiskLensAI.Web.csproj
```

Health check endpoint:

```
http://localhost:5000/health
https://localhost:5001/health
```

Expected response: `Healthy` with HTTP 200.

## Future Gates

When the solution is stable, the following gates may be added:

| Gate | Command | When to add |
|------|---------|-------------|
| Format check | `dotnet format RiskLensAI.sln --verify-no-changes` | After first full working slice |
| Coverage threshold | `dotnet test --collect:"XPlat Code Coverage"` | After core workflow tests exist |
| Architecture enforcement | ArchUnit or NDepend rule | After layer structure is stable |

Do not add these gates prematurely. They slow iteration before the solution is stable.
