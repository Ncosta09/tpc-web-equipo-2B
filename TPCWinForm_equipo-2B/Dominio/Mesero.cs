using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio
{
    public class Mesero
    {
        public int ID { get; set; }
        public string nombre { get; set; }
        List<Mesa> mesasAsignadas { get; set; }
    }
}