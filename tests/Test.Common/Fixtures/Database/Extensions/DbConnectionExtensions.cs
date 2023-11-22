﻿using System.Data;
using System.Data.Common;

namespace Test.Common.Fixtures.Database.Extensions;

public static class DbConnectionExtensions
{
    public static async ValueTask<bool> TryOpenAsync(this DbConnection connection, CancellationToken cancellationToken)
    {
        if (connection.State is ConnectionState.Open)
            return false;

        await connection.OpenAsync(cancellationToken);
        return true;
    }
}