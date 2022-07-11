namespace WTranslate
{
    interface ITranslate
    {
        Task<string?> TranslateTextAsync(string texto, string IdiomaOrigem, string IdiomaDestino);
    }
}
