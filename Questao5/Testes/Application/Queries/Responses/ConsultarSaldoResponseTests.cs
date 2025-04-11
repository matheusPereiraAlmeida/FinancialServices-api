using Questao5.Application.Queries.Responses;
using Xunit;

namespace Questao5.Testes.Application.Queries.Responses
{
    public class ConsultarSaldoResponseTests
    {
        [Theory]
        [InlineData(123, "Katherine Sanchez", "01/01/25 : 10:30", "123,45")]
        [InlineData(456, "João Silva", "15/02/25 : 14:55", "987,65")]
        public void Construtor_DeveInicializarPropriedadesCorretamente(int numero, string nome, string dataConsulta, string saldo)
        {
            var response = new ConsultarSaldoResponse(numero, nome, dataConsulta, saldo);

            Assert.Equal(numero, response.Numero);
            Assert.Equal(nome, response.Nome);
            Assert.Equal(dataConsulta, response.DataConsulta);
            Assert.Equal(saldo, response.Saldo);
        }
    }
}
