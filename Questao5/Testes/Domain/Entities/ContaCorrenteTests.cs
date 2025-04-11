using Questao5.Domain.Entities;
using Xunit;

namespace Questao5.Testes.Domain.Entities
{
    public class ContaCorrenteTests
    {
        [Theory]
        [InlineData("abc123", 123, "João da Silva", true)]
        [InlineData("def456", 456, "Maria Souza", false)]
        [InlineData("ghi789", 789, "Carlos Alberto", true)]
        public void ContaCorrente_DeveInicializarComValoresCorretos(string id, int numero, string nome, bool ativo)
        {
            var conta = new ContaCorrente(id, numero, nome, ativo);

            Assert.Equal(id, conta.IdContaCorrente);
            Assert.Equal(numero, conta.Numero);
            Assert.Equal(nome, conta.Nome);
            Assert.Equal(ativo, conta.Ativo);
        }
    }
}
