using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bluesoft.Prueba.Ingreso.DataAccess;
using Bluesoft.Prueba.Ingreso.Entities;
using log4net.Config;
using log4net;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Cors;

namespace Bluesoft.Prueba.Ingreso.Api.Controllers
{
    [EnableCors("CorsNone")]
    [Produces("application/json")]
    [Route("api/Libros")]
    public class LibrosController : Controller
    {
        private readonly BluesoftPruebaDBContext _context;

        public LibrosController(BluesoftPruebaDBContext context)
        {
            _context = context;
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        // GET: api/Libros
        [HttpGet]
        public IEnumerable<Libro> GetLibros()
        {
            //return _context.Libros.Include(a => a.Autor);
            return _context.Libros;
        }

        // GET: api/Libros/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLibro([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var libro = await _context.Libros.SingleOrDefaultAsync(m => m.Id == id);

                if (libro == null)
                {
                    return NotFound();
                }

                return Ok(libro);
            }
            catch (Exception ex)
            {
                var logger = LogManager.GetLogger(typeof(Program));
                logger.Error(ex.Message);
                return BadRequest();
            }
        }

        // PUT: api/Libros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibro([FromRoute] int id, [FromBody] Libro libro)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != libro.Id)
                {
                    return BadRequest();
                }

                _context.Entry(libro).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(id))
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

        // POST: api/Libros
        [HttpPost]
        public async Task<IActionResult> PostLibro([FromBody] Libro libro)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Libros.Add(libro);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetLibro", new { id = libro.Id }, libro);
            }
            catch (Exception ex)
            {
                var logger = LogManager.GetLogger(typeof(Program));
                logger.Error(ex.Message);
                return BadRequest();                
            }
            
        }

        // DELETE: api/Libros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var libro = await _context.Libros.SingleOrDefaultAsync(m => m.Id == id);
                if (libro == null)
                {
                    return NotFound();
                }

                _context.Libros.Remove(libro);
                await _context.SaveChangesAsync();

                return Ok(libro);
            }
            catch (Exception ex)
            {
                var logger = LogManager.GetLogger(typeof(Program));
                logger.Error(ex.Message);
                return BadRequest();
            }
            
        }

        private bool LibroExists(int id)
        {
            try
            {
                return _context.Libros.Any(e => e.Id == id);
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