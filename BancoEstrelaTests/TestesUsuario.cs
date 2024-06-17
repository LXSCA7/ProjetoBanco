using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ProjetoBanco.Models;
using ProjetoBanco.Controllers;
using ProjetoBanco.Context;

namespace BancoEstrelaTests
{
    public class TestesUsuario
    {
        [Fact]
        public void RealizaTransferencia_DeveRetornarNovoSaldo_DosDoisUsuarios()
        {
            var user1 = new Usuario() {
                Nome = "Joao",
                Sobrenome = "da Silva",
                CPF = "123.123.123-12",
                Senha = "MyStr0ngPassword(!)",
                Saldo = 150.00M
            };

            var user2 = new Usuario() {
                Nome = "Carlos",
                Sobrenome = "Alberto",
                CPF = "321.321.321-32",
                Senha = "SenhaF0rte(!)",
                Saldo = 2500.00M
            };

            decimal valor = 100M;
            decimal saldoUsuario = user1.Saldo - valor;

            UsuariosContext context = new();
            UsuarioController usuarioController = new(context);

            usuarioController.TransferirEntreContas(user1, user2, valor);

            Assert.Equal(saldoUsuario, user1.Saldo);
        }

        [Fact]
        public void NaoRealizaTransferencia_DeveRetornarErro_SaldoImpossivel()
        {
            var user1 = new Usuario()
            {
                Nome = "Joao",
                Sobrenome = "da Silva",
                CPF = "123.123.123-12",
                Senha = "MyStr0ngPassword(!)",
                Saldo = 150.00M
            };

            var user2 = new Usuario()
            {
                Nome = "Carlos",
                Sobrenome = "Alberto",
                CPF = "321.321.321-32",
                Senha = "SenhaF0rte(!)",
                Saldo = 2500.00M
            };

            decimal valor = 500M;
            decimal saldoUsuario = user1.Saldo - valor;

            UsuariosContext context = new();
            UsuarioController usuarioController = new(context);

            var exception = Assert.Throws<Exception>(() => usuarioController.TransferirEntreContas(user1, user2, valor));

            Assert.Equal("Erro: saldo do usuario menor que valor.", exception.Message);
        }
    
        [Fact]
        public void ConfereSenhaDoUsuario_DeveRetornarTrue_ParaSenhasCorretas()
        {
            // usuario e senha existentes no DB
            string username = "teste"; 
            string senha = "Teste12#";

            UsuariosContext context = new();
            UsuarioController usuarioController = new(context);

            bool resultado = usuarioController.SenhaCorreta(username, senha); // busca no banco de dados

            Assert.True(resultado);
        }
    }
}