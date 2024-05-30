using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
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
            using (var db = new UsuariosContext())
            {
                UsuariosContext context = new UsuariosContext();
                var usuarioController = new UsuarioController(context);
                string senha = Console.ReadLine();
                usuarioController.MudarSenha(user, senha);
            }
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
                    char.ToUpper(resposta);
                }
            } while (resposta == 'N');
            Console.Write("Insira o valor da transferência: R$ ");
            decimal valorTransferencia = decimal.Parse(Console.ReadLine());
            if (valorTransferencia > user.Saldo || valorTransferencia <= 0)
            {
                Console.WriteLine("Impossível realizar a transferência.");
            }

            Console.WriteLine($"Você realmente deseja realizar uma transferência no valor de R$ {valorTransferencia} para" +
                $" {receber.Nome} {receber.Sobrenome}? [S/N]: ");
            char resposta2 = char.Parse(Console.ReadLine());

            if (resposta2 == 'N')
            {
                Console.WriteLine("Transferência cancelada.");
                return;
            }

            UsuariosContext context = new UsuariosContext();
            var usuarioController = new UsuarioController(context);
            usuarioController.TransferirEntreContas(user, receber, valorTransferencia);
            Console.WriteLine("Transferência realizada com sucesso.");
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
            while (Console.ReadKey(true).Key != key)
            { 

            }
        }
    }
}