using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Venta
    {
        public int NumeroMesa { get; set; }
        public decimal Total { get; set; }
        public List<DetalleVenta> DetalleVenta { get; set; }

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
    }
}
