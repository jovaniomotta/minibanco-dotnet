namespace MiniBanco.ConsoleApp.Models
{
    public class ContaPoupanca : Conta
    {
        public double TaxaRendimentoMensal { get; private set; }
        public int DiaAniversario { get; private set; }

        public ContaPoupanca(string titular, int numero, decimal saldoInicial, double taxaRendimentoMensal, int diaAniversario)
            : base(titular, numero, saldoInicial)
        {
            this.TaxaRendimentoMensal = taxaRendimentoMensal;
            this.DiaAniversario = diaAniversario;
        }

        public override void AplicarFechamentoMensal()
        {
            decimal rendimento = Saldo * (decimal)(TaxaRendimentoMensal / 100.0);
            Saldo += rendimento;
            RegistrarTransacao($"Rendimento Poupança ({TaxaRendimentoMensal}%)", "Deposito", rendimento);
        }
    }
}