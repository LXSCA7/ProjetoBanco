using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBanco.Models
{
    public class FormatacaoCPF
    {
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
    }
}