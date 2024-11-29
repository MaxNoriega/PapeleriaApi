using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPaplria.Models
{
    public class Puntos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // No es una columna Identity
        public int NumCtrl { get; set; } // Id del cliente
        public decimal PuntosDisponibles { get; set; }
    }

}
