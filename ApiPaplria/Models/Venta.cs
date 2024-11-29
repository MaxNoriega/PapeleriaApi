using System.ComponentModel.DataAnnotations;
using static ApiPaplria.Context.AppDBContext;

namespace ApiPaplria.Models
{
    public class Venta
    {
        [Key]
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public decimal PtsGenerados { get; set; }
        public required Alumno Cliente { get; set; }
        public required ICollection<DetalleVenta> Detalles { get; set; }
    }


}