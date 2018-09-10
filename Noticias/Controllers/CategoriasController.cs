using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Noticias.Data;
using Noticias.Models.CategoriasViewModels;
using Noticias.Models;
using Microsoft.EntityFrameworkCore;

namespace Noticias.Controllers
{
    public class CategoriasController : Controller
    {
        private DbNoticias db;
        public CategoriasController(DbNoticias db)
        {
            this.db = db;
        }
        //Torna o programa assincrono async Task<IActionResult>
        public IActionResult Index()
        {
            var categorias = db.Categorias.OrderBy(c => c.Descricao);
            return View(categorias);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoriaViewModel model)
        {
            if (ModelState.IsValid)
            {
                Categoria categoria = new Categoria()
                {
                    Descricao = model.Descricao,
                };
                db.Add(categoria);
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
