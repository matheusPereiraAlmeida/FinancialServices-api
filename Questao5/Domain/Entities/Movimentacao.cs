namespace Questao5.Domain.Entities
{
    public class Movimentacao
    {
        public string IdMovimento { get; }          
        public string IdContaCorrente { get; }         
        public DateTime DataMovimento { get; }           
        public char TipoMovimento { get; }            
        public double Valor { get; }

        public Movimentacao() { }

        public Movimentacao(string idMovimento, string idContaCorrente, DateTime dataMovimento, char tipoMovimentacao, double valor) {
            IdMovimento = idMovimento;
            IdContaCorrente = idContaCorrente;
            DataMovimento = dataMovimento;
            TipoMovimento = tipoMovimentacao;
            Valor = valor;
        }
    }
}