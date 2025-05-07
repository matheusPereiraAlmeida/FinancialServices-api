using Questao5.Application.Common;
using static Questao5.Domain.Enumerators.TipoErro;
using Xunit;
using Questao5.Domain.Language;

namespace Questao5.Testes.Application.Common
{
    public class RegraDeNegocioExceptionTests
    {
        [Fact]
        public void Construtor_DeveInicializarPropriedadesCorretamente()
        {
            var tipoErroEsperado = TipoErroDominio.INVALID_ACCOUNT;
            var mensagemEsperada = Mensagens.ContaNaoEncontrada;

            var ex = new RegraDeNegocioException(tipoErroEsperado, mensagemEsperada);

            Assert.Equal(tipoErroEsperado, ex.TipoErro);
            Assert.Equal(mensagemEsperada, ex.Message);
        }

        [Fact]
        public void DeveLancarRegraDeNegocioException()
        {
            static void Acao() => throw new RegraDeNegocioException(TipoErroDominio.INVALID_ACCOUNT, Mensagens.ContaNaoEncontrada);

            var ex = Assert.Throws<RegraDeNegocioException>(Acao);
            Assert.Equal(TipoErroDominio.INVALID_ACCOUNT, ex.TipoErro);
            Assert.Equal(Mensagens.ContaNaoEncontrada, ex.Message);
        }
    }
}
