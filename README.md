# Call

A modern take on Epicor dynamic dispatch. Based on FunctionRunner.

Call invokes an Epicor Function selected at runtime. Server callers pass actual objects. REST callers supply positional or named JSON arguments.

Call is the product. CallLib is its Function library. CallUBAQ is the required dispatch BAQ.

## Start here

1. Read [Install Call](docs/INSTALL.md) and select the matching package lane.
2. Import the packages, then configure the real company, owner, and security settings.
3. Read the [SDK guide](docs/SDK.md) for API contracts and examples.
4. Use [UseCall.cs](examples/UseCall.cs) for reusable server methods.

## Contents

- [SDK guide](docs/SDK.md) and its PDF in pdf/Call-SDK-Guide.pdf.
- [Press release](posts/03-Call-Press-Release.md) and its PDF in pdf/Call-Press-Release.pdf.
- [AI integration guide](docs/AI.md) and [AI kit instructions](docs/AI-RELEASE.md).
- Portable AI archives in ai/ and an unpacked skill in skills/call/.
- Matching native package pairs in dist/5.1.100/ and dist/5.2.100/.
- [Release status](docs/RELEASE-STATUS.md), [MIT license](LICENSE), and [notices](NOTICE.md).
- SHA-256 hashes in manifest.json for every distributed file except that manifest itself.

This bundle contains no build tooling or tests. The package code and the small consumer helper remain covered by the MIT license.

The packages use sample company CALLDEMO and owner KLINCECUM. Configure company mappings, ownership, and security after import. Read the release status for the tested scope.

Call Workbench is a separate product and is not included.

Copyright (c) 2026 Kevin P. Lincecum. Copyright belongs solely to Kevin P. Lincecum.
