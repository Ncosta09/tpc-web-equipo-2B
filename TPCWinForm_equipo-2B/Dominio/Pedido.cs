using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio
{
    public class Pedido
    {
        public int ID { get; set; }
        public DateTime fechaApertura { get; set; }
        public DateTime fechaCierre { get; set; }
        public List<DetallePedido> detallePedido { get; set; }
    }
}