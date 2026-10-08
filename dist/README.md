# Call native package lanes

Use the package version that matches the destination's native Epicor/ICE version.

| Package version | Required pair |
|---|---|
| 5.1.100 | 5.1.100/CallLib.efxj and 5.1.100/CallUBAQ.baq |
| 5.2.100 | 5.2.100/CallLib.efxj and 5.2.100/CallUBAQ.baq |

Keep each pair together with its manifest and legal files. Never change a version header to claim compatibility.

Both pairs use sample company CALLDEMO and owner KLINCECUM. Import directly, then follow [installation](../docs/INSTALL.md) for company, ownership, security, and compilation settings.

Read [release status](../docs/RELEASE-STATUS.md) for the verification boundary.
