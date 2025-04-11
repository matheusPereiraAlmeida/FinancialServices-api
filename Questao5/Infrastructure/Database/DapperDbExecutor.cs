using Dapper;
using Questao5.Domain.Interfaces;
using System.Data;

namespace Questao5.Infrastructure.Database
{
    public class DapperDbExecutor : IDbExecutor
    {
        private readonly IDbConnection _connection;

        public DapperDbExecutor(IDbConnection connection)
        {
            _connection = connection;
        }

        public int Execute(string sql, object param = null)
        {
            return _connection.Execute(sql, param);
        }

        public T QueryFirstOrDefault<T>(string sql, object param = null)
        {
            return _connection.QueryFirstOrDefault<T>(sql, param);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)
        {
            return _connection.QueryAsync<T>(sql, param);
        }
    }
}
