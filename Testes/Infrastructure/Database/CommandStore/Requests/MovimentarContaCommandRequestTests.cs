using Moq;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Common;
using Questao5.Domain.Language;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using static Questao5.Domain.Enumerators.TipoErro;
using Xunit;
using Questao5.Domain.Interfaces;

namespace Questao5.Testes.Infrastructure.Database.CommandStore.Requests
{
    public class MovimentarContaCommandRequestTests
    {
        private readonly Mock<IDbExecutor> _mockConnection;
        private readonly Mock<ILogger<MovimentarContaCommandRequest>> _mockLogger;
        private readonly MovimentarContaCommandRequest _service;

        public MovimentarContaCommandRequestTests()
        {
            _mockConnection = new Mock<IDbExecutor>();
            _mockLogger = new Mock<ILogger<MovimentarContaCommandRequest>>();
            _service = new MovimentarContaCommandRequest(_mockConnection.Object, _mockLogger.Object);
        }

        [Fact]
        public void MovimentarConta_DeveRetornarIdMovimento_QuandoInsercaoForBemSucedida()
        {
            var mockCommand = new MovimentarContaCommand("idRequisicao", "idContaCorrente", 100, "credito");
            _mockConnection
                      .Setup(c => c.Execute(It.IsAny<string>(), It.IsAny<object>()))
                      .Returns(1); 

            var result = _service.MovimentarConta(mockCommand);

            Assert.NotEqual(Guid.Empty, result);
            _mockConnection.Verify(c => c.Execute(It.IsAny<string>(), It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void MovimentarConta_DeveLancarExcecao_QuandoFalhaNaPersistenciaDaIdempotencia()
        {
            var mockCommand = new MovimentarContaCommand("idRequisicao", "idContaCorrente", 100, "credito");
            _mockConnection
                .Setup(c => c.Execute(It.IsAny<string>(), It.IsAny<object>()))
                .Returns(() => 0); 

            var ex = Assert.Throws<RegraDeNegocioException>(() => _service.MovimentarConta(mockCommand));
            Assert.Equal(TipoErroDominio.NO_ROW_AFFECTED, ex.TipoErro);
            Assert.Equal(Mensagens.IdempotenciaNaoPersistida, ex.Message);
        }

        [Fact]
        public void MovimentarConta_DeveLancarExcecao_QuandoFalhaNaPersistenciaDoMovimento()
        {
            var mockCommand = new MovimentarContaCommand("idRequisicao", "idContaCorrente", 100, "credito");
            int callCount = 0;

            _mockConnection
                .Setup(c => c.Execute(It.IsAny<string>(), It.IsAny<object>()))
                .Returns(() =>
                {
                    callCount++;
                    return callCount == 1 ? 1 : 0; // 1º insert (idempotência) ok, 2º insert (movimento) falha
                });

            var ex = Assert.Throws<RegraDeNegocioException>(() => _service.MovimentarConta(mockCommand));
            Assert.Equal(TipoErroDominio.NO_ROW_AFFECTED, ex.TipoErro);
            Assert.Equal(Mensagens.MovimentacaoNaoPersistida, ex.Message);
        }
    }
}
