using System;
using System.Collections.Generic;


    public abstract class Conta
    {
        public string Titular { get; protected set; }
        public int Numero { get; protected set; }
        public decimal Saldo { get; protected set; }
        public List<Transacao> Extrato { get; private set; }

        public Conta(string titular, int numero, decimal saldoInicial)
        {
            this.Titular = titular;
            this.Numero = numero;
            this.Saldo = saldoInicial;
            this.Extrato = new List<Transacao>();
        }

        public void RegistrarTransacao(string descricao, string tipo, decimal valor)
        {
            Transacao novaTransacao = new Transacao(descricao, tipo, valor);
            Extrato.Add(novaTransacao);
        }

        public void Depositar(decimal valor, string descricao)
        {
            if (valor <= 0)
                throw new ArgumentException("O valor do depósito deve ser maior que zero.");

            Saldo += valor;
            RegistrarTransacao(descricao, "Deposito", valor);
        }

        public virtual void Sacar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("O valor do saque deve ser maior que zero.");

            if (Saldo < valor)
                throw new MiniBanco.ConsoleApp.Exceptions.SaldoInsuficienteException($"Saldo insuficiente. Saldo atual: R$ {Saldo:F2}");

            Saldo -= valor;
            RegistrarTransacao("Saque realizado", "Saque", valor);
        }

        public abstract void AplicarFechamentoMensal();
    }

    