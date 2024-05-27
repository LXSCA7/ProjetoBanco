using System.Diagnostics.Tracing;
using ProjetoBanco.Models;
using ProjetoBanco.Context;
using ProjetoBanco.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Net.NetworkInformation;
using System.Security;
using System.ComponentModel;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Clear();
        bool repete = true;
        do {
            Menu();
            int esc = Int32.Parse(Console.ReadLine());
            switch (esc)
            {
                case 1:
                    PrimeiroCaso();
                    break;
                    
                case 2:          
                    Console.Clear();
                    break;

                case 3:
                    Console.Clear();
                    break;

                case 4:
                    Console.Clear();
                    DepositoOutroBanco();
                    break;

                case 5:
                    ListaUsuarios();
                    break;
                case 6:
                    Escreve("Encerrando o programa");
                    repete = false;
                    break;
                default:
                    Console.WriteLine("| Opção inválida. Por favor, insira novamente!");
                    Thread.Sleep(1000);
                    Console.Clear();
                    break;
            }
        } while (repete == true);
    }

    private static void Menu()
    {
        Console.WriteLine("+--------------------------------------------+");
        Console.WriteLine("|             BANCO  ★  ESTRELA              |");
        Console.WriteLine("+--------------------------------------------+");
        Console.WriteLine("|                                            |");
        Console.WriteLine("| [1] Criar uma conta                        |");
        Console.WriteLine("| [2] Fazer Login                            |");
        Console.WriteLine("| [3] Encerrar sua conta                     |");
        Console.WriteLine("| [4] Fazer depósito de outro banco          |");
        Console.WriteLine("| [5] Listar usuários cadastrados            |");
        Console.WriteLine("|                                            |");
        Console.WriteLine("| [6] Sair                                   |");
        Console.WriteLine("|                                            |");
        Console.WriteLine("+--------------------------------------------+");
        Console.Write("| SUA ESCOLHA: ");
    }

    private static void Escreve(string frase)
    {
        int repetitions = 6;
        int interval = 230; // 230ms

        for (int i = 0; i < repetitions; i++)
        {
            switch (i % 3)
            {
                case 0:
                    Console.Write($"{frase}.  \r");
                    break;
                case 1:
                    Console.Write($"{frase}.. \r");
                    break;
                case 2:
                    Console.Write($"{frase}... \r");
                    break;
            }
            Thread.Sleep(interval);
        }
        Console.WriteLine($"{frase}...");
    }

    private static void Cabecalho()
    {
        Console.WriteLine("+---------------------+");
        Console.WriteLine("|  BANCO  ★  ESTRELA  |");
        Console.WriteLine("+---------------------+");
    }

    // casos:
    private static void PrimeiroCaso()
    {
        Console.Clear();
                    Cabecalho();
                    Console.Write("Insira seu nome: ");
                    string nome = Console.ReadLine();
                    Console.Write("Insira seu Sobrenome: ");
                    string sobrenome = Console.ReadLine();
                    Console.Write("Insira sua Data de nascimento [dd/MM/yyyy]: ");
                    DateTime nascimento = DateTime.Parse(Console.ReadLine());
                    nascimento = nascimento.Date;
                    if (!Verificacao.VerificaNascimento(nascimento))
                    {
                        Console.WriteLine("O Banco Estrela só aceita usuários maiores de idade.");
                        return;
                    }
                    Console.Clear();
                    Cabecalho();
                    Console.WriteLine($"Olá, {nome} {sobrenome}, vamos continuar com o seu cadastro!");
                    bool erroVerificacao;
                    string username;
                    string senha;
                    string senha2;
                    do
                    {
                        erroVerificacao = false;
                        Console.Write("Insira um nome de usuário: ");
                        username = Console.ReadLine();
                        Escreve("Verificando nome de usuário");

                        if (Verificacao.UsuarioExiste(username))
                        {
                            Console.WriteLine("Opa! Esse nome de usuário já existe! Escolha outro.");
                            Thread.Sleep(300);
                            erroVerificacao = true;
                        }

                        if (!Verificacao.UsernameAprovado(username))
                        {
                            Console.WriteLine("O nome de usuário não pode conter espaços em branco. Tente novamente.");
                            Thread.Sleep(300);
                            erroVerificacao = true;
                        }

                    } while (erroVerificacao);
                    Console.WriteLine("Certo! Seu nome de usuário é " + username);
                    do {
                        erroVerificacao = false;
                        Console.Write("Insira uma senha: ");
                        SecureString testeSenha = PegaSenhaEscondido();

                        senha = new System.Net.NetworkCredential(string.Empty, testeSenha).Password;

                        if (!Verificacao.SenhaAprovada(senha))
                        {
                            Console.WriteLine("\nOps, sua senha não bate com os requisitos mínimos:");
                            Console.WriteLine(" - MIN 8 CARACTERES");
                            Console.WriteLine(" - MIN 1 CARACTER ESPECIAL");
                            Console.WriteLine(" - MIN 1 NUMERO");
                            Console.WriteLine(" - MIN 1 LETRA MAIÚSCULA");
                            Console.WriteLine(" - MIN 1 LETRA MINÚSCULA");
                            Thread.Sleep(2000);
                            erroVerificacao = true;
                        }
                    } while (erroVerificacao);
                    do
                    {
                        erroVerificacao = false;
                        Console.Write("\nConfirme sua senha: ");
                        SecureString senhaEsc = PegaSenhaEscondido();
                        senha2 = new System.Net.NetworkCredential(string.Empty, senhaEsc).Password;
                        if (senha2 != senha)
                        {
                            Console.WriteLine("\nSuas senhas não correspondem. Tente novamente.");
                            erroVerificacao = true;
                        }
                    } while (erroVerificacao);

                    Console.Clear();
                    Usuario novoUsuario = new Usuario
                    {
                        Nome = nome,
                        Sobrenome = sobrenome,
                        DataDeNascimento = nascimento,
                        Username = username,
                        Senha = senha,
                        Saldo = 0.00M
                    };

                    UsuariosContext context = new UsuariosContext();
                    UsuarioController usuarioController = new UsuarioController(context);
                    usuarioController.CriarConta(novoUsuario);
                    Escreve("Criando sua conta");
                    Console.Clear();
                    Cabecalho();
                    Console.WriteLine($"{nome} {sobrenome} sua conta foi criada com sucesso!");
                    Console.WriteLine($"O Banco Estrela te da as boas-vindas!");
                    Thread.Sleep(3000);
                    Console.Clear();
    }

    private static SecureString PegaSenhaEscondido()
    {
        SecureString senha = new SecureString();
        ConsoleKeyInfo tecla;
        do
        {
            tecla = Console.ReadKey(true);
            if (tecla.Key != ConsoleKey.Backspace)
            {
                senha.AppendChar(tecla.KeyChar);
                Console.Write("*");
            }
            else
            {
                if (senha.Length > 0)
                {
                    senha.RemoveAt(senha.Length - 1);
                    Console.Write("\b \b");
                }
            }
        } while (tecla.Key != ConsoleKey.Enter);
        return senha;
    }

    // caso 2
    private static void ListaUsuarios()
    {
        Console.Clear();
        Cabecalho();
        using (var db = new UsuariosContext())
        {
            var contas = db.Usuarios.ToList();
            int i = 1;
            foreach (var cadastro in contas)
            {
                Console.WriteLine($"{i} - Número da Conta: {cadastro.Id} | Nome: {cadastro.Nome} {cadastro.Sobrenome}");
                i++;
            }
        }
    }

    // caso 4
    private static void DepositoOutroBanco()
    {
        Cabecalho();
        Console.Write("Digite o número da conta: ");
        int numeroConta = Int32.Parse(Console.ReadLine());
        Escreve("Buscando conta");
        using (var db = new UsuariosContext())
        {
            var receber = db.Usuarios.Find(numeroConta);
            if (receber == null)
            {
                Console.WriteLine("Conta não encontrada...");
                return;
            }
            Console.WriteLine($"Número da Conta: {receber.Id} | Nome: {receber.Nome} {receber.Sobrenome}");
            Console.Write("Isso está correto? [S/N]: ");
            string correto = Console.ReadLine();
            correto = correto.ToUpper();
            char resposta = correto[0];
            if (resposta == 'N')
                return;
            
            Console.Write($"Insira o valor que será depositado para {receber.Nome} {receber.Sobrenome}: ");
            decimal valorDepositado = Decimal.Parse(Console.ReadLine());
            // depositando...

            UsuariosContext context = new UsuariosContext();
            UsuarioController usuarioController = new UsuarioController(context);
            usuarioController.AddSaldo(receber, receber.Id, valorDepositado);

            Console.WriteLine("Depósito realizado.");
            Thread.Sleep(500);
            Console.Clear();
        }
    }
}