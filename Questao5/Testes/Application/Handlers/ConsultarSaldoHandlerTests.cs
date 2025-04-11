using Moq;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Xunit;

namespace Questao5.Testes.Application.Handlers
{
    public class ConsultarSaldoHandlerTests
    {
        [Fact]
        public async Task Handle_DeveRetornarSaldoCorreto_QuandoContaExisteEHaMovimentacoes()
        {
            var contaId = "conta123";
            var conta = new ContaCorrente(contaId, 1234, "teste", true);

            var movimentacoes = new List<Movimentacao>
            {
                new("idMovimento", "idConta", DateTime.Today, 'C', 100),
                new("idMovimento", "idConta", DateTime.Today, 'D', 40)
            };

            var dataConsulta = new DateTime(2025, 4, 11);

            var mockContaQueryRepo = new Mock<IContaQueryRequest>();
            mockContaQueryRepo.Setup(r => r.PegaInformacoesContaPorId(contaId))
                .Returns(conta);

            var mockMovimentacaoQueryRepo = new Mock<IMovimentacaoQueryRequest>();
            mockMovimentacaoQueryRepo.Setup(r => r.ObterMovimentosAsync(contaId))
                .ReturnsAsync(new MovimentarContaQueryResponse(dataConsulta, movimentacoes));

            var mockLogger = new Mock<ILogger<ConsultarSaldoHandler>>();

            var handler = new ConsultarSaldoHandler(mockContaQueryRepo.Object, mockMovimentacaoQueryRepo.Object, mockLogger.Object);

            var request = new ConsultarSaldoQuery();
            request.PreencheContaId(contaId);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.Equal(1234, result.Numero);
            Assert.Equal("teste", result.Nome);
            Assert.Equal(dataConsulta.ToString("dd/MM/yyyy : HH:mm"), result.DataConsulta);
            Assert.Equal("60,00", result.Saldo); // 100 - 40 = 60
        }
    }
}
