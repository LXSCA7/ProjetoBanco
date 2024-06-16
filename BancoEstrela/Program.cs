using System.Diagnostics.Tracing;
using ProjetoBanco.Models;
using ProjetoBanco.Context;
using ProjetoBanco.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Net.NetworkInformation;
using System.Security;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Security.Cryptography;
using ProjetoBanco.Migrations;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Clear();
        bool repete = true;
        do {
            Menu();
            int esc;
            string input = Console.ReadLine();
            bool sucesso = Int32.TryParse(input, out esc);
            if (!sucesso)
            {
                esc = 99;
            }    
            switch (esc)
            {
                case 1:
                    PrimeiroCaso();
                    break;
                    
                case 2:
                    Usuario userLogado = CasoLogin(); 
                    Console.Clear();
                    if (userLogado == null)
                    {
                        Console.WriteLine("Não foi possível realizar seu login. Tente novamente mais tarde!");
                        Thread.Sleep(500);
                        break;
                    }
                    InfoLogado(userLogado);
                    break;

                case 3:
                    Console.Clear();
                    DepositoOutroBanco();
                    break;

                case 4:
                    ListaUsuarios();
                    break;
                    
                case 5:
                    EsqueceuUsername();
                    break;

                case 6:
                    EsqueceuSenha();
                    break;
                
                case 7:
                    Escreve("Encerrando o programa");
                    repete = false;
                    break;

                default:
                    Console.WriteLine("| Opção inválida. Por favor, insira novamente!");
                    Logado.EsperaTecla(ConsoleKey.Enter);
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
        Console.WriteLine("| [3] Fazer depósito de outro banco          |");
        Console.WriteLine("| [4] Listar usuários cadastrados            |");
        Console.WriteLine("| [5] Esqueci meu nome de usuário            |");
        Console.WriteLine("| [6] Esqueci minha senha                    |");
        Console.WriteLine("|                                            |");
        Console.WriteLine("| [7] Sair                                   |");
        Console.WriteLine("|                                            |");
        Console.WriteLine("+--------------------------------------------+");
        Console.Write("| SUA ESCOLHA: ");
    }

    public static void Escreve(string frase)
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
        if (!Verificacao.VerificaNascimento(nascimento))
        {
            Console.WriteLine("O Banco Estrela só aceita usuários maiores de idade.");
            return;
        }
        Console.Clear();
        Cabecalho();
        Console.WriteLine($"Olá, {nome} {sobrenome}, vamos continuar com o seu cadastro!");
        bool erroVerificacao;
        string username;
        Console.Write("Insira seu CPF: ");
        string CPF = Console.ReadLine();

        if (CPF.ToLower() == "test" || CPF.ToLower() == "teste")
        {
            string cpfTeste = CPFGenerator.GetCpf();
            CPF = cpfTeste;
            if (CPF == null)
            {
                Console.WriteLine("Erro ao gerar CPF de teste.");
                return;
            }
            Console.WriteLine($"CPF de teste gerado: {CPF}");
        }

        if (CPF.Any(x => !char.IsDigit(x)))
            CPF = FormatacaoCPF.CorrigeCPF(CPF);

        if (Verificacao.CPFExiste(FormatacaoCPF.FormataCPF(CPF)))
        {
            Console.WriteLine("Opa! Já existe uma conta cadastrada com o seu CPF!");
            Console.WriteLine("Esqueceu seu nome de usuário ou senha? Utilize a opção para isso!");
            Console.WriteLine("Não foi você que se cadastrou? Por favor, contate o nosso suporte.");
            Logado.EsperaTecla(ConsoleKey.Enter);
            return;
        }

        if (!Verificacao.VerificaCPF(CPF) || CPF.All(c => c == CPF[0]))
        {
            Console.WriteLine("Seu CPF é inválido. Infelizmente não podemos prosseguir com a criação da conta.");
            Logado.EsperaTecla(ConsoleKey.Enter);
            return;
        }
        CPF = FormatacaoCPF.FormataCPF(CPF);
        do
        {
            erroVerificacao = false;
            Console.Write("Insira um nome de usuário: ");
            username = Console.ReadLine();
            Escreve("Verificando nome de usuário");

            if (username.IsNullOrEmpty())
            {
                Console.WriteLine("Nome de usuário não pode ficar em branco.");
                Thread.Sleep(300);
                erroVerificacao = true;
            }

            if (Verificacao.UsuarioExiste(username))
            {
                Console.WriteLine("Opa! Esse nome de usuário já existe! Escolha outro.");
                Thread.Sleep(300);
                erroVerificacao = true;
            }

            if (!Verificacao.UsernameAprovado(username))
            {
                Console.WriteLine("O nome de usuário não pode conter espaços em branco. Tente novamente.");
                Thread.Sleep(300);
                erroVerificacao = true;
            }

        } while (erroVerificacao);
        Console.WriteLine("Certo! Seu nome de usuário é " + username);
        string senha = PegarSenha();
        Console.WriteLine("\nPergunta de segurança - Lembre sua resposta! Ela é importante para recuperação da sua conta.");
        string resposta;
        do {
            Console.WriteLine("Qual o nome do seu prato favorito?");
            Console.Write("Resposta: ");
            resposta = Console.ReadLine();

            if (resposta.IsNullOrEmpty())
                Console.WriteLine("A resposta não pode ficar em branco. Tente novamente.");
        } while (resposta.IsNullOrEmpty());

        Console.Clear();
        Usuario novoUsuario = new Usuario
        {
            Nome = nome,
            Sobrenome = sobrenome,
            DataDeNascimento = nascimento,
            Username = username,
            Senha = senha,
            Saldo = 0.00M,
            CPF = CPF,
            PerguntaDeSeguranca = resposta
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

    private static string PegarSenha()
    {
        string senha;
        string senha2;
        bool erroVerificacao;
        do 
        {
            erroVerificacao = false;
            Console.Write("Insira uma senha: ");
            SecureString testeSenha = PegaSenhaEscondido();

            senha = new System.Net.NetworkCredential(string.Empty, testeSenha).Password;

            if (!Verificacao.SenhaAprovada(senha))
            {
                Console.WriteLine("\nOps, sua senha não bate com os requisitos mínimos:");
                Console.WriteLine(" - MIN 8 CARACTERES");
                Console.WriteLine(" - MIN 1 CARACTER ESPECIAL");
                Console.WriteLine(" - MIN 1 NUMERO");
                Console.WriteLine(" - MIN 1 LETRA MAIÚSCULA");
                Console.WriteLine(" - MIN 1 LETRA MINÚSCULA");
                Thread.Sleep(2000);
                erroVerificacao = true;
            }
        } while (erroVerificacao);
        do
        {
            erroVerificacao = false;
            Console.Write("\nConfirme sua senha: ");
            SecureString senhaEsc = PegaSenhaEscondido();
            senha2 = new System.Net.NetworkCredential(string.Empty, senhaEsc).Password;
            if (senha2 != senha)
            {
                Console.WriteLine("\nSuas senhas não correspondem. Tente novamente.");
                erroVerificacao = true;
            }
        } while (erroVerificacao);

        return senha;
    }
    private static Usuario CasoLogin()
    {
        Console.Clear();
        Cabecalho();
        Console.Write("Nome de usuário: ");
        string loginUsuario = Console.ReadLine();
        Escreve("Buscando nome de usuário");
        if(!Verificacao.UsuarioExiste(loginUsuario))
        {
            Console.WriteLine("Usuário não encontrado.");
            return null;
        }
        bool erroVerificacao;
        int quantTentativas = 0;
        string loginSenha;
        do
        {
            erroVerificacao = false;
            Console.Write("Digite sua senha: ");
            SecureString loginSenhaSegura = PegaSenhaEscondido();
            loginSenha = new System.Net.NetworkCredential(string.Empty, loginSenhaSegura).Password;
            UsuariosContext context = new UsuariosContext();
            UsuarioController usuarioController = new UsuarioController(context);

            if (!usuarioController.SenhaCorreta(loginUsuario, loginSenha) && quantTentativas < 3)
            {
                Console.WriteLine("\nSenha incorreta. Tente novamente");
                erroVerificacao = true;
                quantTentativas++;
            }
            if (quantTentativas >= 3)
            {
                Console.WriteLine("Tentativa máxima atingida.");
                Thread.Sleep(400);
                return null;
            }
        } while (erroVerificacao);

        UsuariosContext context2 = new UsuariosContext();
        UsuarioController usuarioController2 = new UsuarioController(context2);
        Usuario usuarioLogado = usuarioController2.RealizaLogin(loginUsuario, loginSenha);
        if (usuarioLogado != null)
        {
            return usuarioLogado;
        }
        return null;
    }

    private static void EsqueceuSenha()
    {
        Console.Clear();
        Cabecalho();
        Console.Write("Insira seu nome de usuário: ");
        string username = Console.ReadLine();
        if(!Verificacao.UsuarioExiste(username))
        {
            Console.WriteLine("Usuário inexistente.");
            Logado.EsperaTecla(ConsoleKey.Enter);
            return;
        }

        UsuariosContext context = new();
        UsuarioController usuarioController = new(context);
        var user = context.Usuarios.SingleOrDefault(u => u.Username == username);

        
        Console.Write("Insira seu CPF: ");
        string CPF = Console.ReadLine();
        CPF = FormatacaoCPF.CorrigeCPF(CPF);
        CPF = FormatacaoCPF.FormataCPF(CPF);

        if (CPF.IsNullOrEmpty() || CPF != user.CPF)
        {
            Console.WriteLine("CPF Incorreto.");
            Logado.EsperaTecla(ConsoleKey.Enter);
            return;
        }

        Console.Write("Insira a resposta de segurança: ");
        string resposta = Console.ReadLine();

        if (resposta.IsNullOrEmpty() || resposta.ToLower() != user.PerguntaDeSeguranca.ToLower())
        {
            Console.WriteLine("Resposta incorreta.");
            Logado.EsperaTecla(ConsoleKey.Enter);
            return;
        }

        Console.WriteLine($"Olá, {user.Nome} {user.Sobrenome}.");
        string novaSenha = PegarSenha();
        usuarioController.MudarSenha(user, novaSenha);
        Console.WriteLine("\nSua senha foi atualizada!");
        Logado.EsperaTecla(ConsoleKey.Enter);
        Console.Clear();
        return;
    }

    private static void EsqueceuUsername() {
        Console.Clear();
        Cabecalho();
        Console.Write("Insira seu CPF: ");
        string CPF = Console.ReadLine();
        CPF = FormatacaoCPF.CorrigeCPF(CPF);
        CPF = FormatacaoCPF.FormataCPF(CPF);

        UsuariosContext context = new();
        UsuarioController usuarioController = new(context);

        var user = context.Usuarios.SingleOrDefault(u => u.CPF == CPF);
        if (user == null)
        {
            Console.WriteLine("CPF inválido ou inexistente.");
            Logado.EsperaTecla(ConsoleKey.Enter);
            Console.Clear();
            return;
        }

        Console.Write("Insira sua data de nascimento [dd/MM/aaaa]: ");
        DateTime data = DateTime.Parse(Console.ReadLine());

        if (data != user.DataDeNascimento)
        {
            Console.WriteLine("Data inválida ou incorreta.");
            Logado.EsperaTecla(ConsoleKey.Enter);
            Console.Clear();
            return;
        }

        Console.WriteLine("Insira a resposta para a pergunta de segurança.");
        Console.Write("Qual o seu prato favorito? ");
        string resposta = Console.ReadLine();
        if (resposta != user.PerguntaDeSeguranca)
        {
            Console.WriteLine("Resposta incorreta.");
            Logado.EsperaTecla(ConsoleKey.Enter);
            Console.Clear();
            return;
        }

        string novoUsername;
        bool repetir;
        do
        {
            repetir = false;
            Console.Write($"Olá {user.Nome} {user.Sobrenome}, insira seu nome novo de usuário: ");
            novoUsername = Console.ReadLine();

            if (novoUsername == user.Username) 
            {
                Console.WriteLine("Seu novo nome de usuário não pode ser igual o anterior.");
                Logado.EsperaTecla(ConsoleKey.Enter);
                Console.Clear();
                return;
            }

            if (!Verificacao.UsernameAprovado(novoUsername))
            {
                Console.WriteLine("Seu nome de usuário é inválido.");
                Logado.EsperaTecla(ConsoleKey.Enter);
                Console.Clear();
                return;
            }


            if (Verificacao.UsuarioExiste(novoUsername))
            {
                Console.WriteLine("Seu nome de usuário deve ser único. Tente novamente.");
                repetir = true;
            }
        } while (repetir);

        usuarioController.AtualizarUsername(user, novoUsername);
        Console.WriteLine($"Suas credenciais foram atualizadas com sucesso. Seu novo nome de usuário é: {user.Username}");
        Logado.EsperaTecla(ConsoleKey.Enter);
        return;
    }
    private static void InfoLogado(Usuario user)
    {
        bool logout = false;
        do
        {
            if (user.CPF == null)
            {
                Cabecalho();
                Logado.AddCPF(user);
                if (user.CPF == null) {
                    Escreve("Saindo da conta");
                    Console.Clear();
                    logout = true;
                }
            }

            if (logout == true)
                return;
            

            Cabecalho();
            Console.WriteLine("Olá, " + user.Nome);
            Console.WriteLine("Seu saldo: R$ " + user.Saldo);
            Console.WriteLine("[1] Conferir suas informações");
            Console.WriteLine("[2] Realizar um saque");
            Console.WriteLine("[3] Realizar uma transferência");
            Console.WriteLine("[4] Mudar senha");
            Console.WriteLine("[5] Deletar sua conta\n");

            Console.WriteLine("[6] Sair");
            Console.Write("Escolha: ");
            int esc = Int32.Parse(Console.ReadLine());
            switch (esc)
            {
                case 1:
                    Console.Clear();
                    Cabecalho();
                    Logado.MostrarInformacoes(user);
                    break;
                case 2:
                    Console.Clear();
                    Cabecalho();
                    Logado.Sacar(user);
                    Console.Clear();
                    break;
                case 3:
                    Console.Clear();
                    Cabecalho();
                    Logado.Transferir(user);
                    Console.Clear();
                    break;
                case 4:
                    Console.Clear();
                    Cabecalho();
                    Logado.MudarSenha(user);
                    Console.Clear();
                    break;
                case 5:
                    Console.Clear();
                    Cabecalho();
                    Logado.ApagarConta(user);
                    logout = true;
                    break;
                default:
                    Console.Clear();
                    logout = true;
                    break;
            }
        } while (!logout);
        Escreve("Saindo da conta");
        Console.Clear();
    }
    public static SecureString PegaSenhaEscondido()
    {
        SecureString senha = new SecureString();
        ConsoleKeyInfo tecla;
        do
        {
            tecla = Console.ReadKey(true);
            if (tecla.Key != ConsoleKey.Backspace && tecla.Key != ConsoleKey.Enter)
            {
                senha.AppendChar(tecla.KeyChar);
                Console.Write("*");
            }
            else
            {
                if (senha.Length > 0 && tecla.Key != ConsoleKey.Enter)
                {
                    senha.RemoveAt(senha.Length - 1);
                    Console.Write("\b \b");
                }
            }
        } while (tecla.Key != ConsoleKey.Enter);
        return senha;
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
                Console.WriteLine("Conta não encontrada.");
                Logado.EsperaTecla(ConsoleKey.Enter);
                return;
            }
            Console.WriteLine($"Número da Conta: {receber.Id} | Nome: {receber.Nome} {receber.Sobrenome}");
            Console.Write("Isso está correto? [S/N]: ");
            string correto = Console.ReadLine();
            correto = correto.ToUpper();
            char resposta = correto[0];
            if (resposta == 'N')
            {
                Console.WriteLine("Depósito cancelado.");
                Logado.EsperaTecla(ConsoleKey.Enter);
                Console.Clear();
                return;
            }
            
            Console.Write($"Insira o valor que será depositado para {receber.Nome} {receber.Sobrenome}: R$ ");
            decimal valorDepositado = Decimal.Parse(Console.ReadLine());

            if (valorDepositado <= 0)
            {
                Console.WriteLine("Impossível realizar depósito.");
                Logado.EsperaTecla(ConsoleKey.Enter);
                Console.Clear();
                return;
            }

            UsuariosContext context = new UsuariosContext();
            UsuarioController usuarioController = new UsuarioController(context);
            usuarioController.AddSaldo(receber, receber.Id, valorDepositado);

            Console.WriteLine("Depósito realizado.");
            Logado.EsperaTecla(ConsoleKey.Enter);
            Console.Clear();
        }
    }
}