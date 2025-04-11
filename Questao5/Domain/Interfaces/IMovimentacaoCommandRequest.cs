using Questao5.Application.Commands.Requests;

namespace Questao5.Domain.Interfaces
{
    public interface IMovimentacaoCommandRequest
    {
        Guid MovimentarConta(MovimentarContaCommand request);
    }
}
