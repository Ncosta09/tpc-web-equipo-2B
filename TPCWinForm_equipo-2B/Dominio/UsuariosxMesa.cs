using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class UsuariosxMesa
    {
        public int ID { get; set; }
        public Usuario Usuario { get; set; }
        public Mesa Mesa { get; set; }
    }
}
