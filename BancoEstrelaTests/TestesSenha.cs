using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ProjetoBanco.Models;

namespace BancoEstrelaTests
{
    public class TestesSenha
    {
        [Fact]
        public void VerificaSenhaForte_DeveRetornarTrue_ParaSenhasAprovadas()
        {
            string senha = "MyStr0ngPassword(!)";

            bool resultado = Verificacao.SenhaAprovada(senha);

            Assert.True(resultado);
        }

        [Fact]
        public void VerificaSenhaInvalida_DeveRetornarFalse()
        {
            string senhaInvalida = "senha123 123 com espaços em branco";

            bool resultado = Verificacao.SenhaAprovada(senhaInvalida);

            Assert.False(resultado);
        }
    }
}