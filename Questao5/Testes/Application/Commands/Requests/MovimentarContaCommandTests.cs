using Questao5.Application.Commands.Requests;
using Xunit;

namespace Questao5.Testes.Application.Commands.Requests
{
    public class MovimentarContaCommandTests
    {
        [Fact]
        public void Construtor_DeveInicializarPropriedadesCorretamente()
        {
            var idRequisicao = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";
            var idContaCorrente = "A7BCDE88-1234-4567-8910-1234567890AB";
            var valor = 123.45;
            var tipoMovimento = "C";

            var command = new MovimentarContaCommand(idRequisicao, idContaCorrente, valor, tipoMovimento);

            Assert.Equal(idRequisicao, command.IdRequisicao);
            Assert.Equal(idContaCorrente, command.IdContaCorrente);
            Assert.Equal(valor, command.Valor);
            Assert.Equal(tipoMovimento, command.TipoMovimento);
        }
    }
}
