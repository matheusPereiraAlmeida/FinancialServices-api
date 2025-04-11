using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Xunit;

namespace Questao5.Testes.Infrastructure.Database.QueryStore.Responses
{
    public class MovimentarContaQueryResponseTests
    {
        [Fact]
        public void Construtor_DeveInicializarPropriedadesCorretamente()
        {
            var dataConsulta = new DateTime(2025, 4, 10, 14, 0, 0);
            var movimentacoes = new List<Movimentacao>
            {
                new(Guid.NewGuid().ToString(), "idConta", DateTime.Today, 'c', 100)
            };

            var response = new MovimentarContaQueryResponse(dataConsulta, movimentacoes);

            Assert.Equal(dataConsulta, response.DataConsulta);
            Assert.Equal(movimentacoes, response.Movimentacoes);
        }

    }
}
