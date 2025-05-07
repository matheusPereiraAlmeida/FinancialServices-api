using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentarContaCommand : IRequest<ResultadoMovimentacao>
    {
        /// <example>B6BAFC09-6967-ED11-A567-055DFA4A16C9</example>
        public string IdRequisicao { get; }
        /// <example>B6BAFC09-6967-ED11-A567-055DFA4A16C9</example>
        public string IdContaCorrente { get; }
        /// <example>123,45</example>
        public double Valor { get; }
        /// <example>C</example>
        public string TipoMovimento { get; }     

        public MovimentarContaCommand(string idRequisicao, string idContaCorrente, double valor, string tipoMovimento)
        {
            IdRequisicao = idRequisicao;
            IdContaCorrente = idContaCorrente;
            Valor = valor;
            TipoMovimento = tipoMovimento;
        }
    }
}
