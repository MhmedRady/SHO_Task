﻿using Microsoft.Data.SqlClient;
using SHO_Task.Application.Abstractions;
using System.Data;

namespace SHO_Task.Infrastructure;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();

        return connection;
    }
}
