## Summary

<!-- What does this PR do? One or two sentences. -->

## Related issue

<!-- Closes #<number> -->

## Changes

<!--
List every file changed and why. Keep it tight.
-->

- 

## Verification

<!--
Paste the output or confirm each command passed.
-->

- [ ] `dotnet restore RiskLensAI.sln` — passed
- [ ] `dotnet build RiskLensAI.sln --no-restore -m:1` — passed
- [ ] `dotnet test RiskLensAI.sln --no-build -m:1` — passed

## Architecture checklist

- [ ] Domain does not reference Infrastructure, Web, EF Core, Blazor, or any AI SDK
- [ ] Application does not reference Infrastructure or any AI SDK
- [ ] All AI calls go through `IAiProvider`
- [ ] No provider-specific SDK types appear in Application or Domain
- [ ] No secrets, API keys, or raw sensitive document content are logged
- [ ] Tests do not require live AI provider calls or real database connections

## Notes for reviewer

<!-- Anything that needs extra attention, tradeoffs made, or deferred follow-up. -->
