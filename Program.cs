using System.Diagnostics.Tracing;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Clear();
        bool repete = true;
        do {
            Menu();
            int esc = Int32.Parse(Console.ReadLine());
            switch (esc)
            {
                case 1:
                    Console.Clear();
                    break;
                    
                case 2:
                    Console.Clear();
                    break;

                case 3:
                    Console.Clear();
                    break;

                case 4:
                    Console.Clear();
                    break;

                case 5:
                    EncerraPrograma();
                    repete = false;
                    break;
                default:
                    Console.WriteLine("| Opção inválida. Por favor, insira novamente!");
                    Thread.Sleep(1000);
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
        Console.WriteLine("| [3] Encerrar sua conta                     |");
        Console.WriteLine("| [4] Fazer depósito de outro banco          |");
        Console.WriteLine("|                                            |");
        Console.WriteLine("| [5] Sair                                   |");
        Console.WriteLine("|                                            |");
        Console.WriteLine("+--------------------------------------------+");
        Console.Write("| SUA ESCOLHA: ");
    }

    private static void EncerraPrograma()
    {
        int repetitions = 6;
        int interval = 230; // 230ms

        for (int i = 0; i < repetitions; i++)
        {
            switch (i % 3)
            {
                case 0:
                    Console.Write("Encerrando o programa.  \r");
                    break;
                case 1:
                    Console.Write("Encerrando o programa.. \r");
                    break;
                case 2:
                    Console.Write("Encerrando o programa... \r");
                    break;
            }
            Thread.Sleep(interval);
        }
        Console.WriteLine("Encerrando o programa...");
    }
}