using System.Data;

namespace Raizen.Cliente.Common.InfraStructure.DbConnectionFactories
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        public string ConnectionString { get; }

        public string ReadConnectionString { get; }
        public DbConnectionFactory(string connectionString)
        {
            ConnectionString = connectionString;
            ReadConnectionString = connectionString;

        }

        public DbConnectionFactory(string connectionString,
            string readConnectionString) : this(connectionString)
        {
            ReadConnectionString = connectionString;
        }

        public IDbConnection GetConnection()
        {
            return new Microsoft.Data.SqlClient.SqlConnection(ConnectionString);
        }

        public IDbConnection GetReadConnection()
        {
            return new Microsoft.Data.SqlClient.SqlConnection(ReadConnectionString);
        }
    }
}
