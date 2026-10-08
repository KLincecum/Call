# Call AI documentation and skill

Copyright (c) 2026 Kevin P. Lincecum. MIT License.

This kit teaches an AI assistant to generate and review consumers of Call for Epicor.

## Start with the integration guide

Give the assistant `AI-INTEGRATION.md` and the target Function's signature. Include the caller location and desired error policy.

Example request:

> Use the Call integration guide to write a server consumer. The target is Pricing.Calculate. Inputs are iPartNum (String) and iQuantity (Decimal), in that order. Its only output is oPrice (Decimal). Use Return mode. Explain argument construction and failure handling. Treat the target as illustrative and do not execute it.

The example describes a hypothetical target. Replace it with a verified target before executing generated code.

For reusable calls, also provide `call/assets/UseCall.cs`. The helper is source code to embed in the consuming Epicor Function.

For package selection and installation, provide `AI-INSTALLATION.md` and the matching native package pair from the Call release.

## Use the skill

Extract the complete `call` folder into a skill directory supported by your AI host.

Keep its references, assets, manifest, and LICENSE together. Do not copy only SKILL.md.

For Codex, place the folder under your configured skills directory. Start a new session if the host does not refresh skills automatically.

Then request:

> Use $call to generate a consumer from this verified target signature. Explain the selected entry point, argument types, outputs, and error handling.

For an AI host without skill discovery, provide `call/SKILL.md` as instructions. Supply the referenced files when the assistant needs them.

Do not paste the helper into a system prompt as executable instructions. Provide it as source material for the consumer implementation.

## Contents

The following paths refer to the extracted Call-AI-Kit.zip archive. Native packages are supplied separately in the full Call release.

| Item | Purpose |
|---|---|
| AI-INTEGRATION.md | Worked examples, argument shapes, REST binding, and recovery |
| AI-INSTALLATION.md | Package preparation, native installation, and evidence boundaries |
| call/SKILL.md | Short task instructions and reference selection |
| call/references/ | Guides copied from the maintained documentation |
| call/assets/UseCall.cs | Exact maintained consumer helper |
| call/manifest.json | Resource hashes, contract version, and supported package lanes |
| LICENSE | MIT license and personal copyright |

The kit does not contain Epicor assemblies, credentials, native exports, or business data.
The skill does not install Call into Epicor or grant execution permission.

## Compatibility

The methods contract is version 1. The maintained native package lanes are 5.1.100 and 5.2.100.

Select packages from the verified destination version. Installing this skill does not establish native compatibility or deployment state.

Use the supplied installation guide for the distinction between local checks and native acceptance.
