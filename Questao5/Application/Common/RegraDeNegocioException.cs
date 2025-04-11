using static Questao5.Domain.Enumerators.TipoErro;

namespace Questao5.Application.Common
{
    public class RegraDeNegocioException : Exception
    {
        public TipoErroDominio TipoErro { get; }

        public RegraDeNegocioException(TipoErroDominio tipoErro, string mensagem) : base(mensagem)
        {
            TipoErro = tipoErro;
        }
    }
}
