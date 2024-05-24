internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("#============================================#");
        Console.WriteLine("#             BANCO DO BRASILLLL             #");
        Console.WriteLine("#============================================#");
        Console.WriteLine("# [1] PARA CRIAR UMA CONTA                   #");
        Console.WriteLine("# [2] PARA FAZER LOGIN                       #");
        Console.WriteLine("# [3] PARA FAZER UM DEPÓSITO DE OUTRO BANCO  #");
        Console.WriteLine("#============================================#");
        Console.Write("# SUA ESCOLHA: ");
        int esc = Int32.Parse(Console.ReadLine());
    }
}