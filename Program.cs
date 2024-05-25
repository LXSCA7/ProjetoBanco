﻿using System.Diagnostics.Tracing;
using ProjetoBanco.Models;
using ProjetoBanco.Context;
using ProjetoBanco.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Net.NetworkInformation;

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

    private static bool VerificaNascimento(DateTime data)
    {
        DateTime anoAtual = DateTime.Now;
        if (anoAtual.Year - data.Year < 18)
            return false;
    
        return true;
    }

    private static bool UsuarioExiste(string nomeDeUsuario)
    {
        using (var db = new UsuariosContext())
        {
            return db.Usuarios.Any(u => u.Username == nomeDeUsuario);
        }
    }

    private static bool UsernameAprovado(string nomeDeUsuario)
    {
        foreach (char c in nomeDeUsuario)
        {
            if (c == ' ')
                return false;
        }

        return true;
    }

    private static bool SenhaAprovada(string senha)
    {
        bool caracterEspecial = false;
        bool numeros = false;
        bool letraMin = false;
        bool letraMai = false;
        if (senha.Length < 8)
            return false;

        foreach (char c in senha)
        {
            if (c == ' ')
                return false;

            if (c == '!' || c == '@' || c == '#' || c == '$' || c == '%' || c == '&' || c == '.' || c == ',')
                caracterEspecial = true;
            if (char.IsNumber(c))
                numeros = true;
            if (char.IsUpper(c))
                letraMai = true;
            if (char.IsLower(c))
                letraMin = true;
        }

        if (!caracterEspecial || !numeros || !letraMai || !letraMin)
        {
            return false;
        }

        return true;
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
                    if (!VerificaNascimento(nascimento))
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
                    do
                    {
                        erroVerificacao = false;
                        Console.Write("Insira um nome de usuário: ");
                        username = Console.ReadLine();

                        if (UsuarioExiste(username))
                        {
                            Console.WriteLine("Opa! Esse nome de usuário já existe! Escolha outro.");
                            Thread.Sleep(300);
                            erroVerificacao = true;
                        }

                        if (!UsernameAprovado(username))
                        {
                            Console.WriteLine("O nome de usuário não pode conter espaços em branco. Tente novamente.");
                            Thread.Sleep(300);
                            erroVerificacao = true;
                        }

                    } while (erroVerificacao);
                    do {
                        erroVerificacao = false;
                        Console.Write("Insira uma senha: ");
                        senha = Console.ReadLine();

                        if (!SenhaAprovada(senha))
                        {
                            Console.WriteLine("Ops, sua senha não bate com os requisitos mínimos:");
                            Console.WriteLine(" - MIN 8 CARACTERES");
                            Console.WriteLine(" - MIN 1 CARACTER ESPECIAL");
                            Console.WriteLine(" - MIN 1 NUMERO");
                            Console.WriteLine(" - MIN 1 LETRA MAIÚSCULA");
                            Console.WriteLine(" - MIN 1 LETRA MINÚSCULA");
                            Thread.Sleep(2000);
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
            Console.Write("Isso está correto? [S/N]");
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