using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using System.Web;

namespace SMDriveV2.Controllers
{
    public class Extrator
    {

        class MyWebClient : WebClient
        {
            protected override WebRequest GetWebRequest(Uri address)
            {
                HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;

                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

                return request;
            }
        }

        public List<string> ExtraiHAPBehance()
        {
            List<string> imagens = new List<string>();

            var url = "https://www.behance.net/brenoGomesSousa";

            var data = new MyWebClient().DownloadString(url);

            var doc = new HtmlAgilityPack.HtmlDocument();

            doc.LoadHtml(data);

            var node = doc.DocumentNode.SelectNodes("//a").Where(d => d.GetAttributeValue("class", "").Contains(
                        "rf-project-cover__image-container js-project-cover-image-link js-project-link"));

            foreach (var item in node)
            {
                imagens.Add(item.OuterHtml);
                if (imagens.Count >= 12)
                {
                    break;
                }
            }

            return imagens;
        }

        public static Tuple<List<string>, List<string>, List<string>, List<string>> ExtraiPortfolioBehance()
        {
            List<string> imagens = new List<string>();
            List<string> links = new List<string>();
            List<string> nomes = new List<string>();
            List<string> tipos = new List<string>();


            var url = "https://www.behance.net/brenoGomesSousa";

            var data = new MyWebClient().DownloadString(url);

            var doc = new HtmlAgilityPack.HtmlDocument();

            doc.LoadHtml(data);

            var node = doc.DocumentNode.SelectNodes("//div").Where(d => d.GetAttributeValue("class", "").Contains(
                        "rf-project-cover rf-project-cover--project js-item js-project-cover qa-project-cover editable"));

            if (node.Count() >= 0)
            {         
                foreach (var item in node)
                {
                    var projeto = new HtmlAgilityPack.HtmlDocument();
                    projeto.LoadHtml(item.OuterHtml.ToString());

                    var imagem = projeto.DocumentNode.SelectNodes("//img").Where(p => p.GetAttributeValue("class", "").Contains(
                        "rf-project-cover__image"));
                    var link = projeto.DocumentNode.SelectNodes("//a").Where(p => p.GetAttributeValue("class", "").Contains(
                        "rf-project-cover__image-container js-project-cover-image-link js-project-link"));
                    var nome = projeto.DocumentNode.SelectNodes("//a").Where(p => p.GetAttributeValue("class", "").Contains(
                        "rf-project-cover__title js-project-cover-title-link"));
                    var tipo = projeto.DocumentNode.SelectNodes("//a").Where(p => p.GetAttributeValue("class", "").Contains(
                        "rf-project-cover__field-link"));

                    imagens.Add(imagem.FirstOrDefault().Attributes["src"].Value);
                    links.Add(link.FirstOrDefault().Attributes["href"].Value);
                    nomes.Add(nome.FirstOrDefault().InnerText);
                    tipos.Add(tipo.FirstOrDefault().InnerText);
                }
            }

            return Tuple.Create(imagens, links, nomes, tipos);
        }
    }
}