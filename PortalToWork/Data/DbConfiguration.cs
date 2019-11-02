using System;
using Npgsql;

namespace PortalToWork.Data
{
    public class DbConfiguration
    {
        private NpgsqlConnectionStringBuilder _connectionStringBuilder;
        
        public DbConfiguration()
        {
            _connectionStringBuilder = new NpgsqlConnectionStringBuilder();
            
            _connectionStringBuilder.Host = Environment.GetEnvironmentVariable("DB_SERVER");
            _connectionStringBuilder.Username = Environment.GetEnvironmentVariable("DB_USER");
            _connectionStringBuilder.Password = Environment.GetEnvironmentVariable("DB_PASSWORD");
            _connectionStringBuilder.Database = Environment.GetEnvironmentVariable("DB_NAME");

            var port = Environment.GetEnvironmentVariable("DB_PORT");

            if (port != null)
            {
                _connectionStringBuilder.Port = int.Parse(port);
            }
        }

        public override string ToString()
        {
            return _connectionStringBuilder.ToString();
        }
    }
}