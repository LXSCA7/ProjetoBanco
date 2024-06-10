using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using ProjetoBanco.Context;
using ProjetoBanco.Controllers;
using ProjetoBanco.Migrations;

namespace ProjetoBanco.Models
{
    public class Logado
    {
        public static void MudarSenha(Usuario user)
        {
            UsuariosContext context = new();
            UsuarioController usuarioController = new(context);
            Console.Write("Insira sua senha atual: ");
            string senhaAtual = Console.ReadLine();
            if (senhaAtual != user.Senha)
            {
                Console.WriteLine("Senha incorreta.");
                EsperaTecla(ConsoleKey.Enter);
                return;
            }
            string senha;
            bool aprovado;
            do 
            {
                Console.WriteLine("Deixe em branco para cancelar.");
                Console.Write("Insira sua nova senha: ");
                senha = Console.ReadLine();
                aprovado = Verificacao.SenhaAprovada(senha);
                if (string.IsNullOrWhiteSpace(senha))
                {
                    Console.WriteLine("Operação cancelada.");
                    EsperaTecla(ConsoleKey.Enter);
                    return;
                }
                if (!aprovado)
                    Console.WriteLine("Senha não aprovada. Digite novamente. ");
                if (senha == user.Senha)
                {
                    Console.WriteLine("Sua nova senha não pode ser igual a anterior. Tente novamente.");
                    aprovado = false;
                }
            } while (!aprovado);
            usuarioController.MudarSenha(user, senha);

            Console.WriteLine("Sua senha foi alterada com sucesso!");
            EsperaTecla(ConsoleKey.Enter);
        }

        public static void Transferir(Usuario user)
        {
            char resposta;
            Usuario receber = new Usuario();
            do
            {
                Console.Write("Insira o número da conta: ");
                int conta = Int32.Parse(Console.ReadLine());
                using (var db = new UsuariosContext())
                {
                    receber = db.Usuarios.Find(conta);
                    if (receber == null)
                    {
                        Console.WriteLine("Conta não encontrada...");
                        EsperaTecla(ConsoleKey.Enter);
                        return;
                    }
                    if (receber.Id == user.Id)
                    {
                        Console.WriteLine("Não é possível realizar uma transferência para você mesmo.");
                        EsperaTecla(ConsoleKey.Enter);
                        return;
                    }
                    Console.WriteLine($"Transferência: para {receber.Nome} {receber.Sobrenome}");
                    Console.Write("Isso está correto? [S/N]: ");
                    resposta = char.Parse(Console.ReadLine());
                    resposta = char.ToUpper(resposta);
                }
            } while (resposta == 'N');
            Console.WriteLine($"Seu saldo: {user.Saldo}");
            Console.Write("Insira o valor da transferência: R$ ");
            decimal valorTransferencia = decimal.Parse(Console.ReadLine());
            if (valorTransferencia > user.Saldo || valorTransferencia <= 0)
            {
                Console.WriteLine("Impossível realizar a transferência.");
                EsperaTecla(ConsoleKey.Enter);
                return;
            }

            Console.Write($"Você realmente deseja realizar uma transferência no valor de R$ {valorTransferencia} para" +
                $" {receber.Nome} {receber.Sobrenome}? [S/N]: ");
            string resposta2 = Console.ReadLine();
            resposta2 = resposta2.ToUpper();
            char resposta2C = resposta2[0];

            if (resposta2C == 'N')
            {
                Console.WriteLine("Transferência cancelada.");
                return;
            }

            UsuariosContext context = new UsuariosContext();
            var usuarioController = new UsuarioController(context);
            usuarioController.TransferirEntreContas(user, receber, valorTransferencia);
            Console.WriteLine("Transferência realizada com sucesso.");
            EsperaTecla(ConsoleKey.Enter);
        }
    
        public static void ApagarConta(Usuario user)
        {
            if (user.Saldo > 0)
            {
                Console.WriteLine("Não é possível apagar sua conta com saldo. Remova o saldo da sua conta e tente novamente.");
                EsperaTecla(ConsoleKey.Enter);
                return;
            }
            Console.WriteLine("Poxa! Que pena que deseja excluir sua conta...");
            Console.Write("Insira seu nome de usuário: ");
            string username = Console.ReadLine();
            if (username.ToUpper() != user.Username.ToUpper())
            {
                Console.WriteLine("Nome de usuário incorreto.");
                EsperaTecla(ConsoleKey.Enter);
                return;
            }
            Console.WriteLine("Deixe em branco para cancelar.");
            Console.Write("Insira sua senha: ");
            SecureString senha = Program.PegaSenhaEscondido();
            string senhaString = new System.Net.NetworkCredential(string.Empty, senha).Password;
            
            if (string.IsNullOrWhiteSpace(senhaString))
            {
                Console.WriteLine("\n");
                Program.Escreve("Cancelando operação");
                EsperaTecla(ConsoleKey.Enter);
                return;
            }

            if (senhaString != user.Senha)
            {
                Console.WriteLine("\nSenha incorreta.");
                EsperaTecla(ConsoleKey.Enter);
                return;
            }
            UsuariosContext context = new();
            UsuarioController usuarioController = new(context);

            usuarioController.RemoverConta(user);
            Console.WriteLine("\nSua conta foi deletada.");
            EsperaTecla(ConsoleKey.Enter);
        }
        public static void Sacar(Usuario user)
        { 
            bool continuarSaque = false;
            decimal valorSaque;
            do
            {
                Console.WriteLine("Seu saldo: " + user.Saldo);
                Console.WriteLine("Digite 0 para cancelar o saque.\n");
                Console.Write("Insira o valor: R$ ");
                valorSaque = decimal.Parse(Console.ReadLine());
                if (valorSaque <= 0)
                {
                    Program.Escreve("Cancelando saque");
                    EsperaTecla(ConsoleKey.Enter);
                    return;
                }
                if (valorSaque > user.Saldo)
                {
                    Console.WriteLine("Impossível realizar saque com esse valor. Tente novamente.");
                    continuarSaque = true;
                }
            } while (continuarSaque);


            UsuariosContext context = new();
            UsuarioController usuarioController = new(context);
            usuarioController.Sacar(user, valorSaque);
            Console.WriteLine($"Saque realizado. Novo saldo: R$ {user.Saldo}");
            EsperaTecla(ConsoleKey.Enter);
        }
        public static void MostrarInformacoes(Usuario user)
        {
            using (var db = new UsuariosContext())
            {
                UsuariosContext context = new();
                UsuarioController usuarioController = new(context);
                Console.WriteLine($"Nome completo: {user.Nome} {user.Sobrenome}");
                Console.WriteLine($"Data de nascimento: {user.DataDeNascimento.ToShortDateString()}");
                Console.WriteLine($"Número da conta: {user.Id}");
                Console.WriteLine($"Saldo: R$ {user.Saldo}");
                Console.WriteLine($"Nome de usuário: {user.Username}");
                Console.WriteLine($"Senha: {user.Senha}");
                EsperaTecla(ConsoleKey.Enter);
                Console.Clear();
            }
        }
        
        public static void AddCPF(Usuario user)
        {
            Console.WriteLine("Você ainda não cadastrou o seu CPF!");
            Console.Write("Insira seu CPF: ");
            string CPF = Console.ReadLine();

            if (CPF.Any(x => !char.IsDigit(x)))
                CPF = FormatacaoCPF.CorrigeCPF(CPF);

            if (Verificacao.CPFExiste(FormatacaoCPF.FormataCPF(CPF)))
            {
                Console.WriteLine("Opa! Já existe uma conta cadastrada com o seu CPF!");
                Console.WriteLine("Esqueceu seu nome de usuário ou senha? Utilize a opção para isso!");
                Console.WriteLine("Não foi você que se cadastrou? Por favor, contate o nosso suporte.");
                EsperaTecla(ConsoleKey.Enter);
            }
            
            if (!Verificacao.VerificaCPF(CPF))
            {
                Console.WriteLine("Seu CPF é inválido. Você realizará logout.");
                EsperaTecla(ConsoleKey.Enter);
                return;
            }
            CPF = FormatacaoCPF.FormataCPF(CPF);
            
            UsuariosContext context = new();
            UsuarioController usuarioController = new(context);
            usuarioController.AtualizarCPF(user, CPF);
        }
        public static void EsperaTecla(ConsoleKey key)
        {
            Console.WriteLine("Pressione enter para continuar.");
            while (Console.ReadKey(true).Key != key)
            { 

            }
        }
    }
}