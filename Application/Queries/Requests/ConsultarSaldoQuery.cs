using MediatR;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;

namespace Questao5.Application.Queries.Requests
{
    public class ConsultarSaldoQuery : IRequest<ConsultarSaldoResponse>, IConsultarSaldoQuery
    {
        public string ContaId { get; private set; }
        public int NumeroConta { get; private set; }
        public TipoBuscaConta TipoBusca { get; private set; }

        public void PreencheNumeroConta(int numeroConta)
        {
            NumeroConta = numeroConta;
            TipoBusca = TipoBuscaConta.PorNumero;
        }

        public void PreencheContaId(string contaId)
        {
            ContaId = contaId;
            TipoBusca = TipoBuscaConta.PorId;
        }
    }
}
