using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using System.Web;
using Microsoft.AspNet.Identity;
using SMDriveV2.Models;

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

        public List<string> MontaMosaico()
        {
            List<string> imagens = new List<string>();

            var urlUsuarios = (new ApplicationDbContext()).Users.Select(w => w.UrlBehance).ToList();
            Random rnd = new Random();
            urlUsuarios = urlUsuarios.OrderBy(u => rnd.Next()).Take(12).ToList();

            if (urlUsuarios != null)
            {
                foreach (var item in urlUsuarios)
                {
                    var dt = new MyWebClient().DownloadString(item);
                    var dc = new HtmlAgilityPack.HtmlDocument();
                    dc.LoadHtml(dt);
                    var nd = dc.DocumentNode.SelectNodes("//a").Where(d => d.GetAttributeValue("class", "").Contains(
                            "rf-project-cover__image-container js-project-cover-image-link js-project-link"));

                    imagens.Add(nd.FirstOrDefault().OuterHtml);
                    if (imagens.Count >= 12)
                    {
                        break;
                    }
                }
            }
            
            return imagens;
        }

        public List<string> ExtraiHAPBehance()
        {
            List<string> imagens = new List<string>();

            List<string> imagensEmb = new List<string>();

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

            var rnd = new Random();
            imagensEmb = imagens.OrderBy(item => rnd.Next()).ToList();

            return imagensEmb;
        }

        public static Tuple<List<string>, List<string>, List<string>, List<string>> ExtraiPortfolioBehance(string urlPerfilUsu)
        {
            List<string> imagens = new List<string>();
            List<string> links = new List<string>();
            List<string> nomes = new List<string>();
            List<string> tipos = new List<string>();

            //var url = "https://www.behance.net/brenoGomesSousa";

            var data = new MyWebClient().DownloadString(urlPerfilUsu);

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