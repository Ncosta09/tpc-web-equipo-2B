using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio
{
    public class Mesa
    {
        public int ID { get; set; }
        public int numeroMesa { get; set; }
        public Mesero meseroAsignado { get; set; }
        public Pedido pedidoActual { get; set; }
    }
}