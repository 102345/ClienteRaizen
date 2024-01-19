using System.Data;

namespace Raizen.Cliente.Common.InfraStructure.DbConnectionFactories
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection();

        IDbConnection GetReadConnection();
    }
}
