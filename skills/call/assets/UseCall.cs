// Copyright (c) 2026 Kevin P. Lincecum
// SPDX-License-Identifier: MIT

// Paste this helper inside a consuming Function. It turns Get's DataSet into named, typed methods.
// Validate the table before casting so incompatible versions fail with a useful message.
(Func<string, string, object[], (bool Success, object[] Outputs, DataSet Details)> Run,
 Func<string, string, object, (bool Success, object[] Outputs, DataSet Details)> Packed) UseCall(Func<DataSet> acquire)
{
    if (acquire == null) throw new ArgumentNullException(nameof(acquire));
    var carrier = acquire();
    if (carrier == null || carrier.Tables.Count != 1 || !carrier.Tables.Contains("Methods"))
        throw new Ice.BLException("Call did not return its methods.");
    // Validate the carrier before casting delegates from a separately compiled library.
    var table = carrier.Tables["Methods"];
    if (table.Rows.Count != 1 || table.Rows[0].RowState == DataRowState.Deleted ||
        !table.Columns.Contains("Version") || table.Columns["Version"].DataType != typeof(int) ||
        !table.Columns.Contains("Run") || !table.Columns.Contains("Packed"))
        throw new Ice.BLException("Call returned an invalid methods table.");
    var row = table.Rows[0];
    if (!(row["Version"] is int version) || version != 1)
        throw new Ice.BLException("This consumer requires Call contract 1.");
    var run = row["Run"] as Func<string, string, object[], (bool Success, object[] Outputs, DataSet Details)>;
    var packed = row["Packed"] as Func<string, string, object, (bool Success, object[] Outputs, DataSet Details)>;
    if (run == null || packed == null) throw new Ice.BLException("Call returned incompatible method types.");
    return (run, packed);
}
