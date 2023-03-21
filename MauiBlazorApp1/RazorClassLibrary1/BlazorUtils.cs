using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
//using Twilio.Rest.Api.V2010.Account.Usage.Record;

//namespace DialeticaBlazorApp
//{
public enum TipoArquivo
{
    Desconhecido,
    Imagem,
    PDF,
    Word,
    MP3
}
public static class BlazorUtils
{
    //public static byte[] ConverteStreamToByteArray(this Stream stream)
    //{
    //    byte[] byteArray = new byte[16 * 1024];
    //    using (MemoryStream mStream = new MemoryStream())
    //    {
    //        int bit;
    //        while ((bit = stream.Read(byteArray, 0, byteArray.Length)) > 0)
    //        {
    //            mStream.Write(byteArray, 0, bit);
    //        }
    //        return mStream.ToArray();
    //    }
    //}

    public static string UppercaseFirst(this string s)
    {
        // Check for empty string.
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        // Return char and concat substring.
        return char.ToUpper(s[0]) + s.Substring(1);
    }
    /// <summary>
    /// Get the first several words from the summary.
    /// </summary>
    public static string FirstWords(this string input, int numberWords)
    {
        try
        {
            // Number of words we still want to display.
            int words = numberWords;
            // Loop through entire summary.
            for (int i = 0; i < input.Length; i++)
            {
                // Increment words on a space.
                if (input[i] == ' ')
                {
                    words--;
                }
                // If we have no more words to display, return the substring.
                if (words == 0)
                {
                    return input.Substring(0, i);
                }
            }
        }
        catch (Exception)
        {
            // Log the error.
        }
        return string.Empty;
    }


    public static DateTime TryConvertToDateTime(this string d)
    {
        DateTime result;
        DateTime.TryParse(d, out result);

        return result;
    }
    public static long Idade(this DateTime birthdate)
    {
        // Save today's date.
        var today = DateTime.Today;

        // Calculate the age.
        var age = today.Year - birthdate.Year;

        // Go back to the year in which the person was born in case of a leap year
        if (birthdate.Date > today.AddYears(-age)) age--;

        return age;
    }
    public static TipoArquivo Tipo(this string NomeDoArquivo)
    {
        if (NomeDoArquivo.ToLower().EndsWith(".jpg")
            || NomeDoArquivo.ToLower().EndsWith(".jpeg")
            || NomeDoArquivo.ToLower().EndsWith(".gif")
            || NomeDoArquivo.ToLower().EndsWith(".png")
           )
        {
            return TipoArquivo.Imagem;
        }

        if (NomeDoArquivo.ToLower().EndsWith(".pdf"))
        {
            return TipoArquivo.PDF;
        }

        if (NomeDoArquivo.ToLower().EndsWith(".doc")
            || NomeDoArquivo.ToLower().EndsWith(".docx")
           )
        {
            return TipoArquivo.Word;
        }

        if (NomeDoArquivo.ToLower().EndsWith(".mp3"))
        {
            return TipoArquivo.MP3;
        }

        return TipoArquivo.Desconhecido;
    }

    public static string ptBR_ToString(this DateTime d, string Formato)
    {
        var ci = new System.Globalization.CultureInfo("pt-BR");
        return d.ToString(Formato, ci);
    }
    public static string GMTtoBraziliaTime_ptBR_ToString(this DateTime d, string Formato)
    {
        var ci = new System.Globalization.CultureInfo("pt-BR");
        try
        {
            /*
             41
             E. South America Standard Time
             (GMT-03:00) Brasilia
             */
            //var tzi = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            var tzi = TimeZoneInfo.CreateCustomTimeZone("Brasil", TimeSpan.FromHours(-3), "(GMT-03:00) Brasil", "Hora do Brasil");

            return TimeZoneInfo.ConvertTimeFromUtc(d, tzi).ToUniversalTime().ToLocalTime().ToString(Formato, ci);
        }
        catch
        {
            return DateTime.MinValue.ToString(ci);
        }

    }
    public static string GMTtoBraziliaTime_ptBR_ToString(this DateTime d)
    {
        var ci = new System.Globalization.CultureInfo("pt-BR");
        try
        {
            /*
             41
             E. South America Standard Time
             (GMT-03:00) Brasilia
             */
            //var tzi = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            var tzi = TimeZoneInfo.CreateCustomTimeZone("Brasil", TimeSpan.FromHours(-3), "(GMT-03:00) Brasil", "Hora do Brasil");

            return TimeZoneInfo.ConvertTimeFromUtc(d, tzi).ToUniversalTime().ToLocalTime().ToString(ci);
        }
        catch
        {
            return DateTime.MinValue.ToString(ci);
        }
    }

    public static string PrimeiraPalavra(this string texto)
    {
        var result = System.Text.RegularExpressions.Regex.Match(texto, @"^([\w\-]+)");
        return result.Value;
    }

    public static Guid ToGuid(this string id)
    {
        return Guid.Parse(id);
    }
    public static bool IsGuid(this string value)
    {
        //Guid x;
        return Guid.TryParse(value, out _);
    }

    public static string? GetQueryValue(this NavigationManager NavigationManager, string QueryName)
    {
        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

        if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(QueryName, out var param))
        {
            return param.FirstOrDefault();
        }

        return null;
    }

    public static ObservableCollection<T> ToObservableCollection<T>(this List<T> lista)
    {
        return new ObservableCollection<T>(lista);
    }

    public static async Task<object> Focus(this IJSRuntime jsRuntime, string elementId)
    {
        return await jsRuntime.InvokeAsync<object>("focusElement", elementId);
    }

    //public class CPF
    //{
    public struct Cpf
    {
        private readonly string _value;

        public readonly bool EhValido;
        private Cpf(string value)
        {
            _value = value;

            if (value == null)
            {
                EhValido = false;
                return;
            }

            var posicao = 0;
            var totalDigito1 = 0;
            var totalDigito2 = 0;
            var dv1 = 0;
            var dv2 = 0;

            bool digitosIdenticos = true;
            var ultimoDigito = -1;

            foreach (var c in value)
            {
                if (char.IsDigit(c))
                {
                    var digito = c - '0';
                    if (posicao != 0 && ultimoDigito != digito)
                    {
                        digitosIdenticos = false;
                    }

                    ultimoDigito = digito;
                    if (posicao < 9)
                    {
                        totalDigito1 += digito * (10 - posicao);
                        totalDigito2 += digito * (11 - posicao);
                    }
                    else if (posicao == 9)
                    {
                        dv1 = digito;
                    }
                    else if (posicao == 10)
                    {
                        dv2 = digito;
                    }

                    posicao++;
                }
            }

            if (posicao > 11)
            {
                EhValido = false;
                return;
            }

            if (digitosIdenticos)
            {
                EhValido = false;
                return;
            }

            var digito1 = totalDigito1 % 11;
            digito1 = digito1 < 2
                ? 0
                : 11 - digito1;

            if (dv1 != digito1)
            {
                EhValido = false;
                return;
            }

            totalDigito2 += digito1 * 2;
            var digito2 = totalDigito2 % 11;
            digito2 = digito2 < 2
                ? 0
                : 11 - digito2;

            EhValido = dv2 == digito2;
        }

        public static implicit operator Cpf(string value)
            => new Cpf(value);

        public override string ToString() => _value;
    }

    public static bool ValidarCPF(Cpf sourceCPF) => sourceCPF.EhValido;
    //}

    public static bool IsCPF(this string cpf)
    {
        return ((Cpf)cpf).EhValido;
    }

}
//}

public static class ConfirmExtensions
{
    public static async Task Alert(this IJSRuntime JSRuntime, string message)
    {
        await JSRuntime.InvokeVoidAsync("alert", message);
    }
    public static ValueTask<bool> Confirm(this IJSRuntime JSRuntime, string message)
    {
        return JSRuntime.InvokeAsync<bool>("confirm", message);
    }
    public static async Task OpenTab(this IJSRuntime JSRuntime, string url)
    {
        await JSRuntime.InvokeVoidAsync("AbreNovaGuia", url);
    }
    public static async Task<bool> IsMobile(this IJSRuntime JSRuntime)
    {
        return (await JSRuntime.InvokeAsync<bool>("IsMobile"));
    }

}
