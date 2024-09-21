using System.Text.RegularExpressions;
using WTranslate;

namespace Translate.Google
{
    public class TranslateGoogle : GetTranslateGoogle, ITranslate
    {

        async Task<string?> ITranslate.TranslateTextAsync(string texto, string IdiomaOrigem, string IdiomaDestino)
        {
            var html = await GetTranslateAsync(texto, IdiomaOrigem, IdiomaDestino);

            if (html != null)
            {
                var regex = new Regex("<div class=\"result-container\">([^<]*)</div>");
                var match = regex.Match(html);
                if (match.Success)
                {
                    var textFin = match.Groups[1].Value;

                    return textFin;
                }
                else
                {
                    throw new Exception("Não foi possível extrair o texto da tradução");
                }
            }
            else
            {
                return null;
            }
        }

    }
}
