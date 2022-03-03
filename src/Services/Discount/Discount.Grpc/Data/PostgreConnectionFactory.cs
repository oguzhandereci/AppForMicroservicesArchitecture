using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Data;

namespace Discount.Grpc.Data
{
    public class PostgreConnectionFactory : IDbConnectionFactory, IDisposable
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _connection;

        public PostgreConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetOpenConnection()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                _connection = new NpgsqlConnection(_configuration.GetValue<string>("ConnectionStrings:PostgreConnection"));
                _connection.Open();
            }

            return _connection;
        }
        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
