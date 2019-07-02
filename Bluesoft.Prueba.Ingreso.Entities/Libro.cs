using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bluesoft.Prueba.Ingreso.Entities
{
    [Table("Libros")]
    public class Libro
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [MaxLength(30)]
        public string NombreLibro { get; set; }

        [ForeignKey("IdAutor")]
        public Autor Autor { get; set; }
        public int IdAutor { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria Categoria { get; set; }
        public int IdCategoria { get; set; }

        [MaxLength(13)]
        public string Isbn { get; set; }

    }
}
