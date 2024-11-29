using System.ComponentModel.DataAnnotations;

namespace ApiPaplria.Models
{
    public class Pago
    {
        [Key]
        public int IdPago { get; set; }
        public int IdVenta { get; set; }
        public required string TipoPago { get; set; } // Dinero o Puntos
        public decimal MontoTotal { get; set; }
        public  required string CuentaOrigen { get; set; }
        public required string CuentaDestino { get; set; }
        public required string EstadoTrans { get; set; }
    }

}
