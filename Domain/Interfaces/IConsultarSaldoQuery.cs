using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Interfaces
{
    public interface IConsultarSaldoQuery
    {
        public string ContaId { get; }
        public int NumeroConta { get; }
        public TipoBuscaConta TipoBusca { get; }
        public void PreencheNumeroConta(int numeroConta);
        public void PreencheContaId(string contaId);
    }
}
