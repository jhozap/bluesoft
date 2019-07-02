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
    [Route("api/Autores")]
    public class AutoresController : Controller
    {
        private readonly BluesoftPruebaDBContext _context;

        public AutoresController(BluesoftPruebaDBContext context)
        {
            _context = context;
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            
        }

        // GET: api/Autores
        [HttpGet]
        public IEnumerable<Autor> GetAutores()
        {
            return _context.Autores;
        }

        // GET: api/Autores/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAutor([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var autor = await _context.Autores.SingleOrDefaultAsync(m => m.Id == id);

                if (autor == null)
                {
                    return NotFound();
                }

                return Ok(autor);
            }
            catch (Exception ex)
            {
                var logger = LogManager.GetLogger(typeof(Program));
                logger.Error(ex.Message);
                return BadRequest();
            }
            
        }

        // PUT: api/Autores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutor([FromRoute] int id, [FromBody] Autor autor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != autor.Id)
                {
                    return BadRequest();
                }

                _context.Entry(autor).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorExists(id))
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

        // POST: api/Autores
        [HttpPost]
        public async Task<IActionResult> PostAutor([FromBody] Autor autor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Autores.Add(autor);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAutor", new { id = autor.Id }, autor);
            }
            catch (Exception ex)
            {
                var logger = LogManager.GetLogger(typeof(Program));
                logger.Error(ex.Message);
                return BadRequest();
            }
            
        }

        // DELETE: api/Autores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutor([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var autor = await _context.Autores.SingleOrDefaultAsync(m => m.Id == id);
                if (autor == null)
                {
                    return NotFound();
                }

                _context.Autores.Remove(autor);
                await _context.SaveChangesAsync();

                return Ok(autor);
            }
            catch (Exception ex)
            {
                var logger = LogManager.GetLogger(typeof(Program));
                logger.Error(ex.Message);
                return BadRequest();
            }
            
        }

        private bool AutorExists(int id)
        {
            try
            {
                return _context.Autores.Any(e => e.Id == id);
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