using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Pedido
    {
        public int ID { get; set; }
        public int Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaCierre { get; set; }
        public Mesa Mesa { get; set; }
        public Usuario Usuario { get; set; }
        // Nueva propiedad para el precio total del pedido
        public decimal PrecioTotalMesa { get; set; }
        // Nueva propiedad para almacenar los detalles de la venta
        public List<DetalleVenta> DetalleVenta { get; set; }
    }
}