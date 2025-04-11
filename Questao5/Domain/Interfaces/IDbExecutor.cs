using System.Data;

namespace Questao5.Domain.Interfaces
{
    public interface IDbExecutor
    {
        int Execute(string sql, object param = null);
        T QueryFirstOrDefault<T>(string sql, object param = null);
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null);
    }
}
