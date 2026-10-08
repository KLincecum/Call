# Call installation guidance for AI assistants

Copyright (c) 2026 Kevin P. Lincecum. MIT License.

## Select the package pair

Use CallLib.efxj and CallUBAQ.baq from the same version folder. Match the destination's native Epicor/ICE package version: 5.1.100 or 5.2.100.

These numbers are package versions, not marketing release names. Keep the manifest and legal files with each pair.

The packages contain sample company CALLDEMO and owner KLINCECUM. Import them directly with Epicor's native import facilities. Do not edit or repackage the files first.

CALLDEMO is an example, not a required company. Do not create that company merely to install Call.

## Before import

1. Select the authorized destination company. Use a non-production destination for initial acceptance.
2. Inspect existing CallLib and CallUBAQ identities. Export both before an authorized update.
3. Stop if either name belongs to an unrelated artifact. Do not overwrite it.
4. Check that native DynamicQuery, Newtonsoft.Json, and System.Text.Json are available.

Call has no custom Function-library dependency. It does not use the EFX business object or a Designer service at runtime.

## Import and configure

1. Import CallUBAQ.baq as CallUBAQ in BAQ Designer. Check the import log and the base GetList directive named CallUBAQ.
2. Compile or regenerate that directive through the native BPM facilities. Check for compiler errors and check its current state.
3. Import CallLib.efxj as CallLib through the native Function import facility. Run native validation on all seven Functions.
4. Check the company, library owner, and BAQ author. Change the owner and author to the appropriate local account.
5. Add the real company to CallLib's allowed company mappings. Remove CALLDEMO if that mapping remains.
6. Configure Function-library access, BAQ access, API-key access scopes, and target Function permissions for the intended callers.
7. Review the shared BAQ setting and BAQDEFAULT security code against local policy. Shared access does not bypass native permissions.
8. Publish CallLib when required for the consumer route. The supplied library starts unpublished.
9. Read back the installed signatures, references, code, and directive. Check the matching package pair.

The public Functions are Get, Call, Packed, Rest, and RestNamed. RestCore and ReadSignature are private implementation Functions.

Epicor assigned the import company to the library and BAQ in the verified 5.2.100 test. It preserved KLINCECUM as owner and author.

The importer discarded the nonexistent CALLDEMO library mapping. The library required an explicit real-company mapping before execution.

Recipients must check these settings even when import succeeds. If the packaged owner does not exist locally, select a valid local owner.

The BAQ directive required compilation after import. Check its enabled and current states. Setting a status field is not compilation.

Do not accept a successful BAQ import message as proof that the directive exists. Check the directive and execute a harmless known target.

## Check the consumer route

1. Add a native reference to CallLib in a server consumer library.
2. Execute a harmless target with a known signature and expected output.
3. Check Return and Throw behavior with a controlled failure.
4. For REST, check authentication, company context, oSuccess, output types, and serialization.
5. For Decimal-sensitive consumers, check raw HTTP response text with a decimal-preserving client.
6. Repeat the relevant checks with the intended caller's permissions.

Signature visibility does not grant execution permission. Epicor enforces access during native invocation.

If an outcome is uncertain, inspect the target's state before another attempt. Call does not automatically retry or guarantee rollback.

## Verification boundary

The 5.2.100 dummy-company test used isolated library and query names. It verified native import, company reassignment, retained ownership, mapping, directive compilation, and successful REST dispatch.

The test did not replace the existing Call installation. It did not establish ordinary-user access or exact Decimal digits in raw HTTP text.

The 5.1.100 lane shares the corrected packaging logic and local checks. This test did not repeat native import on a 5.1.100 server.

## Later updates

Export the installed pair first. Import the matching replacement pair, check ownership and security, compile, and check the consumer route.

Keep the full MIT license and Kevin P. Lincecum's copyright with redistributed copies.
