# Call release status

Edition: 2026-10-08. Methods contract: 1. Package lanes: 5.1.100 and 5.2.100.

Copyright (c) 2026 Kevin P. Lincecum. MIT License.

## Distribution

The public bundle contains documentation, PDFs, native CallLib and CallUBAQ packages, and the AI skill with its consumer helper.

Build tooling, tests, private receipts, installation credentials, and development history are excluded.

The packages use sample company CALLDEMO and owner KLINCECUM. Import directly, then configure company mappings, ownership, and security. No source build is required.

## Review scope

The release review checks both version lanes, source inclusion, deterministic packages, checksums, links, legal notices, and embedded metadata.

The local simulated tests exercise the server and REST paths. The AI examples compile and execute against the simulated host.

The Markdown and PDF content are compared. Rendered PDF pages receive visual review.

The final edition removes unsupported fixed argument-size, argument-count, small JSON-depth, and message-length restrictions.

This is a runtime change from earlier native installations. Local coverage includes requests beyond the former restrictions and retains invalid-input checks.

## Native evidence

Earlier native work demonstrated the DynamicQuery signature route and the UBAQ InvokeFunction bridge. Call does not call the EFX business object.

The corrected 5.2.100 packages passed an isolated native import test with dummy company CALLDEMO and owner KLINCECUM. Epicor assigned the real import company and retained the owner. The library required a real-company mapping, and the BAQ directive required native compilation. REST dispatch then succeeded with zero compiler errors or warnings.

The test used isolated identities. It did not repeat native import on a 5.1.100 server, ordinary-user regression, or exact raw HTTP Decimal acceptance. Check the intended destination and consumer route after installation.

The owner account KLINCECUM is included at the author's request. Private destination identifiers, raw responses, and test receipts are excluded.
