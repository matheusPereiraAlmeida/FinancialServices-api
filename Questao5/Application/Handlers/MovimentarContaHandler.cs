using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Common;
using Questao5.Domain.Interfaces;
using Questao5.Domain.Language;
using static Questao5.Domain.Enumerators.TipoErro;

namespace Questao5.Application.Handlers
{
    public class MovimentarContaHandler : IRequestHandler<MovimentarContaCommand, ResultadoMovimentacao>
    {
        private readonly IContaQueryRequest _contaQueryRepo;
        private readonly IMovimentacaoCommandRequest _movimentacaoCommandRepo;        
        private readonly IMovimentacaoQueryRequest _movimentacaoQueryRepo;
        private readonly ILogger<ConsultarSaldoHandler> _logger;

        public MovimentarContaHandler(IContaQueryRequest contaQueryRepo, IMovimentacaoCommandRequest movimentacaoCommandRepo, IMovimentacaoQueryRequest movimentacaoQueryRepo, ILogger<ConsultarSaldoHandler> logger)
        {
            _contaQueryRepo = contaQueryRepo;
            _movimentacaoCommandRepo = movimentacaoCommandRepo;
            _movimentacaoQueryRepo = movimentacaoQueryRepo;
            _logger = logger;
        }

        public async Task<ResultadoMovimentacao> Handle(MovimentarContaCommand request, CancellationToken cancellationToken)
        {
            ValidaRegrasNegocio(request);
                
            var idMovimento = _movimentacaoCommandRepo.MovimentarConta(request);

            var resposta = new ResultadoMovimentacao(idMovimento);
            return resposta;
        }

        private void ValidaRegrasNegocio(MovimentarContaCommand request)
        {
            if (request.Valor < 0)
            {
                _logger.LogWarning("TIPO: {TipoErro} | {Mensagem}", TipoErroDominio.INVALID_VALUE, Mensagens.ContaNaoEncontrada);
                throw new RegraDeNegocioException(TipoErroDominio.INVALID_VALUE, Mensagens.ValorMovimentoInvalido);
            }

            if (TipoMovimentacaoInvalida(request))
            {
                _logger.LogWarning("TIPO: {TipoErro} | {Mensagem}", TipoErroDominio.INVALID_TYPE, Mensagens.TipoInvalido);
                throw new RegraDeNegocioException(TipoErroDominio.INVALID_TYPE, Mensagens.TipoInvalido);
            }

            var conta = _contaQueryRepo.PegaInformacoesContaPorId(request.IdContaCorrente);
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

            var idIdempotencia = _movimentacaoQueryRepo.PegaIdempotenciaPorId(request.IdRequisicao);
            if (idIdempotencia != null && request.IdRequisicao == idIdempotencia.Requisicao)
            {
                _logger.LogWarning("TIPO: {TipoErro} | {Mensagem}", TipoErroDominio.ALREADY_EXISTS, Mensagens.OperacaoJaExiste);
                throw new RegraDeNegocioException(TipoErroDominio.ALREADY_EXISTS, Mensagens.OperacaoJaExiste);
            }
        }

        private static bool TipoMovimentacaoInvalida(MovimentarContaCommand request)
            => !(request.TipoMovimento.ToUpper() == "C" || request.TipoMovimento.ToUpper() == "D");
    }
}
