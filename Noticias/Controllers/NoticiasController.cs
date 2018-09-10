using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Noticias.Data;
using Noticias.Models.NoticiasViewModels;
using Noticias.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Noticias.Models.CategoriasViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Noticias.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class NoticiasController : Controller
    {
        private DbNoticias db;
        public NoticiasController(DbNoticias db)
        {
            this.db = db;
        }
        //Torna o programa assincrono async Task<IActionResult>
        public IActionResult Index()
        {
            var noticias = db.Noticias.OrderByDescending(c => c.DataPublicacao);
            return View(noticias);
        }
        [HttpGet]
        public IActionResult Create()
        {
            NoticiaViewModel model = new NoticiaViewModel();
            model.Categorias = new SelectList(db.Categorias.OrderBy(c => c.Descricao), "id", "Descricao");
            model.DataPublicacao = DateTime.Now;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(NoticiaViewModel model)
        {
            if (ModelState.IsValid)
            {
                Noticia noticia = new Noticia()
                {
                    Titulo = model.Titulo,
                    Corpo = model.Corpo,
                    DataPublicacao = model.DataPublicacao,
                    Autor = model.Autor,
                    Destaque = model.Destaque,
                    CategoriaId = model.CategoriaId
                };
                db.Add(noticia);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Categoria categoria = await db.Categorias.SingleOrDefaultAsync(c => c.id == id);

            if (categoria == null)
            {
                return NotFound();
            }

            CategoriaViewModel model = new CategoriaViewModel
            {
                id = categoria.id,
                Descricao = categoria.Descricao
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoriaViewModel model)
        {
            if (ModelState.IsValid)
            {
                Categoria categoria = await db.Categorias.SingleOrDefaultAsync(c => c.id == model.id);

                categoria.Descricao = model.Descricao;
                db.Update(categoria);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Categoria categoria = await db.Categorias.SingleOrDefaultAsync(c => c.id == id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);

        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfimed(int? id)
        {
            Categoria categoria = db.Categorias.Single(m => m.id == id);
            db.Categorias.Remove(categoria);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
