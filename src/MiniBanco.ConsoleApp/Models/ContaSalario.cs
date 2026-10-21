using MiniBanco.ConsoleApp.Exceptions;

namespace MiniBanco.ConsoleApp.Models
{
    public class ContaSalario : Conta
    {
        public string Empregador { get; private set; }
        public int LimiteSaquesMensais { get; private set; }
        private int _quantidadeSaquesNoMes;

        public ContaSalario(string titular, int numero, decimal saldoInicial, string empregador, int limiteSaquesMensais)
            : base(titular, numero, saldoInicial)
        {
            this.Empregador = empregador;
            this.LimiteSaquesMensais = limiteSaquesMensais;
            this._quantidadeSaquesNoMes = 0;
        }

        public override void Sacar(decimal valor)
        {
            if (valor <= 0)
                throw new System.ArgumentException("O valor do saque deve ser maior que zero.");

            _quantidadeSaquesNoMes++;
            decimal custoAdicional = 0m;

            if (_quantidadeSaquesNoMes > LimiteSaquesMensais)
                custoAdicional = 5.00m;

            if (Saldo < (valor + custoAdicional))
            {
                _quantidadeSaquesNoMes--; // Reverte contagem se falhar
                throw new SaldoInsuficienteException($"Saldo insuficiente para cobrir o saque e a tarifa de R$ {custoAdicional:F2}. Requerido: R$ {valor + custoAdicional:F2}");
            }

            Saldo -= valor;
            RegistrarTransacao("Saque em Conta Salário", "Saque", valor);

            if (custoAdicional > 0m)
            {
                Saldo -= custoAdicional;
                RegistrarTransacao("Tarifa sobre Saque Excedente", "Tarifa", custoAdicional);
            }
        }

        public override void AplicarFechamentoMensal()
        {
            _quantidadeSaquesNoMes = 0;
        }
    }
}