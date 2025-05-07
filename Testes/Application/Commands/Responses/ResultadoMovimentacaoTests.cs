using Questao5.Application.Commands.Responses;
using Xunit;

namespace Questao5.Testes.Application.Commands.Responses
{
    public class ResultadoMovimentacaoTests
    {
        [Fact]
        public void Construtor_DeveInicializar_IdMovimento_Corretamente()
        {
            var idMovimentoEsperado = Guid.NewGuid();

            var resultado = new ResultadoMovimentacao(idMovimentoEsperado);

            Assert.Equal(idMovimentoEsperado, resultado.IdMovimento);
        }
    }
}
