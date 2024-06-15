using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace ProjetoBanco.Models
{
    public class CPFGenerator
    {
        private static readonly HttpClient client = new HttpClient();
        private const string apiUrl = "https://api.invertexto.com/v1/faker?token=8139%7CR8be900dl0LQ48ijS7Lkx8CC7beDeRmq&fields=cpf&locale=pt_BR";

        public static async Task<string> GetCpfAsync()
        {
            try 
            {
                var resposta = await client.GetStringAsync(apiUrl);
                var json = JObject.Parse(resposta);
                return json["cpf"]?.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter CPF: {ex.Message}");
                return null;
            }
        }

        public static string GetCpf()
        {
            try
            {
                Task<string> task = GetCpfAsync();
                task.Wait();
                return task.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter CPF: {ex.Message}");
                return null;
            }
        }
    }
}