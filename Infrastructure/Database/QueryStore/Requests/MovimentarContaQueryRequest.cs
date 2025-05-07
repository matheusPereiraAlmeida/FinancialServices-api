using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using System.Data;

namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
    public class MovimentarContaQueryRequest : IMovimentacaoQueryRequest
    {
        private readonly IDbExecutor _connection;

        public MovimentarContaQueryRequest(IDbExecutor connection)
        {
            _connection = connection;
        }

        public IdempotenciaQueryResponse PegaIdempotenciaPorId(string idIdempotencia)
        {
            const string sql = @"
                SELECT chave_idempotencia AS ChaveIdempotencia, requisicao, resultado
                FROM idempotencia 
                WHERE requisicao = @idIdempotencia";

            return _connection.QueryFirstOrDefault<IdempotenciaQueryResponse>(sql, new { idIdempotencia });
        }

        public async Task<MovimentarContaQueryResponse> ObterMovimentosAsync(string idContaCorrente)
        {
            const string sql = @"
                SELECT idmovimento, idcontacorrente, datamovimento, tipomovimento, valor
                FROM movimento 
                WHERE idcontacorrente = @idContaCorrente";

            var movimentacoes = await _connection.QueryAsync<Movimentacao>(sql, new { idContaCorrente });
            var dataConsulta = DateTime.Now;

            return new MovimentarContaQueryResponse(dataConsulta, movimentacoes);
        }
    }
}
