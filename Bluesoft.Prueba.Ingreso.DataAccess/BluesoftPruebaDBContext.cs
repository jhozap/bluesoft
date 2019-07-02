using Bluesoft.Prueba.Ingreso.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bluesoft.Prueba.Ingreso.DataAccess
{
    public class BluesoftPruebaDBContext : DbContext
    {
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Libro> Libros { get; set; }

        public BluesoftPruebaDBContext(DbContextOptions options) : base(options) { }

        public BluesoftPruebaDBContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
