using ProjetoBanco.Models;

namespace BancoEstrelaTests;

public class UnitTest1
{
    [Fact]
    public void VerificaCPFValido_DeveRetornarFalse_ParaCPFsInvalidos()
    {
        string cpf = "986.848.840-00";
        cpf = FormatacaoCPF.CorrigeCPF(cpf);

        bool resultado = Verificacao.VerificaCPF(cpf);

        Assert.True(resultado);
    }

    [Fact]
    public void VerificaCPFInvalido_DeveRetornarFalse()
    {
        string cpf = "152.127.315-55";
        cpf = FormatacaoCPF.CorrigeCPF(cpf);
        
        bool resultado = Verificacao.VerificaCPF(cpf);

        Assert.False(resultado);
    }

    [Fact]
    public void VerificaExistenciaDoCPF_DeveRetornarTrue_ParaCPFsExistentes()
    {
        string cpf = "986.848.840-00";

        bool resultado = Verificacao.CPFExiste(cpf);

        Assert.True(resultado);
    }

    [Fact]
    public void VerificaExistenciaDoCPF_DeveRetornarFalse_ParaCPFsInexistentes()
    {
        string cpf = "123.456.789-00";
        cpf = FormatacaoCPF.CorrigeCPF(cpf);

        bool resultado = Verificacao.CPFExiste(cpf);

        Assert.False(resultado);
    }
}