using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Domain.Interfaces
{
    public interface IMovimentacaoQueryRequest
    {
        IdempotenciaQueryResponse PegaIdempotenciaPorId(string idIdempotencia);
        Task<MovimentarContaQueryResponse> ObterMovimentosAsync(string idContaCorrente);
    }
}
