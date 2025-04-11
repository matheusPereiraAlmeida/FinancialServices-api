using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Common;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Interfaces;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConsultarSaldoQuery _consultarSaldoQuery;
        public ContaCorrenteController(IMediator mediator, IConsultarSaldoQuery consultarSaldoQuery)
        {
            _mediator = mediator;
            _consultarSaldoQuery = consultarSaldoQuery;
        }

        /// <summary>
        /// Consulta o saldo da conta corrente a partir do número da conta.
        /// </summary>
        /// <param name="numeroConta">Número da conta corrente</param>
        /// <returns>Saldo da conta corrente</returns>
        /// <response code="200">Saldo retornado com sucesso</response>
        /// <response code="400">Conta inválida</response>
        [HttpGet("consultaSaldoPorNumero/{numeroConta}")]
        [ProducesResponseType(typeof(ConsultarSaldoResponse), 200)]
        [ProducesResponseType(typeof(Exception), 400)]
        public async Task<IActionResult> ConsultaSaldoPorNumero([FromRoute] int numeroConta)
        {
            try
            {
                _consultarSaldoQuery.PreencheNumeroConta(numeroConta);
                var resultado = await _mediator.Send(_consultarSaldoQuery);
                return Ok(resultado);

            }
            catch (RegraDeNegocioException ex)
            {
                return BadRequest(new
                {
                    tipoErro = ex.TipoErro.ToString(),
                    mensagemErro = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    tipoErro = ex.ToString(),
                    mensagemErro = ex.Message
                });
            }
        }

        /// <summary>
        /// Consulta o saldo da conta corrente a partir do ID da conta.
        /// </summary>
        /// <param name="contaId">Id da conta corrente.</param>
        /// <returns>Saldo da conta corrente.</returns>
        /// <response code="200">Saldo retornado com sucesso</response>
        /// <response code="400">Conta inválida</response>
        [HttpGet("ConsultarSaldoPorId/{contaId}")]
        [ProducesResponseType(typeof(ConsultarSaldoResponse), 200)]
        [ProducesResponseType(typeof(Exception), 400)]
        public async Task<IActionResult> ConsultarSaldoPorId([FromRoute] string contaId)
        {
            try
            {
                _consultarSaldoQuery.PreencheContaId(contaId);
                var resultado = await _mediator.Send(_consultarSaldoQuery);
                return Ok(resultado);

            }
            catch (RegraDeNegocioException ex)
            {
                return BadRequest(new
                {
                    tipoErro = ex.TipoErro.ToString(),
                    mensagemErro = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    tipoErro = ex.ToString(),
                    mensagemErro = ex.Message
                });
            }
        }

        /// <summary>
        /// Realiza uma movimentação (crédito ou débito) em uma conta corrente usando o ID da conta
        /// </summary>
        /// <param name="request">Dados da movimentação.</param>
        /// <returns>Id do movimento gerado.</returns>
        /// <response code="200">Movimentação realizada com sucesso.</response>
        /// <response code="400">Dados inválidos para movimentação.</response>
        [HttpPost("MovimentarConta")]
        public async Task<IActionResult> RealizarMovimentacao([FromBody] MovimentarContaCommand request)
        {
            try
            {
                var resultado = await _mediator.Send(request);
                return Ok(resultado);
            }
            catch (RegraDeNegocioException ex)
            {
                return BadRequest(new
                {
                    tipoErro = ex.TipoErro.ToString(),
                    mensagemErro = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    tipoErro = ex.ToString(),
                    mensagemErro = ex.Message
                });
            }
        }
    }
}
