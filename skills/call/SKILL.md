---
name: call
description: Integrate, explain, review, or install Call, the Epicor dynamic dispatch product. Use for CallLib server calls, reusable delegates, packed arguments, REST binding, and matching CallLib/CallUBAQ packages. Do not use for unrelated function calls or Call Workbench UI work.
license: MIT
---

# Call

Copyright (c) 2026 Kevin P. Lincecum.

Call runs an Epicor Function selected by library and Function names at runtime. Server callers pass real objects. REST callers send JSON.

Call is the product. CallLib is the Function library. CallUBAQ is the dispatch BAQ.

## Select the guidance

- For consumer code, explanations, or debugging, read [integration](references/integration.md).
- For package selection and installation, read [installation](references/installation.md).
- For a complete reusable server consumer, copy [UseCall.cs](assets/UseCall.cs) into the consumer Function's code body.

The bundle contains the maintained helper source. The [manifest](manifest.json) records the packaged resource hashes.

This skill works without the development repository. It does not include build tooling or tests.

Do not infer installed state from this skill. Inspect the authorized target when its current state matters.

## Establish the contract

Determine the caller location, target names, declared inputs, declared outputs, and error policy before generating runnable code.

Reuse known facts. Ask for missing signature facts when they affect correctness. Otherwise provide an explicitly illustrative template.

Never invent an output cast, an installed target, or a default argument. A successful target can have zero outputs.

## Select the public API

| Need | API |
|---|---|
| One server call | EfxLib.CallLib.Call |
| Repeated calls within one request | Get with UseCall, then Run or Packed |
| Scalar or tuple convenience | EfxLib.CallLib.Packed |
| Positional REST inputs | CallLib.Rest |
| Named REST inputs | CallLib.RestNamed |

Add a native Function-library reference to CallLib for server consumers. RestCore and ReadSignature are private implementation Functions.

This example assumes an illustrative Pricing.Calculate target with String and Decimal inputs:

```csharp
var (success, outputs, details) = EfxLib.CallLib.Call("Pricing", "Calculate", new object[] { "PART-123", 10m }, "Throw");
```

Explain the argument order and error policy. Cast outputs only after establishing their declared types.

## Preserve the contract

- Call and Run require a non-null Object[] in target input order.
- Use `Array.Empty<object>()` for zero inputs and `new object[] { null }` for one null input.
- Wrap an array or tuple in an outer Object[] when it is one input.
- Packed expands Object[] and ITuple inputs. Packed's bare null means zero inputs.
- Keep actual server objects intact. Do not convert them through JSON for server calls.
- Get delegates capture the current request. Do not retain them across requests or send them through REST.
- UseCall is a supplied local helper, not an Epicor built-in.
- Direct outputs are oSuccess, oOutputs, and oDetails. Acquired delegates return Success, Outputs, and Details.
- Choose exactly Throw or Return. In Return mode, check success before reading outputs.
- Failure does not establish rollback. Unknown or a missing response requires target-state reconciliation before another attempt.
- REST iArguments is a JSON string inside the outer request. Supply every declared input with exact names or positional order.
- Check oSuccess even with HTTP 200. Authentication and transport errors can occur outside Call.
- Metadata visibility does not grant native execution permission.

The integration reference explains these rules with complete examples and corrections.

## Select packages accurately

Select a lane explicitly from the verified destination version. Keep CallLib and CallUBAQ from the same lane with their manifest and legal files.

The maintained lanes are 5.1.100 and 5.2.100. These are native package versions, not inferred marketing releases.

Do not convert historical test results into current deployment claims. Use the installation reference to distinguish evidence types.

Do not invent workload caps or claim unlimited host capacity. Native resource and serialization constraints still apply.

This skill does not authorize deployment, publication, or business operations. Continue only within the user's actual scope.

## Teach clearly

Present the smallest complete example first. Explain why each contract choice matters.

Use plain technical English. Separate observed facts, illustrative values, assumptions, and unverified behavior.

Preserve Kevin P. Lincecum's personal copyright and MIT license. Keep the attribution "Based on FunctionRunner."
