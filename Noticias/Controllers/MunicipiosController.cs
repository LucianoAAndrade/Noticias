using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Noticias.Data;
using Noticias.Models;

namespace Noticias
{
    public class MunicipiosController : Controller
    {
        //private readonly DBNoticias _context;

        private DbNoticias db;
        public MunicipiosController(DbNoticias db)
        {
            this.db = db;
        }
        //Torna o programa assincrono async Task<IActionResult>
        public IActionResult Index()
        {
            var municipio = db.Municipio.OrderBy(c => c.Nome);
            return View(municipio);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Municipio model)
        {
            if (ModelState.IsValid)
            {
                Municipio municipio = new Municipio()
                {
                    Nome = model.Nome,
                    Uf = model.Uf
                };
                db.Add(municipio);
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

            Municipio municipio = await db.Municipio.SingleOrDefaultAsync(c => c.id == id);

            if (municipio == null)
            {
                return NotFound();
            }

            Municipio model = new Municipio
            {
                id = municipio.id,
                Nome = municipio.Nome,
                Uf = municipio.Uf
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Municipio model)
        {
            if (ModelState.IsValid)
            {
                Municipio municipio = await db.Municipio.SingleOrDefaultAsync(c => c.id == model.id);

                municipio.Nome = model.Nome;
                municipio.Uf = model.Uf;
                db.Update(municipio);
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

            Municipio municipio = await db.Municipio.SingleOrDefaultAsync(c => c.id == id);

            if (municipio == null)
            {
                return NotFound();
            }

            return View(municipio);

        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfimed(int? id)
        {
            Municipio municipio = db.Municipio.Single(m => m.id == id);
            db.Municipio.Remove(municipio);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }      
    }
}
