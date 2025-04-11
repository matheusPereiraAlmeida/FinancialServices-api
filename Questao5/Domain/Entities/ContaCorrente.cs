namespace Questao5.Domain.Entities
{
    public class ContaCorrente
    {
        public string IdContaCorrente { get; }  
        public int Numero { get; }              
        public string Nome { get; }             
        public bool Ativo { get; }              

        public ContaCorrente(string idContaCorrente, int numero, string nome, bool ativo) 
        { 
            IdContaCorrente = idContaCorrente;
            Numero = numero;
            Nome = nome;
            Ativo = ativo;
        }  
    }
}
