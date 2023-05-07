using System.Data;

namespace PodkarpackiLekarz.Shared.Persistence;
public interface ISqlConnectionFactory
{
    IDbConnection GetOpenConnection();

    IDbConnection CreateNewConnection();

    string GetConnectionString();
}
