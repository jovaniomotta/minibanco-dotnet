using System;
using System.Collections.Generic;
using MiniBanco.ConsoleApp.Models;
using MiniBanco.ConsoleApp.Exceptions;
using MiniBanco.ConsoleApp.Services;

namespace MiniBanco.ConsoleApp
{
    class Program
    {
        private static List<Conta> listaContas = new List<Conta>();

        static void Main(string[] args)
        {
            int opcao = -1;
            while (opcao != 0)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("");
                    Console.WriteLine("    MINIBANCO DIGITAL - MOTOR DE GESTÃO");
                    Console.WriteLine("");
                    Console.WriteLine("1 - Abrir Nova Conta");
                    Console.WriteLine("2 - Registrar Movimentação (Depósito/Saque)");
                    Console.WriteLine("3 - Aplicar Fechamento Mensal (Polimorfismo)");
                    Console.WriteLine("4 - Exibir Relatório das Contas & Saúde Financeira");
                    Console.WriteLine("0 - Sair");
                    Console.WriteLine("");
                    Console.Write("Escolha uma opção: ");
                    
                    opcao = int.Parse(Console.ReadLine());

                    switch (opcao)
                    {
                        case 1:
                            AbrirConta();
                            break;
                        case 2:
                            RegistrarMovimentacao();
                            break;
                        case 3:
                            FecharMes();
                            break;
                        case 4:
                            ExibirRelatorios();
                            break;
                        case 0:
                            Console.WriteLine("\nEncerrando o sistema. Até mais!");
                            break;
                        default:
                            Console.WriteLine("\nOpção inválida! Escolha um número do menu.");
                            AguardarTecla();
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n[Erro de Formato]: Entrada inválida. Por favor, digite números onde solicitado.");
                    Console.ResetColor();
                    AguardarTecla();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n[Erro Inesperado]: {ex.Message}");
                    Console.ResetColor();
                    AguardarTecla();
                }
            }
        }

        private static void AbrirConta()
        {
            Console.Clear();
            Console.WriteLine("--- ABRIR NOVA CONTA ---");
            Console.WriteLine("1 - Conta Corrente");
            Console.WriteLine("2 - Conta Poupança");
            Console.WriteLine("3 - Conta Salário");
            Console.Write("Selecione o tipo: ");
            int tipo = int.Parse(Console.ReadLine());

            Console.Write("Digite o nome do Titular: ");
            string titular = Console.ReadLine();
            Console.Write("Digite o Número da Conta: ");
            int numero = int.Parse(Console.ReadLine());
            Console.Write("Digite o Saldo Inicial (R$): ");
            decimal saldo = decimal.Parse(Console.ReadLine());

            switch (tipo)
            {
                case 1:
                    Console.Write("Digite o Limite do Cheque Especial (R$): ");
                    decimal limite = decimal.Parse(Console.ReadLine());
                    listaContas.Add(new ContaCorrente(titular, numero, saldo, limite));
                    Console.WriteLine("\nConta Corrente aberta com sucesso!");
                    break;
                case 2:
                    Console.Write("Digite a Taxa de Rendimento Mensal (%): ");
                    double taxa = double.Parse(Console.ReadLine());
                    Console.Write("Digite o Dia de Aniversário do contrato: ");
                    int dia = int.Parse(Console.ReadLine());
                    listaContas.Add(new ContaPoupanca(titular, numero, saldo, taxa, dia));
                    Console.WriteLine("\nConta Poupança aberta com sucesso!");
                    break;
                case 3:
                    Console.Write("Digite o nome da Empresa Empregadora: ");
                    string empregador = Console.ReadLine();
                    Console.Write("Digite o Limite de Saques Gratuitos no Mês: ");
                    int limiteSaques = int.Parse(Console.ReadLine());
                    listaContas.Add(new ContaSalario(titular, numero, saldo, empregador, limiteSaques));
                    Console.WriteLine("\nConta Salário aberta com sucesso!");
                    break;
                default:
                    Console.WriteLine("\nTipo de conta inexistente.");
                    break;
            }
            AguardarTecla();
        }

        private static void RegistrarMovimentacao()
        {
            Console.Clear();
            Console.WriteLine("--- REGISTRAR MOVIMENTAÇÃO ---");
            Console.Write("Digite o número da conta destino: ");
            int numero = int.Parse(Console.ReadLine());

            Conta conta = BuscarContaPorNumero(numero);

            if (conta == null)
            {
                Console.WriteLine("\nConta não encontrada no sistema.");
                AguardarTecla();
                return;
            }

            Console.WriteLine($"Conta localizada! Titular: {conta.Titular} | Saldo: R$ {conta.Saldo:F2}");
            Console.WriteLine("1 - Depósito");
            Console.WriteLine("2 - Saque");
            Console.Write("Selecione a operação: ");
            int op = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor da operação (R$): ");
            decimal valor = decimal.Parse(Console.ReadLine());

            try
            {
                if (op == 1)
                {
                    Console.Write("Digite uma descrição para o Depósito: ");
                    string desc = Console.ReadLine();
                    conta.Depositar(valor, desc);
                    Console.WriteLine("\nDepósito efetuado com sucesso!");
                }
                else if (op == 2)
                {
                    conta.Sacar(valor);
                    Console.WriteLine("\nSaque efetuado com sucesso!");
                }
                else
                {
                    Console.WriteLine("\nOperação inválida cancelada.");
                }
            }
            catch (SaldoInsuficienteException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n[Operação Negada]: {ex.Message}");
                Console.ResetColor();
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[Dados Inválidos]: {ex.Message}");
                Console.ResetColor();
            }

            AguardarTecla();
        }

        private static void FecharMes()
        {
            Console.Clear();
            Console.WriteLine("--- FECHAMENTO MENSAL AUTOMATIZADO ---");
            
            if (listaContas.Count == 0)
            {
                Console.WriteLine("Nenhuma conta registrada para aplicar o fechamento.");
                AguardarTecla();
                return;
            }

            foreach (Conta c in listaContas)
            {
                c.AplicarFechamentoMensal();
            }

            Console.WriteLine("\nFechamento Mensal aplicado com sucesso via Polimorfismo em todas as contas!");
            AguardarTecla();
        }

        private static void ExibirRelatorios()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("                RELATÓRIO DE SAÚDE FINANCEIRA INTEGRAL");
            Console.WriteLine("");

            if (listaContas.Count == 0)
            {
                Console.WriteLine("\nNenhuma conta aberta até o momento.");
                AguardarTecla();
                return;
            }

            // Instanciação local do motor de regras isolado
            MotorAnalise motor = new MotorAnalise();

            foreach (Conta c in listaContas)
            {
                // RF10 - Processamento via motor desacoplado
                ResultadoAnalise analise = motor.AnalisarConta(c);

                Console.WriteLine($"\nTitular: {c.Titular} | Número: {c.Numero} | Saldo Atual: R$ {c.Saldo:F2}");
                
                if (c is ContaCorrente cc)
                    Console.WriteLine($"Tipo: Conta Corrente | Cheque Especial Contratado: R$ {cc.LimiteChequeEspecial:F2}");
                else if (c is ContaPoupanca cp)
                    Console.WriteLine($"Tipo: Conta Poupança | Taxa de Rendimento: {cp.TaxaRendimentoMensal}% | Aniversário: Dia {cp.DiaAniversario}");
                else if (c is ContaSalario cs)
                    Console.WriteLine($"Tipo: Conta Salário | Empregador Vinculado: {cs.Empregador}");

                Console.WriteLine("");
                Console.WriteLine($"Pontuação Atingida: {analise.PontosObtidos} de {analise.PontuacaoMaxima} possíveis.");
            }
        }
    }
}

