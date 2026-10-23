using System;
using MiniBanco.ConsoleApp.Exceptions;

namespace MiniBanco.ConsoleApp.Models
{
public class ContaCorrente : Conta
{
        internal readonly object LimiteChequeEspecial;

        public ContaCorrente(string titular, int numero, decimal saldoInicial)
		: base(titular, numero, saldoInicial)
    {
        this.LimiteChequeEspecial = limiteChequeEspecial;
    }

    public override void Sacar(decimal valor)
    {
        if (valor <=0)
        throw new System.ArgumentException("o valor do saque deve ser maior que zero.");

        if (Saldo + LimiteChequeEspecial < valor)
                throw new SaldoInsuficienteException($"Limite exaurido (Saldo + Cheque Especial). Disponível: R$ {Saldo + LimiteChequeEspecial:F2}");

            Saldo -= valor;
            RegistrarTransacao("Saque em Conta Corrente", "Saque", valor);
        }

        public override void AplicarFechamentoMensal()
        {
            decimal tarifaManutencao = 10.00m;
            Saldo -= tarifaManutencao;
            RegistrarTransacao("Tarifa de Manutenção Mensal", "Tarifa", tarifaManutencao);
        }
    }
}

