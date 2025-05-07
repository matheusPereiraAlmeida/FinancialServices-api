using Moq;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Common;
using Questao5.Application.Handlers;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Domain.Language;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Xunit;
using static Questao5.Domain.Enumerators.TipoErro;

namespace Questao5.Testes.Application.Handlers
{
    public class MovimentarContaHandlerTests
    {
        [Fact]
        public async Task Handle_Sucesso()
        {
            // Arrange
            var idConta = Guid.NewGuid().ToString();
            var idRequisicao = Guid.NewGuid().ToString();
            var idMovimentoEsperado = Guid.NewGuid();
            var tipoMovimento = "C";

            var mockContaQuery = new Mock<IContaQueryRequest>();
            var mockMovimentacaoCommand = new Mock<IMovimentacaoCommandRequest>();
            var mockMovimentacaoQuery = new Mock<IMovimentacaoQueryRequest>();
            var mockLogger = new Mock<ILogger<ConsultarSaldoHandler>>();

            var conta = new ContaCorrente(idConta, 123, "nome", true);

            mockContaQuery.Setup(r => r.PegaInformacoesContaPorId(idConta)).Returns(conta);
            mockMovimentacaoQuery.Setup(m => m.PegaIdempotenciaPorId(idRequisicao)).Returns((IdempotenciaQueryResponse?)null);
            mockMovimentacaoCommand.Setup(m => m.MovimentarConta(It.IsAny<MovimentarContaCommand>())).Returns(idMovimentoEsperado);

            var handler = new MovimentarContaHandler(mockContaQuery.Object, mockMovimentacaoCommand.Object, mockMovimentacaoQuery.Object, mockLogger.Object);

            var command = new MovimentarContaCommand(idRequisicao, idConta, 100.50, tipoMovimento);

            var resultado = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(idMovimentoEsperado, resultado.IdMovimento);
        }

        [Theory]
        [InlineData(-10.0, "C", true, false, false, TipoErroDominio.INVALID_VALUE, Mensagens.ValorMovimentoInvalido)]
        [InlineData(100.0, "X", true, false, false, TipoErroDominio.INVALID_TYPE, Mensagens.TipoInvalido)]
        [InlineData(100.0, "C", false, false, false, TipoErroDominio.INVALID_ACCOUNT, Mensagens.ContaNaoEncontrada)]
        [InlineData(100.0, "C", true, false, false, TipoErroDominio.INACTIVE_ACCOUNT, Mensagens.ContaInativa)]
        [InlineData(100.0, "C", true, true, true, TipoErroDominio.ALREADY_EXISTS, Mensagens.OperacaoJaExiste)]
        public async Task Deve_Lancar_RegraDeNegocioException_Para_Casos_Invalidos(double valor, string tipoMovimento, bool contaExiste, bool contaAtiva, bool requisicaoDuplicada, TipoErroDominio tipoEsperado, string mensagemEsperada)
        {
            var idRequisicao = Guid.NewGuid().ToString();
            var idContaCorrente = Guid.NewGuid().ToString();

            var mockContaQuery = new Mock<IContaQueryRequest>();
            var mockMovimentacaoCommand = new Mock<IMovimentacaoCommandRequest>();
            var mockMovimentacaoQuery = new Mock<IMovimentacaoQueryRequest>();
            var mockLogger = new Mock<ILogger<ConsultarSaldoHandler>>();

            if (contaExiste)
            {
                mockContaQuery.Setup(m => m.PegaInformacoesContaPorId(idContaCorrente))
                    .Returns(new ContaCorrente(idContaCorrente, 123, "Cliente Teste", contaAtiva));
            }
            else
            {
                mockContaQuery.Setup(m => m.PegaInformacoesContaPorId(idContaCorrente))
                    .Returns((ContaCorrente?)null);
            }

            if (requisicaoDuplicada)
            {
                mockMovimentacaoQuery.Setup(m => m.PegaIdempotenciaPorId(idRequisicao))
                    .Returns(new IdempotenciaQueryResponse("chave", idRequisicao, "resultado"));
            }
            else
            {
                mockMovimentacaoQuery.Setup(m => m.PegaIdempotenciaPorId(idRequisicao))
                    .Returns((IdempotenciaQueryResponse?)null);
            }

            var handler = new MovimentarContaHandler(
                mockContaQuery.Object,
                mockMovimentacaoCommand.Object,
                mockMovimentacaoQuery.Object,
                mockLogger.Object
            );

            var command = new MovimentarContaCommand(idRequisicao, idContaCorrente, valor, tipoMovimento);

            var excecao = await Assert.ThrowsAsync<RegraDeNegocioException>(() => handler.Handle(command, CancellationToken.None));
            Assert.Equal(tipoEsperado, excecao.TipoErro);
            Assert.Equal(mensagemEsperada, excecao.Message);
        }
    }
}