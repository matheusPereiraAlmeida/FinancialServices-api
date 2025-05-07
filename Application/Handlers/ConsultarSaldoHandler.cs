using MediatR;
using Questao5.Application.Common;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;
using Questao5.Domain.Language;
using static Questao5.Domain.Enumerators.TipoErro;

namespace Questao5.Application.Handlers
{
    public class ConsultarSaldoHandler : IRequestHandler<ConsultarSaldoQuery, ConsultarSaldoResponse>
    {
        private readonly IContaQueryRequest _contaQueryRepo;
        private readonly IMovimentacaoQueryRequest _movimentacaoQueryRepo;
        private readonly ILogger<ConsultarSaldoHandler> _logger;

        public ConsultarSaldoHandler(IContaQueryRequest contaQueryRepo, IMovimentacaoQueryRequest movimentacaoQueryRepo, ILogger<ConsultarSaldoHandler> logger)
        {
            _contaQueryRepo = contaQueryRepo;
            _movimentacaoQueryRepo = movimentacaoQueryRepo;
            _logger = logger;
        }

        public async Task<ConsultarSaldoResponse> Handle(ConsultarSaldoQuery request, CancellationToken cancellationToken)
        {
            var conta = request.TipoBusca == TipoBuscaConta.PorId ? _contaQueryRepo.PegaInformacoesContaPorId(request.ContaId) : _contaQueryRepo.PegaInformacoesContaPorNumeroConta(request.NumeroConta);            
            ValidaRegrasNegocio(conta);

            var movimentos = _movimentacaoQueryRepo.ObterMovimentosAsync(conta.IdContaCorrente).Result;
            if(movimentos == null || !movimentos.Movimentacoes.Any())
                return new ConsultarSaldoResponse(conta.Numero, conta.Nome, movimentos.DataConsulta.ToString("dd/MM/yyyy : HH:mm"), "0,00");

            var creditos = movimentos.Movimentacoes.Where(v => v.TipoMovimento == 'C').Sum(v => v.Valor);
            var debitos = movimentos.Movimentacoes.Where(v => v.TipoMovimento == 'D').Sum(v => v.Valor);
            var saldo = creditos - debitos;

            return new ConsultarSaldoResponse(conta.Numero, conta.Nome, movimentos.DataConsulta.ToString("dd/MM/yyyy : HH:mm"), saldo.ToString("F2"));
        }

        private void ValidaRegrasNegocio(Domain.Entities.ContaCorrente conta)
        {
            if (conta == null)
            {
                _logger.LogWarning("TIPO: {TipoErro} | {Mensagem}", TipoErroDominio.INVALID_ACCOUNT, Mensagens.ContaNaoEncontrada);
                throw new RegraDeNegocioException(TipoErroDominio.INVALID_ACCOUNT, Mensagens.ContaNaoEncontrada);
            }

            if (!conta.Ativo)
            {
                _logger.LogWarning("TIPO: {TipoErro} | {Mensagem}", TipoErroDominio.INACTIVE_ACCOUNT, Mensagens.ContaInativa);
                throw new RegraDeNegocioException(TipoErroDominio.INACTIVE_ACCOUNT, Mensagens.ContaInativa);
            }
        }
    }
}
