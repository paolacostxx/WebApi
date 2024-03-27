using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FilmeController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: Filme
        public async Task<ActionResult> Index()
        {
            return View(await db.Filme.ToListAsync());
        }

        // GET: Filme/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filme filme = await db.Filme.FindAsync(id);
            if (filme == null)
            {
                return HttpNotFound();
            }
            return View(filme);
        }

        // GET: Filme/Create
        public ActionResult Create()
        {
            return View();
        }  
        
        // GET: Filme/Api
        public ActionResult Api()
        {
            return View();
        }

        // POST: Filme/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Codigo,Nome,Ano_Lancamento,Genero,Produtora")] Filme filme)
        {
            if (ModelState.IsValid)
            {
                db.Filme.Add(filme);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(filme);
        }

        // GET: Filme/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filme filme = await db.Filme.FindAsync(id);
            if (filme == null)
            {
                return HttpNotFound();
            }
            return View(filme);
        }

        // POST: Filme/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Codigo,Nome,Ano_Lancamento,Genero,Produtora")] Filme filme)
        {
            if (ModelState.IsValid)
            {
                db.Entry(filme).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(filme);
        }

        // GET: Filme/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filme filme = await db.Filme.FindAsync(id);
            if (filme == null)
            {
                return HttpNotFound();
            }
            return View(filme);
        }

        // POST: Filme/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Filme filme = await db.Filme.FindAsync(id);
            db.Filme.Remove(filme);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
