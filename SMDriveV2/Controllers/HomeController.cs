using Microsoft.AspNet.Identity;
using SMDriveV2.Models;
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
            var imgs = ext.MontaMosaico();

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
            var usuarios = (new ApplicationDbContext()).Users.ToList().OrderBy(o => o.UserName);

            ViewBag.Usuarios = usuarios;

            return View();
        }

        public ActionResult PortfolioUsuario()
        {
            ViewBag.Message = "Em Construção...";

            string userId = User.Identity.GetUserId();
            if (!String.IsNullOrEmpty(userId))
            {
                var user = (new ApplicationDbContext()).Users.FirstOrDefault(s => s.Id == userId);
                var projetos = Extrator.ExtraiPortfolioBehance(user.UrlBehance);

                ViewBag.imagens = projetos.Item1;
                ViewBag.links = projetos.Item2;
                ViewBag.nomes = projetos.Item3;
                ViewBag.tipos = projetos.Item4;
            }
            
            return View();
        }

        public ActionResult Perfil()
        {
            ViewBag.Message = "Em Construção...";

            return View();
        }

        public ActionResult Aptidoes()
        {
            ViewBag.Message = "Em Construção...";

            return View();
        }
    }
}