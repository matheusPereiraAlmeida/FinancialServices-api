using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using System.Data;

namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
    public class ContaQueryRequest : IContaQueryRequest
    {
        private readonly IDbExecutor _connection;

        public ContaQueryRequest(IDbExecutor connection)
        {
            _connection = connection;
        }

        public ContaCorrente PegaInformacoesContaPorId(string contaId)
        {
            const string sql = @"
                SELECT idcontacorrente, numero, nome, ativo
                FROM contacorrente 
                WHERE idcontacorrente = @contaId";

            var result = _connection.QueryFirstOrDefault<dynamic>(sql, new { contaId });
            if (result == null)
                return null;

            return new ContaCorrente(
                result.idcontacorrente,
                (int)result.numero,
                result.nome,
                Convert.ToBoolean(result.ativo)
            );
        }

        public ContaCorrente PegaInformacoesContaPorNumeroConta(int numeroConta)
        {
            const string sql = @"
                SELECT idcontacorrente, numero, nome, ativo
                FROM contacorrente 
                WHERE numero = @numeroConta";

            var result = _connection.QueryFirstOrDefault<dynamic>(sql, new { numeroConta });
            if (result == null)
                return null;

            return new ContaCorrente(
                result.idcontacorrente,
                (int)result.numero,
                result.nome,
                Convert.ToBoolean(result.ativo)
            );
        }
    }
}