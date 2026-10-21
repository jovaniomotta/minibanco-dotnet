using System;
using System.Collections.Generic;
using MiniBanco.ConsoleApp.Models;

namespace MiniBanco.ConsoleApp.Services
{
    public class ResultadoAnalise
    {
        public int PontosObtidos { get; set; }
        public int PontuacaoMaxima { get; set; }
        public double PercentualSaude { get; set; }
        public string ClassificacaoFinal { get; set; }
        public string AcaoCorporativa { get; set; }
        public List<Transacao> ItensCriticosTarifas { get; set; }
        public List<Transacao> ItensAtencaoSaques { get; set; }

        public ResultadoAnalise()
        {
            ItensCriticosTarifas = new List<Transacao>();
            ItensAtencaoSaques = new List<Transacao>();
        }
    }

    public class MotorAnalise
    {
        public ResultadoAnalise AnalisarConta(Conta conta)
        {
            ResultadoAnalise resultado = new ResultadoAnalise();
            int totalTransacoes = conta.Extrato.Count;

            foreach (Transacao t in conta.Extrato)
            {
                if (t.Tipo == "Deposito")
                {
                    resultado.PontosObtidos += 10;
                }
                else if (t.Tipo == "Saque")
                {
                    resultado.PontosObtidos += 5;
                    resultado.ItensAtencaoSaques.Add(t);
                }
                else if (t.Tipo == "Tarifa")
                {
                    resultado.PontosObtidos += 0;
                    resultado.ItensCriticosTarifas.Add(t);
                }
            }

            resultado.PontuacaoMaxima = totalTransacoes * 10;
            
            if (totalTransacoes == 0)
            {
                resultado.PercentualSaude = 100.0;
            }
            else
            {
                resultado.PercentualSaude = ((double)resultado.PontosObtidos / (double)resultado.PontuacaoMaxima) * 100.0;
            }

            double saude = resultado.PercentualSaude;

            if (saude >= 90.0 && saude <= 100.0)
            {
                resultado.ClassificacaoFinal = "Conta Saudável";
                resultado.AcaoCorporativa = "Oferecer investimentos e avaliar aumento de limite.";
            }
            else if (saude >= 60.0 && saude < 90.0)
            {
                resultado.ClassificacaoFinal = "Conta em Atenção";
                resultado.AcaoCorporativa = "Contato da gerência para orientação financeira e revisão de tarifas.";
            }
            else
            {
                resultado.ClassificacaoFinal = "Conta em Risco";
                resultado.AcaoCorporativa = "Bloquear aumento de limite e abrir análise de crédito.";
            }

            return resultado;
        }
    }
}