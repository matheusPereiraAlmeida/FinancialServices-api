using Dapper;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Common;
using Questao5.Domain.Interfaces;
using Questao5.Domain.Language;
using System.Data;
using static Questao5.Domain.Enumerators.TipoErro;

namespace Questao5.Infrastructure.Database.CommandStore.Requests
{
    public class MovimentarContaCommandRequest : IMovimentacaoCommandRequest
    {
        private readonly IDbExecutor _connection;
        private readonly ILogger<MovimentarContaCommandRequest> _logger;

        public MovimentarContaCommandRequest(IDbExecutor connection, ILogger<MovimentarContaCommandRequest> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public Guid MovimentarConta(MovimentarContaCommand request)
        {
            var idMovimento = Guid.NewGuid();
            var dataMovimento = DateTime.UtcNow;

            PreencheIdempotencia(request.IdRequisicao);

            const string sql = @"INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) VALUES(@idMovimento, @idcontacorrente, @datamovimento, @tipomovimento, @valor)";
            var parameters = new
            {
                idMovimento = idMovimento,
                idcontacorrente = request.IdContaCorrente,
                datamovimento = dataMovimento.ToString("s"),
                tipomovimento = request.TipoMovimento.ToUpper(),
                valor = request.Valor
            };

            var rowsAffected = _connection.Execute(sql, parameters);
            if (rowsAffected < 1)
            {
                _logger.LogWarning("TIPO: {TipoErro} | {Mensagem}", TipoErroDominio.NO_ROW_AFFECTED, Mensagens.MovimentacaoNaoPersistida);
                throw new RegraDeNegocioException(TipoErroDominio.NO_ROW_AFFECTED, Mensagens.MovimentacaoNaoPersistida);
            }
            return idMovimento;
        }

        private void PreencheIdempotencia(string idRequisicao)
        {
            var idIdempotencia = Guid.NewGuid();

            const string sql = @"INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) VALUES(@chave_idempotencia, @requisicao, @resultado)";
            var parameters = new
            {
                chave_idempotencia = idIdempotencia,
                requisicao = idRequisicao,
                resultado = "OK"
            };

            var rowsAffected = _connection.Execute(sql, parameters);
            if (rowsAffected < 1)
            {
                _logger.LogWarning("TIPO: {TipoErro} | {Mensagem}", TipoErroDominio.NO_ROW_AFFECTED, Mensagens.IdempotenciaNaoPersistida);
                throw new RegraDeNegocioException(TipoErroDominio.NO_ROW_AFFECTED, Mensagens.IdempotenciaNaoPersistida);
            }
        }
    }
}