using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FilmesController : ApiController
    {
        private Entities1 db = new Entities1();

        // GET: api/Filmes
        public IQueryable<Filme> GetFilme()
        {
            return db.Filme;
        }

        // GET: api/Filmes/5
        [ResponseType(typeof(Filme))]
        public async Task<IHttpActionResult> GetFilme(int id)
        {
            Filme filme = await db.Filme.FindAsync(id);
            if (filme == null)
            {
                return NotFound();
            }

            return Ok(filme);
        }

        // PUT: api/Filmes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFilme(int id, Filme filme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != filme.Codigo)
            {
                return BadRequest();
            }

            db.Entry(filme).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Filmes
        [ResponseType(typeof(Filme))]
        public async Task<IHttpActionResult> PostFilme(Filme filme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Filme.Add(filme);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = filme.Codigo }, filme);
        }

        // DELETE: api/Filmes/5
        [ResponseType(typeof(Filme))]
        public async Task<IHttpActionResult> DeleteFilme(int id)
        {
            Filme filme = await db.Filme.FindAsync(id);
            if (filme == null)
            {
                return NotFound();
            }

            db.Filme.Remove(filme);
            await db.SaveChangesAsync();

            return Ok(filme);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FilmeExists(int id)
        {
            return db.Filme.Count(e => e.Codigo == id) > 0;
        }
    }
}