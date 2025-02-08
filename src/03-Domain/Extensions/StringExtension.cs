using System.Diagnostics;
using System.Text.RegularExpressions;

namespace PosTech.Hackathon.Pacientes.Domain.Extensions;

public static class StringExtension
{
    public static bool IsValidName(this string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        try
        {
            string pattern = @"^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$";
            return Regex.IsMatch(name, pattern, RegexOptions.IgnoreCase);
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    public static bool IsValidEmail(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    public static bool IsValidCPF(this string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        cpf = Regex.Replace(cpf, "[^0-9]", ""); 

        if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
            return false;

        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf.Substring(0, 9);
        int soma = tempCpf.Select((t, i) => (t - '0') * multiplicador1[i]).Sum();
        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        tempCpf += digito1;
        soma = tempCpf.Select((t, i) => (t - '0') * multiplicador2[i]).Sum();
        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        return cpf.EndsWith(digito1.ToString() + digito2.ToString());
    }
}
