# REST conversion behavior and accepted limitation

Call retains its existing Newtonsoft.Json and System.Text.Json dependencies and its existing calling APIs. System.Text.Json validates the REST request and preserves each original JSON value; Newtonsoft.Json converts that value to the target's declared type. Server-side calls pass real objects without a JSON round trip.

## Accepted extreme-input limitation

The October 10, 2026 review reproduced process stack overflow in isolated local tests using deliberately constructed objects nested thousands of levels deep. No ordinary Epicor request, actual application consumer or deployed incident was shown to trigger it. Kevin accepted this extreme-input limitation and directed that Call remain simple and clean. The finding is retained as evidence, not an outstanding redesign requirement.

The experimental generic-table materializer and its dedicated tests were removed from maintained source and retained in local historical evidence. RestCore was restored byte-for-byte from its pre-repair working-tree snapshot. No extra REST route, compiled reader helper, embedded assembly, input-type restriction or numeric depth cap is part of this repair. Existing diagnostic, dispatch-once and Throw/Return behavior remains unchanged.

## Public-release rule

Call content intended for public release must produce no genuine Epicor warnings. Do not hide, suppress, relabel or bypass warnings to manufacture a clean result; use a supported workaround and verify the actual released source/package. This is not a blanket prohibition on ordinary runtime type APIs. Kevin approved the existing declared-type operations after their native compilation reported zero errors and zero warnings.

Local tests, native compilation/readback, ordinary-user behavior and consumer adoption remain separate evidence. Historical tests of the removed materializer do not qualify the final restored conversion path as stack-safe at extreme depth. Reopen this finding only for a demonstrated practical requirement or new user direction.
