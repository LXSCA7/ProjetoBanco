using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoBanco.Models;
using Xunit;

namespace BancoEstrelaTests
{
    public class TestesUsername
    {
        [Fact]
        public void VerificaNomeDeUsuario_DeveRetornarTrue_ParaUsuariosValidos()
        {
            string username = "nome_de_usuario";

            bool resultado = Verificacao.UsernameAprovado(username);
        
            Assert.True(resultado);
        }
    }
}