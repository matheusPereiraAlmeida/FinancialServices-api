using Questao5.Infrastructure.Database.QueryStore.Responses;
using Xunit;

namespace Questao5.Testes.Infrastructure.Database.QueryStore.Responses
{
    public class IdempotenciaQueryResponseTests
    {
        [Fact]
        public void Construtor_DeveInicializarPropriedadesCorretamente()
        {
            var chaveIdempotencia = "abc-123";
            var requisicao = "req-456";
            var resultado = "OK";

            var response = new IdempotenciaQueryResponse(chaveIdempotencia, requisicao, resultado);

            Assert.Equal(chaveIdempotencia, response.ChaveIdempotencia);
            Assert.Equal(requisicao, response.Requisicao);
            Assert.Equal(resultado, response.Resultado);
        }
    }
}
