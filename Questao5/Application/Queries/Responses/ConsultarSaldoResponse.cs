namespace Questao5.Application.Queries.Responses
{
    /// <summary>
    /// Resposta da consulta de saldo.
    /// </summary>
    public class ConsultarSaldoResponse
    {
        /// <example>123</example>
        public int Numero { get; }
        /// <example>Katherine Sanchez</example>
        public string Nome { get; }
        /// <example>DD/MM/AA : hh:mm</example>
        public string DataConsulta { get; }
        /// <example>123,45</example>
        public string Saldo { get; }
        public ConsultarSaldoResponse(int numero, string nome, string dataConsulta, string saldo)
        {
            Numero = numero;
            Nome = nome;
            DataConsulta = dataConsulta;
            Saldo = saldo;
        }
    }
}
