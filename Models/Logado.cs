using System;
using System.Collections.Generic;
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
        public static void MudarSenhaDuda()
        {
            using (var db = new UsuariosContext())
            {
                UsuariosContext context = new UsuariosContext();
                var usuarioController = new UsuarioController(context);

                Usuario duda = new Usuario();
                duda = db.Usuarios.Find(10);
                string senha = "Santosk11.";
                usuarioController.MudarSenha(duda, senha);
            }
        }

        public static void Transferir(Usuario user)
        {
            char resposta;
            Usuario receber = new Usuario();
            do
            {
                Console.WriteLine("Insira o número da conta: ");
                int conta = Int32.Parse(Console.ReadLine());
                using (var db = new UsuariosContext())
                {
                    receber = db.Usuarios.Find(conta);
                    if (receber == null)
                    {
                        Console.WriteLine("Conta não encontrada...");
                        return;
                    }
                    Console.WriteLine($"Transferência: para {receber.Nome} + {receber.Sobrenome}");
                    Console.WriteLine("Isso está correto? [S/N]: ");
                    resposta = char.Parse(Console.ReadLine());
                    char.ToUpper(resposta);
                }
            } while (resposta == 'N');
            Console.WriteLine("Insira o valor da transferência: ");
            decimal valorTransferencia = decimal.Parse(Console.ReadLine());
            if (valorTransferencia > user.Saldo || valorTransferencia <= 0)
            {
                Console.WriteLine("Impossível realizar a transferência");
            }

            Console.WriteLine($"Você realmente deseja realizar uma transferência no valor de {valorTransferencia} para" +
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
    }
}