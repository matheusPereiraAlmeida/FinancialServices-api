using static Questao5.Domain.Enumerators.TipoErro;

namespace Questao5.Application.Commands.Responses
{
    /// <summary>
    /// Resposta da movimentação de conta corrente.
    /// </summary>
    public class ResultadoMovimentacao
    {
        /// <example>b4f8c5f2-2a60-4d90-b6a2-3e0b8e7a8a91</example>
        public Guid IdMovimento { get; }

        public ResultadoMovimentacao(Guid idMovimento)
        {
            IdMovimento = idMovimento;
        }
    }
}
