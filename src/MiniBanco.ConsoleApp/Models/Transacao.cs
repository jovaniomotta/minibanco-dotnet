using System;

    public class Transacao
{
    public string Descricao { get; private set; }
    public string Tipo { get; private set;}
    public decimal Valor { get; private set;}

    public Transacao(string descricao, string tipo, decimal valor)
    {
        if (valor <=0)
           throw new ArgumentException("O valor da transação deve ser maior que zero.");

            if (tipo != "Deposito" && tipo != "Saque" && tipo != "Tarifa")
                throw new ArgumentException("Tipo de transação inválido. Permita apenas: Deposito, Saque ou Tarifa.");

            this.Descricao = descricao;
            this.Tipo = tipo;
            this.Valor = valor;
        }
    }

