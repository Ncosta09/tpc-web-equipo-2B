using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio
{
    public class Usuario
    {
        public int id { get; set; }
        public string name { get; set; }
        public string contrasenia { get; set; }
        public string rol { get; set; }
    }
}