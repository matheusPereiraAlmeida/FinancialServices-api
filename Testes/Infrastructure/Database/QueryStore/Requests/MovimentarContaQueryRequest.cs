using Moq;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Xunit;

namespace Questao5.Testes.Infrastructure.Database.QueryStore.Requests
{
    public class MovimentarContaQueryRequestTests
    {
        private readonly Mock<IDbExecutor> _mockConnection;
        private readonly MovimentarContaQueryRequest _service;

        public MovimentarContaQueryRequestTests()
        {
            _mockConnection = new Mock<IDbExecutor>();
            _service = new MovimentarContaQueryRequest(_mockConnection.Object);
        }

        [Fact]
        public void PegaIdempotenciaPorId_DeveRetornarResposta_QuandoExiste()
        {
            var expected = new IdempotenciaQueryResponse("chave", "requisicao", "resultado");
            _mockConnection
                .Setup(d => d.QueryFirstOrDefault<IdempotenciaQueryResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>()
                ))
                .Returns(expected);

            var result = _service.PegaIdempotenciaPorId("req-123");

            Assert.NotNull(result);
            Assert.Equal("chave", result.ChaveIdempotencia);
            Assert.Equal("requisicao", result.Requisicao);
            Assert.Equal("resultado", result.Resultado);
        }

        [Fact]
        public async Task ObterMovimentosAsync_DeveRetornarMovimentos()
        {
            var movimentos = new List<Movimentacao>
        {
            new("1","conta-1", DateTime.Today, 'c', 100 ),
            new("2","conta-1", DateTime.Today, 'd', 50 )
        };

            _mockConnection
                .Setup(d => d.QueryAsync<Movimentacao>(
                    It.IsAny<string>(),
                    It.IsAny<object>()
                ))
                .ReturnsAsync(movimentos);

            var result = await _service.ObterMovimentosAsync("conta-1");

            Assert.NotNull(result);
            Assert.Equal(2, result.Movimentacoes.Count());
            Assert.Contains(result.Movimentacoes, m => m.TipoMovimento == 'c');
            Assert.True(result.DataConsulta <= DateTime.Now);
        }
    }
}
