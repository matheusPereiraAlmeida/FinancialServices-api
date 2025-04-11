using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.QueryStore.Responses
{
    public class MovimentarContaQueryResponse
    {
        public IEnumerable<Movimentacao> Movimentacoes { get; }
        public DateTime DataConsulta { get; }

        public MovimentarContaQueryResponse(DateTime dataConsulta, IEnumerable<Movimentacao> movimentacoes)
        {
            DataConsulta = dataConsulta;
            Movimentacoes = movimentacoes;
        }
    }
}
