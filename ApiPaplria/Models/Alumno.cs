using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPaplria.Models
{
    public class Alumno
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // No es una columna Identity
        public int NumCtrl { get; set; }
        public required string Nombre { get; set; }
    }

}
