using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bluesoft.Prueba.Ingreso.Entities
{
    [Table("Autores")]
    public class Autor
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Nombre { get; set; }

        [MaxLength(30)]
        public string Apellidos { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaNacimiento { get; set; }

    }
}
