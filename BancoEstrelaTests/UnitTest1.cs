using BancoEstrela.Models.Verificacao;

namespace BancoEstrelaTests;

public class UnitTest1
{
    [Fact]
    public void CriarConta_DeveRetornarUsuario()
    {
        var context = new UsuariosContext();
        var controller = new UsuarioController(context);
        

    }
}