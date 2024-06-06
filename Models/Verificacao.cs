using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoBanco.Context;

namespace ProjetoBanco.Models
{
    public class Verificacao
    {
        public static bool VerificaNascimento(DateTime data)
        {
            DateTime anoAtual = DateTime.Now;
            if (anoAtual.Year - data.Year < 18)
                return false;

            return true;
        }

        public static bool UsuarioExiste(string nomeDeUsuario)
        {
            using (var db = new UsuariosContext())
            {
                return db.Usuarios.Any(u => u.Username == nomeDeUsuario);
            }
        }

        public static bool UsernameAprovado(string nomeDeUsuario)
        {
            foreach (char c in nomeDeUsuario)
            {
                if (c == ' ')
                    return false;
            }

            return true;
        }

        public static string CorrigeCPF(string CPF)
        {
            StringBuilder newCPF = new();
            foreach (char c in CPF)
            {
                if (char.IsDigit(c))
                    newCPF.Append(c);
            }

            return newCPF.ToString();
        }

        public static bool VerificaCPF(string CPF)
        {
            if (CPF.Length != 11)
                return false;

            int[] CPFSeparado = new int[11];
            for (int i = 0; i < CPF.Length; i++)
            {
                CPFSeparado[i] = int.Parse(CPF[i].ToString());  
            }

            int valorPrimeiraVerificacao = 0;
            for (int i = 0; i < 9; i++)
            {
                valorPrimeiraVerificacao += CPFSeparado[i] * (10 - i);
            }

            valorPrimeiraVerificacao = valorPrimeiraVerificacao * 10 % 11;
            if (valorPrimeiraVerificacao == 10 || valorPrimeiraVerificacao == 11)
                valorPrimeiraVerificacao = 0;
            if (valorPrimeiraVerificacao != CPFSeparado[9])
                return false;
            
            int valorSegundaVerificacao = 0;
            for (int i = 0; i < 10; i++)
            {
                valorSegundaVerificacao += CPFSeparado[i] * (11 - i);
            }

            valorSegundaVerificacao = valorSegundaVerificacao * 10 % 11;
            if (valorSegundaVerificacao == 10 || valorSegundaVerificacao == 11)
                valorSegundaVerificacao = 0;
            if (valorSegundaVerificacao != CPFSeparado[10])
                return false;

            return true;
        }

        public static string FormataCPF(string CPF)
        {
            StringBuilder newCPF = new();
            for (int i = 0; i < CPF.Length; i++)
            {
                newCPF.Append(CPF[i]);
                if (i == 2 || i == 5)
                    newCPF.Append('.');
                if (i == 8)
                    newCPF.Append('-');
            }

            return newCPF.ToString();
        }

        public static bool CPFExiste(string CPF)
        {
            using (var db = new UsuariosContext())
            {
                return db.Usuarios.Any(u => u.CPF == CPF);
            }
        }
        public static bool SenhaAprovada(string senha)
        {
            bool caracterEspecial = false;
            bool numeros = false;
            bool letraMin = false;
            bool letraMai = false;
            if (senha.Length < 8)
                return false;

            foreach (char c in senha)
            {
                if (c == ' ')
                    return false;

                if (c == '!' || c == '@' || c == '#' || c == '$' || c == '%' || c == '&' || c == '.' || c == ',')
                    caracterEspecial = true;
                if (char.IsNumber(c))
                    numeros = true;
                if (char.IsUpper(c))
                    letraMai = true;
                if (char.IsLower(c))
                    letraMin = true;
            }

            if (!caracterEspecial || !numeros || !letraMai || !letraMin)
            {
                return false;
            }

            return true;
        }
    
    }
}