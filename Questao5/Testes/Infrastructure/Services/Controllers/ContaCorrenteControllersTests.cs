using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Services.Controllers;
using Xunit;

namespace Questao5.Testes.Infrastructure.Services.Controllers
{
    public class ContaCorrenteControllerTests
    {
        [Theory]
        [InlineData(123, "100.00", "Matheus")]
        [InlineData(456, "250.50", "João")]
        [InlineData(789, "0.00", "Maria")]
        public async Task ConsultaSaldoPorNumero_DeveRetornarOk_ComSaldo(int numeroConta, string saldo, string nome)
        {
            var mockMediator = new Mock<IMediator>();
            var mockConsultaQuery = new Mock<IConsultarSaldoQuery>();

            var dataConsulta = DateTime.MinValue.ToString();
            var resultadoEsperado = new ConsultarSaldoResponse(numeroConta, nome, dataConsulta, saldo);

            mockConsultaQuery.Setup(q => q.PreencheNumeroConta(numeroConta));
            mockMediator.Setup(m => m.Send(mockConsultaQuery.Object, default))
                        .ReturnsAsync(resultadoEsperado);

            var controller = new ContaCorrenteController(mockMediator.Object, mockConsultaQuery.Object);
            var resultado = await controller.ConsultaSaldoPorNumero(numeroConta);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var response = Assert.IsType<ConsultarSaldoResponse>(okResult.Value);
            Assert.Equal(numeroConta, response.Numero);
            Assert.Equal(saldo, response.Saldo);
            Assert.Equal(nome, response.Nome);
        }
    }
}
