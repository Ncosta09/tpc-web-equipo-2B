﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetallePedido
    {
        public int ID { get; set; }
        public int Cantidad { get; set; }
        public Pedido Pedido { get; set; }
        public Insumo Insumo { get; set; }
    }
}
