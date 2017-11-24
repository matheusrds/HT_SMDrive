using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMDriveV2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Extrator ext = new Extrator();

            var imgs = ext.ExtraiHAPBehance();

            ViewBag.imagem = imgs;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Em Construção...";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Em Construção...";

            return View();
        }

        public ActionResult PortfolioUsuario()
        {
            ViewBag.Message = "Em Construção...";

            var projetos = Extrator.ExtraiPortfolioBehance();

            ViewBag.imagens = projetos.Item1;
            ViewBag.links = projetos.Item2;
            ViewBag.nomes = projetos.Item3;
            ViewBag.tipos = projetos.Item4;

            return View();
        }
    }
}