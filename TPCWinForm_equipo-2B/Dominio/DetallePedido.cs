using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio
{
    public class DetallePedido
    {
        public int Id { get; set; }
        public Insumo insumo { get; set; }
        public int cantidad { get; set; }
        public decimal precioTotal { get; set; }
    }
}