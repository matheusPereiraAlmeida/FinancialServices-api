using Moq;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Xunit;

namespace Questao5.Testes.Infrastructure.Database.QueryStore.Requests
{
    public class ContaQueryRequestTests
    {
        private readonly Mock<IDbExecutor> _mockConnection;
        private readonly ContaQueryRequest _service;

        public ContaQueryRequestTests()
        {
            _mockConnection = new Mock<IDbExecutor>();
            _service = new ContaQueryRequest(_mockConnection.Object);
        }

        [Fact]
        public void PegaInformacoesContaPorId_DeveRetornarConta_QuandoEncontrada()
        {
            var mockResult = new
            {
                idcontacorrente = "idConta",
                numero = 123,
                nome = "nome",
                ativo = 1 
            };
            _mockConnection
                .Setup(d => d.QueryFirstOrDefault<dynamic>(
                    It.IsAny<string>(),
                    It.IsAny<object>()))
                .Returns(mockResult);

            var resultado = _service.PegaInformacoesContaPorId("idConta");

            Assert.NotNull(resultado);
            Assert.Equal(mockResult.idcontacorrente, resultado.IdContaCorrente);
            Assert.Equal(mockResult.nome, resultado.Nome);
            Assert.Equal(mockResult.numero, resultado.Numero);
            Assert.True(resultado.Ativo);
        }

        [Fact]
        public void PegaInformacoesContaPorNumeroConta_DeveRetornarConta_QuandoEncontrada()
        {
            var mockResult = new
            {
                idcontacorrente = "idConta",
                numero = 123,
                nome = "nome",
                ativo = 1
            }; 
            _mockConnection
                .Setup(d => d.QueryFirstOrDefault<dynamic>(
                    It.IsAny<string>(),
                    It.IsAny<object>()))
                .Returns(mockResult);

            var resultado = _service.PegaInformacoesContaPorNumeroConta(123);

            Assert.NotNull(resultado);
            Assert.Equal(mockResult.idcontacorrente, resultado.IdContaCorrente);
            Assert.Equal(mockResult.nome, resultado.Nome);
            Assert.Equal(mockResult.numero, resultado.Numero);
            Assert.True(resultado.Ativo);
        }

        [Fact]
        public void PegaInformacoesContaPorId_DeveRetornarNull_QuandoNaoEncontrada()
        {
            _mockConnection
                .Setup(d => d.QueryFirstOrDefault<ContaCorrente>(
                    It.IsAny<string>(),
                    It.IsAny<object>()
                ))
                .Returns(() => null);

            var resultado = _service.PegaInformacoesContaPorId("nao-existe");

            Assert.Null(resultado);
        }
    }
}
