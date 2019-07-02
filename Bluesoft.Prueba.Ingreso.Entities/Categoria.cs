using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bluesoft.Prueba.Ingreso.Entities
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string Descripcion { get; set; }
        
    }
}
