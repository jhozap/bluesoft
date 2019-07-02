using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bluesoft.Prueba.Ingreso.DataAccess;
using Bluesoft.Prueba.Ingreso.Entities;
using log4net;
using System.Reflection;
using log4net.Config;
using System.IO;
using Microsoft.AspNetCore.Cors;

namespace Bluesoft.Prueba.Ingreso.Api.Controllers
{
    [EnableCors("CorsNone")]
    [Produces("application/json")]
    [Route("api/Categorias")]
    public class CategoriasController : Controller
    {
        private readonly BluesoftPruebaDBContext _context;

        public CategoriasController(BluesoftPruebaDBContext context)
        {
            _context = context;
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        // GET: api/Categorias
        [HttpGet]
        public IEnumerable<Categoria> GetCategorias()
        {
            return _context.Categorias;            
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoria([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var categoria = await _context.Categorias.SingleOrDefaultAsync(m => m.Id == id);

                if (categoria == null)
                {
                    return NotFound();
                }

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                var logger = LogManager.GetLogger(typeof(Program));
                logger.Error(ex.Message);
                return BadRequest();
            }
            
        }

        // PUT: api/Categorias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria([FromRoute] int id, [FromBody] Categoria categoria)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != categoria.Id)
                {
                    return BadRequest();
                }

                _context.Entry(categoria).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok("Ok");
            }
            catch (Exception ex)
            {
                var logger = LogManager.GetLogger(typeof(Program));
                logger.Error(ex.Message);
                return BadRequest();
            }
            
        }

        // POST: api/Categorias
        [HttpPost]
        public async Task<IActionResult> PostCategoria([FromBody] Categoria categoria)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCategoria", new { id = categoria.Id }, categoria);
            }
            catch (Exception ex)
            {
                var logger = LogManager.GetLogger(typeof(Program));
                logger.Error(ex.Message);
                return BadRequest();
            }
            
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var categoria = await _context.Categorias.SingleOrDefaultAsync(m => m.Id == id);
                if (categoria == null)
                {
                    return NotFound();
                }

                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                var logger = LogManager.GetLogger(typeof(Program));
                logger.Error(ex.Message);
                return BadRequest();
            }
        }

        private bool CategoriaExists(int id)
        {
            try
            {
                return _context.Categorias.Any(e => e.Id == id);
            }
            catch (Exception ex)
            {
                var logger = LogManager.GetLogger(typeof(Program));
                logger.Error(ex.Message);
                return false;
            }
        } 
    }
}