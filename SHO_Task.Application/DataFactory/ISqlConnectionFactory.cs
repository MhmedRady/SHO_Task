using System.Data;

namespace SHO_Task.Application.Abstractions;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
