using System.Xml;

namespace WTranslate
{
    public class VerificaNovaversao
    {
        string urlFeed;
        public string urlDl { get; set; }

        public VerificaNovaversao()
        {
            this.urlFeed = "https://sourceforge.net/p/wtranslate/activity/feed";
            this.urlDl = "https://sourceforge.net/projects/wtranslate/";
        }

        public async Task<string?> Verifica()
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(urlFeed);
                var content = await response.Content.ReadAsStringAsync();
                var doc = new XmlDocument();
                doc.LoadXml(content);
                //verificar a nova versão no xml
                var node = doc.SelectSingleNode("/rss/channel/item/title");
                var versao = node.InnerText;
                versao = versao.Substring(versao.IndexOf("V") + 1, versao.IndexOf(".zip") - versao.IndexOf("V") - 1);

                return versao;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
