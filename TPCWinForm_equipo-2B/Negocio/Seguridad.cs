using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public static class Seguridad
    {
        public static bool sesionIniciada(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;

            if (usuario != null && usuario.ID != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool esAdmin(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;

            if (usuario != null && usuario.Rol != null && usuario.Rol.Descripcion == "Manager")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
