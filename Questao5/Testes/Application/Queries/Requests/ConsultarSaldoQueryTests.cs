using Moq;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;
using Xunit;

namespace Questao5.Testes.Application.Queries.Requests
{
    public class ConsultarSaldoQueryTests
    {
        [Theory]
        [InlineData(123)]
        [InlineData(456)]
        public void PreencheNumeroConta_DeveDefinirNumeroContaETipoBusca(int numeroConta)
        {
            int valorRecebido = 0;
            TipoBuscaConta tipoBuscaRecebido = TipoBuscaConta.PorId;

            var mockQuery = new Mock<IConsultarSaldoQuery>();
            mockQuery.Setup(o => o.PreencheNumeroConta(It.IsAny<int>()))
                .Callback<int>(num =>
                {
                    valorRecebido = numeroConta;
                    tipoBuscaRecebido = TipoBuscaConta.PorNumero;
                });

            mockQuery.Object.PreencheNumeroConta(numeroConta);

            Assert.Equal(numeroConta, valorRecebido);
            Assert.Equal(TipoBuscaConta.PorNumero, tipoBuscaRecebido);
        }

        [Theory]
        [InlineData("abc123")]
        [InlineData("xyz789")]
        public void PreencheContaId_DeveDefinirContaIdETipoBusca(string id)
        {
            string contaId = string.Empty;
            TipoBuscaConta tipoBuscaRecebido = TipoBuscaConta.PorId;

            var mockQuery = new Mock<IConsultarSaldoQuery>();
            mockQuery.Setup(o => o.PreencheContaId(It.IsAny<string>()))
                .Callback<string>(num =>
                {
                    contaId = id;
                    tipoBuscaRecebido = TipoBuscaConta.PorNumero;
                });

            mockQuery.Object.PreencheContaId(id);

            Assert.Equal(id, contaId);
            Assert.Equal(TipoBuscaConta.PorNumero, tipoBuscaRecebido);
        }
    }
}
