
# MiniBanco.ConsoleApp

O **MiniBanco.ConsoleApp** é um motor de processamento e gerenciamento de contas bancárias desenvolvido como uma Console Application em C# (.NET). O sistema gerencia diferentes modalidades de contas, registra movimentações financeiras históricas, aplica regras fiscais e de fechamento mensal via polimorfismo e avalia de forma isolada a saúde financeira de cada cliente.

Este projeto consolida conceitos fundamentais de **Programação Orientada a Objetos (POO)** e boas práticas de arquitetura através do desacoplamento de código (Separation of Concerns).

---

## Tecnologias e Conceitos Aplicados

*   **Linguagem:** C# 
*   **Plataforma:** .NET Core / .NET SDK
*   **Abstração e Herança:** Classe abstrata `Conta` servindo de molde para `ContaCorrente`, `ContaPoupanca` e `ContaSalario`.
*   **Encapsulamento:** Uso restrito de modificadores de acesso (`protected set` e `private set`) para blindagem do saldo e histórico.
*   **Polimorfismo:** Métodos `virtual` e `override` processando o fechamento mensal e saques de forma dinâmica em tempo de execução.
*   **Tratamento de Exceções:** Captura de erros de formato, argumentos inválidos e lançamento de exceção customizada (`SaldoInsuficienteException`).
*   **Clean Code:** Arquitetura dividida em Camadas de Modelos (`Models`), Serviços de Negócio (`Services`) e Exceções (`Exceptions`).

---

## Estrutura do Projeto

```text
MiniBanco.ConsoleApp/
│
├── Exceptions/
│   └── SaldoInsuficienteException.cs
│
├── Models/
│   ├── Transacao.cs                  
│   ├── Conta.cs                      
│   ├── ContaCorrente.cs              
│   ├── ContaPoupanca.cs             
│   └── ContaSalario.cs              
│
├── Services/
│   └── MotorAnalise.cs               
│
└── Program.cs                        
```

---

## Regras do Motor de Análise (`MotorAnalise`)

A saúde financeira do cliente é calculada baseada no tipo de movimentações efetuadas (Depósito = 10 pontos, Saque = 5 pontos, Tarifa = 0 pontos). 

| Percentual Calculado | Classificação Final | Ação Corporativa Recomendada |
| :--- | :--- | :--- |
| **90% a 100%** | Conta Saudável | Oferecer investimentos e avaliar aumento de limite. |
| **60% a 89%** | Conta em Atenção | Contato da gerência para orientação e revisão de tarifas. |
| **0% a 59%** | Conta em Risco | Bloquear aumento de limite e abrir análise de crédito. |

---

##  Como Executar o Projeto

Certifique-se de ter o SDK do .NET instalado em sua máquina.

1. **Clone o repositório:**
    ```link
   https://github.com/jovaniomotta/minibanco-dotnet.git
   ```
   Use o PowerShell do Windows, ou terminal do Visual Studio Code para salvar em seu dispositivo;

   
2. **No Visual Studio Code:**
   
    No terminal, entre no diretório da pasta em que salvou arquivo;

     

2. **Restaure as dependências do ambiente:**
    ```bash
   dotnet restore
   ```

3. **Execute a aplicação:**
   ```bash
   dotnet run
   ```

---

## Desenvolvedores e Contribuição

Criado por Jovanio Motta, com auxílio de IA .
Projeto desenvolvido como parte do curso BackEnd-Net. Sinta-se à vontade para clonar, abrir issues e enviar pull requests com melhorias!
