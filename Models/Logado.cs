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
                if (senha == null)
                {
                    Console.WriteLine("Operação cancelada.");
                    EsperaTecla(ConsoleKey.Enter);
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
            if (username != user.Username)
            {
                Console.WriteLine("Nome de usuário incorreto.");
                EsperaTecla(ConsoleKey.Enter);
                return;
            }
            Console.Write("Insira sua senha: ");
            SecureString senha = Program.PegaSenhaEscondido();
            string senhaString = new System.Net.NetworkCredential(string.Empty, senha).Password;
            if (senhaString != user.Senha)
            {
                Console.WriteLine("Nome de usuário incorreto.");
                EsperaTecla(ConsoleKey.Enter);
                return;
            }
            UsuariosContext context = new();
            UsuarioController usuarioController = new(context);

            usuarioController.RemoverConta(user);
            Console.WriteLine("Sua conta foi deletada.");
            EsperaTecla(ConsoleKey.Enter);
        }
        public static void Sacar(Usuario user)
        { 
            decimal valorSaque;
            do
            {
                Console.WriteLine("Digite 0 para cancelar o saque.");
                Console.Write("Insira o valor: R$ ");
                valorSaque = decimal.Parse(Console.ReadLine());
                if (valorSaque == 0)
                {
                    Program.Escreve("Cancelando saque");
                    Console.Write("Pressione enter para continuar."); 
                    EsperaTecla(ConsoleKey.Enter);
                    return;
                }
                if (valorSaque > user.Saldo)
                {
                    Console.WriteLine("Impossível realizar saque com esse valor. Tente novamente.");
                }
            } while (valorSaque > user.Saldo || valorSaque < 0);


            UsuariosContext context = new();
            UsuarioController usuarioController = new(context);
            usuarioController.Sacar(user, valorSaque);
            Console.WriteLine($"Saque realizado. Novo saldo: R$ {user.Saldo}");
            Console.WriteLine("Aperte enter para continuar. ");
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
                Console.Write("Pressione enter para continuar."); 
                EsperaTecla(ConsoleKey.Enter);
                Console.Clear();
            }
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