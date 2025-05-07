using Questao5.Domain.Entities;
using Xunit;

namespace Questao5.Testes.Domain.Entities
{
    public class MovimentacaoTests
    {
        [Theory]
        [InlineData("1", "C123", "2025-04-10", 'C', 100.50)]
        [InlineData("2", "C456", "2025-04-09", 'D', 250.00)]
        [InlineData("3", "C789", "2025-04-08", 'C', 0.00)]
        public void Construtor_DeveAtribuirValoresCorretamente(string idMovimento, string idContaCorrente, string dataStr, char tipo, double valor)
        {
            var data = DateTime.Parse(dataStr);
            var movimentacao = new Movimentacao(idMovimento, idContaCorrente, data, tipo, valor);
            Assert.Equal(idMovimento, movimentacao.IdMovimento);
            Assert.Equal(idContaCorrente, movimentacao.IdContaCorrente);
            Assert.Equal(data, movimentacao.DataMovimento);
            Assert.Equal(tipo, movimentacao.TipoMovimento);
            Assert.Equal(valor, movimentacao.Valor, 2);
        }

        [Fact]
        public void ConstrutorPadrao_DeveInicializarPropriedadesComDefault()
        {
            var movimentacao = new Movimentacao();
            Assert.Null(movimentacao.IdMovimento);
            Assert.Null(movimentacao.IdContaCorrente);
            Assert.Equal(default, movimentacao.DataMovimento);
            Assert.Equal(default, movimentacao.TipoMovimento);
            Assert.Equal(0, movimentacao.Valor);
        }
    }
}
