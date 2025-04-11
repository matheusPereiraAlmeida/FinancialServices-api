using System.Globalization;

namespace Questao1
{
    public class ContaBancaria
    {
        private int Numero { get; } 
        private string Titular { get; set; } 
        private double Saldo { get; set; }

        private const double TaxaSaque = 3.50;

        public ContaBancaria(int numero, string titular, double depositoInicial)
        {
            Numero = numero;
            Titular = titular;
            Saldo = depositoInicial;
        }

        public ContaBancaria(int numero, string titular)
        {
            Numero = numero;
            Titular = titular;
            Saldo = 0;
        }

        public void Deposito(double quantia)
            => Saldo += quantia;

        public void Saque(double quantia)
            => Saldo -= (quantia + TaxaSaque);

        public void AlteraNomeTitular(string novoNome)
            => Titular = novoNome;

        public override string ToString()
            => $"Conta {Numero}, Titular: {Titular}, Saldo: $ {Saldo.ToString("F2", CultureInfo.InvariantCulture)}";

    }
}
