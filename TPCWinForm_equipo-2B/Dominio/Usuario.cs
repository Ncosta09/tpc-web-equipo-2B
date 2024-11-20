using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public string Imagen { get; set; }
        public Rol Rol { get; set; }
        public int MesasAtendidas { get; set; }

    }

}
