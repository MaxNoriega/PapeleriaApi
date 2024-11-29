using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPaplria.Models
{
    public class DetalleVenta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // No es una columna Identity
        public int IdDetalle { get; set; }
        public int IdVenta { get; set; }
        public int IdPro { get; set; }
        public decimal Subtotal { get; set; }
        public int CantidadProd { get; set; }

        // Navegación
        public required Venta Venta { get; set; }
        public required Producto Producto { get; set; }
    }

}
