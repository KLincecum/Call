# Call integration guide for AI assistants

Copyright (c) 2026 Kevin P. Lincecum. MIT License.

Call is the product. CallLib is its Epicor Function library. CallUBAQ is its dispatch BAQ.

Based on FunctionRunner.

## Purpose

Use this guide to generate or review Call consumers. It teaches API selection, argument construction, result handling, and recovery.

The portable skill contains this guide as `references/integration.md`. It also contains an installation reference and the actual UseCall helper.

This guide does not require a particular AI provider, connector, or local directory.

## 1. Establish the target contract

Before generating runnable code, establish these facts:

| Fact | Why it matters |
|---|---|
| Caller location | Server code passes real objects. REST sends JSON. |
| Library and Function names | These identify the target. |
| Input names, order, and CLR types | These determine argument construction. |
| Output order and CLR types | These determine output indexing and casts. |
| Error policy | Throw raises an exception. Return supplies a failure result. |
| Environment and company | These determine the execution context and access policy. |
| Installed package version | This determines the matching package pair. |

Reuse verified context. Ask only for missing facts that affect the requested implementation.

If the signature is unavailable, produce a labeled template. State the missing facts instead of inventing types or output positions.

Example question: "What are the target's declared inputs and outputs, including their CLR types and order?"

Do not infer permission to execute from a request for code. Signature inspection and a mutating target call are different actions.

## 2. Select the API

| Situation | Select | Reason |
|---|---|---|
| One server call | CallLib.Call | Direct call with an ordered Object[] |
| Several calls in one request | CallLib.Get with UseCall | Acquire typed methods once for that request |
| Server inputs expressed as a scalar or tuple | CallLib.Packed | Convert convenient input syntax into an argument array |
| REST caller with ordered values | CallLib.Rest | Bind a JSON array against the signature |
| REST caller with named values | CallLib.RestNamed | Bind exact names without positional ordering |

`RestCore` and `ReadSignature` are private. Do not generate consumer calls to either Function.

```text
Caller selects target and inputs
              |
       Select server or REST
              |
       Call validates request
              |
       CallUBAQ dispatches once
              |
       Epicor invokes the target
              |
       Consumer handles the result
```

CallUBAQ is the saved dispatch BAQ. REST signature lookup uses a separate, unsaved query.

## 3. Teach one complete server call

The following target is illustrative. Do not claim that it exists in an installation.

Target: `Pricing.Calculate`.

| Direction | Position | Name | CLR type |
|---|---|---|---|
| Input | 0 | iPartNum | System.String |
| Input | 1 | iQuantity | System.Decimal |
| Output | 0 | oPrice | System.Decimal |

Add a native Function-library reference to CallLib in the consumer library.

```csharp
var (success, outputs, details) = EfxLib.CallLib.Call("Pricing", "Calculate", new object[] { "PART-123", 10m }, "Throw");
decimal price = (decimal)outputs[0];
```

Explain the example in this order:

1. The two names select the target.
2. The Object[] contains inputs in target signature order.
3. The `m` suffix supplies a Decimal value.
4. Throw mode raises an exception on failure.
5. A normal return permits access to the declared Decimal output.

Do not copy the output cast to another target without its signature. A successful target can have zero outputs.

## 4. Handle Return mode explicitly

```csharp
var (success, outputs, details) = EfxLib.CallLib.Call("Pricing", "Calculate", new object[] { "PART-123", 10m }, "Return");
if (!success)
{
    string message = Convert.ToString(details.Tables["Call"].Rows[0]["Message"]);
    // Apply the application's failure policy before leaving this branch.
    return;
}
decimal price = (decimal)outputs[0];
```

The failure branch belongs to the application. Adapt it to the application's output contract, logging policy, and user interface.

Do not read target outputs before checking success. Do not add a retry loop to this example.

Only the exact strings `Throw` and `Return` are valid. An invalid mode raises an exception before normal result handling.

## 5. Distinguish argument shapes

The outer Object[] is the input list. An array inside that list is one input value.

| Target expects | Call or Run arguments |
|---|---|
| No inputs | `Array.Empty<object>()` |
| One null input | `new object[] { null }` |
| One array input | `new object[] { values }` |
| One tuple input | `new object[] { tupleValue }` |
| Two scalar inputs | `new object[] { first, second }` |
| One tableset input | `new object[] { tableset }` |

Call and Run reject a null argument array. Packed interprets bare null as zero inputs.

Packed expands Object[] values and ITuple values into separate inputs. Other values become one input.

Use an outer Object[] when Packed must pass an array or tuple as one input. This also avoids reference-array covariance surprises.

The server path preserves real objects. It does not infer CLR types or convert objects through JSON.

## 6. Acquire reusable methods

Copy the supplied UseCall helper into the consumer Function's code body.

The release supplies `examples/UseCall.cs`. The portable skill supplies the same helper at `assets/UseCall.cs` inside its call folder.

Make System and System.Data available to the code. Keep the native Function-library reference to CallLib.

The following example assumes that the helper is present:

```csharp
var call = UseCall(() => EfxLib.CallLib.Get("Throw"));
var first = call.Run("Pricing", "Calculate", new object[] { "PART-123", 10m });
var second = call.Packed("Pricing", "Calculate", ("PART-456", 25m));
decimal firstPrice = (decimal)first.Outputs[0];
decimal secondPrice = (decimal)second.Outputs[0];
```

Get returns a DataSet with one Methods table and one row. Version is 1.

UseCall checks the structure, version, and delegate types. Both methods return:

```csharp
(bool Success, object[] Outputs, System.Data.DataSet Details)
```

Run accepts `(string library, string function, object[] arguments)`.
Packed accepts `(string library, string function, object value)`.

These methods retain the selected error mode. Each execution creates its own result and details.

Keep the methods within the current request. Do not cache them across requests or serialize them through REST.

UseCall is a supplied helper, not a built-in Epicor method. Do not invent a replacement carrier schema.

## 7. Construct a REST request correctly

Use the standard authenticated Epicor Function endpoint for CallLib/Rest or CallLib/RestNamed.

```text
POST {server}/api/v2/efx/{company}/CallLib/RestNamed
Content-Type: application/json
```

This request uses the illustrative Pricing signature:

```json
{
  "iLibrary": "Pricing",
  "iFunction": "Calculate",
  "iArguments": "{\"iQuantity\":10,\"iPartNum\":\"PART-123\"}",
  "iErrorMode": "Return"
}
```

There are two JSON structures. The outer structure is the Epicor Function request. The inner structure is a string containing target arguments.

For Rest, use `"[\"PART-123\",10]"` as iArguments. For RestNamed, use exact, case-sensitive target parameter names.

Generate nested JSON with a serializer when writing client code. Do not concatenate untrusted values into JSON.

Supply every declared input. Optional metadata does not supply defaults in this implementation.

For zero inputs, send the string `"[]"` to Rest or `"{}"` to RestNamed.

The response contains:

| Field | Meaning |
|---|---|
| oSuccess | The Function result flag |
| oOutputs.Outputs | Rows with zero-based Index, declared Name, and serialized Value |
| oDetails.Call | One diagnostic row |

Epicor can add its normal response envelope. Inspect the actual response before generating client property access.

Check oSuccess in Return mode even when HTTP returns 200. Authentication and transport can fail without a Call result.

REST output values must support Epicor serialization. Delegates and arbitrary server object graphs are not portable REST values.

## 8. Preserve uncertainty

| Observed state | Safe interpretation | Next action |
|---|---|---|
| Success is true | Call accepted a successful response | Read declared outputs |
| Status is `NotStarted` | Call reports failure before dispatch | Correct the request or access issue |
| Status is `Failed` | Call reports a failure | Check the message and target effects |
| Status is Unknown | Call cannot establish the outcome | Reconcile target state before another attempt |
| No usable response | The consumer lacks a reliable outcome | Determine target state before another attempt |

Never describe failure as proof of rollback. Never interpret Unknown as permission to retry.

`DispatchStarted=true` records entry into native invocation. It does not prove that the target body started or committed.

Call does not supply an idempotency key or recovery transaction. The application owns those decisions.

Details contains Id, Library, Function, Stage, Status, DispatchStarted, ElapsedMilliseconds, ErrorCode, and Message.

The diagnostic record omits deliberate payload and stack-trace capture. Target messages can still contain application information.

## 9. Correct common mistakes

| Incorrect proposal | Correction | Reason |
|---|---|---|
| Convert a server tableset to JSON before Call | Pass the actual tableset in Object[] | Preserve its declared type and object identity |
| Read Outputs[0] after every call | Check success and the output signature | Failure and zero-output targets do not supply that value |
| Send an object as iArguments | Serialize the argument object into a string | The outer Function input is String |
| Use Packed with bare null for one null input | Supply new object[] { null } | Bare null means zero inputs |
| Store Get delegates in a static cache | Acquire within the current request | Delegates capture the request host |
| Treat HTTP 200 as business success | Check oSuccess | Return mode can report failure in a successful HTTP response |
| Retry after a timeout | Reconcile the target first | The target can complete before the response fails |
| Invoke a private target because metadata exists | Check native execution eligibility | Visibility does not grant access |
| Choose the larger package version | Match the verified destination version | Version order does not establish compatibility |

## 10. Review the generated answer

- Select a public API that matches the caller location.
- Distinguish known facts from illustrative names.
- Match inputs to the declared order, count, names, and CLR types.
- Read outputs only after success and signature checks.
- Include the real helper when delivering a complete UseCall consumer.
- Preserve the nested JSON string in REST examples.
- Preserve uncertainty without silent retries.
- State what the recorded evidence supports.

Teach the smallest complete example first. Explain the contract decisions next. Add advanced options only when the task needs them.

## Workload constraints

Call does not add an argument-size or input-count cap. It does not impose a small JSON nesting cap or truncate diagnostic messages.

Host memory, native request processing, parser representation, and CLR type conversion still constrain workloads. Do not promise unlimited capacity.

Use the installation reference for package preparation and acceptance boundaries. Do not infer deployment from a packaged skill.
