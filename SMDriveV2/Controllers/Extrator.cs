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
    }
}