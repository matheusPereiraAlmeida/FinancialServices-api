namespace Questao5.Domain.Language
{
    public class Mensagens
    {
        public const string ContaInativa = "Apenas contas correntes ativas podem consultar o saldo.";
        public const string ContaNaoEncontrada = "Conta corrente não encontrada.";
        public const string ValorMovimentoInvalido = "Apenas valores positivos podem ser recebidos.";
        public const string TipoInvalido = "Apenas os tipos “débito” ou “crédito” podem ser aceitos";
        public const string MovimentacaoNaoPersistida = "Não foi possível salvar a informação na tabela de Movimentacao.";
        public const string IdempotenciaNaoPersistida = "Não foi possível salvar a informação na tabela de Idempotencia";
        public const string OperacaoJaExiste = "Essa operação já foi efetuada";
    }
}
