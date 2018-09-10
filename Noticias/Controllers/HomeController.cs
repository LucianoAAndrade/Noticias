using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Noticias.Data;
using Noticias.Models;
using Noticias.Models.HomeViewModel;

namespace Noticias.Controllers
{
    public class HomeController : Controller
    {
        DbNoticias db;

        public HomeController(DbNoticias db)
        {
            this.db = db;
        }
        public IActionResult Index(int? filtro = null)
        {
            IndexViewModel model = new IndexViewModel();
            model.Categorias = db.Categorias.OrderBy(c => c.Descricao);
            model.Banner = db.Noticias.Where(n => n.Destaque).OrderByDescending(n => n.DataPublicacao).Take(5);

            if (filtro == null)
            {
                model.UltimasNoticias = db.Noticias.OrderByDescending(n => n.DataPublicacao).Take(10);
            }
            else
            {
                model.UltimasNoticias = db.Noticias.Where(n => n.CategoriaId == filtro).OrderByDescending(n => n.DataPublicacao).Take(10);
            }
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
