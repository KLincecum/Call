# Call for the 2024.2 native package lane

Edition: 2026-10-10. Methods contract: 1. Native package version: 4.3.200.

Use the matching CallLib.efxj and CallUBAQ.baq in [dist/4.3.200](../dist/4.3.200/). The port uses the same maintained Function and BPM source as the 5.1.100 and 5.2.100 lanes. Its package format and native compilation were checked on an Epicor 2024.2 lab with database version 11.3.200.39.

## Verified scope

- All 61 local simulated checks passed for this lane.
- The target-configured pair was imported, compiled, published, and independently read back. The check included Function signatures, code, references and company mappings, plus the BAQ schema, query definition, diagram, directive and workflow binding.
- All 21 native server test groups passed, including Decimal fidelity, positional and named dispatch, Return and Throw behavior, invalid-input handling, and requests beyond the former fixed workload limits.
- Four REST cases passed: positional success, named success, controlled Return failure, and the expected HTTP 400 for controlled Throw failure.
- Native server and REST checks passed again after an application-server restart. Temporary test libraries were removed and their absence verified.

These are bounded installation and execution results. They do not establish ordinary-user access or exact Decimal digits in externally captured raw HTTP response text.

## Installation requirements

Follow [Install Call](INSTALL.md). Use an account authorized to maintain advanced BAQs and BPMs for import and native directive compilation. Check the actual directive after import: a successful import message alone can hide a skipped directive. Keep deployment-account rights separate from intended-consumer permissions.

The verified lab required a host-level XAML dependency repair before native BPM persistence could work. Even JSON-based native directive saves used XAML serialization internally. This was an environment dependency, not a Call source change. The repair was tested across restart. The Call release contains no Microsoft or Epicor runtime assemblies; host dependencies must be maintained through the destination's authorized deployment process.

## Package evidence boundary

The native tests used the real target company and owner. Distributed packages retain sample company CALLDEMO and owner KLINCECUM for recipient installation. Their bytes therefore differ from the target-configured pair. The public manifests report the exact distributed pair as unqualified when no receipt matching those hashes is supplied. This preserves the distinction between demonstrated native behavior and exact-file acceptance; it does not report a native test failure.

Copyright (c) 2026 Kevin P. Lincecum. MIT License.
