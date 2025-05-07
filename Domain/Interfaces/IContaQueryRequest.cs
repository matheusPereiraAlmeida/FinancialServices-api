using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces
{
    public interface IContaQueryRequest
    {
        ContaCorrente PegaInformacoesContaPorId(string contaId);
        ContaCorrente PegaInformacoesContaPorNumeroConta(int numeroConta);
    }
}
