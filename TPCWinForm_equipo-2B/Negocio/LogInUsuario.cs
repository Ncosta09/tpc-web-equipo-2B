using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class LogInUsuario
    {
        public bool loguearse(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT U.IdUsuario AS IDUser, R.Descripcion AS DescripcionRol FROM Usuarios AS U INNER JOIN Roles AS R ON R.IdRol = U.IdRol WHERE U.Correo = @Correo AND U.Contraseña = @Contrasenia");

                datos.setParametro("@Correo", usuario.Email);
                datos.setParametro("@Contrasenia", usuario.Contrasenia);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    usuario.ID = (int)datos.Lector["IDUser"];
                    usuario.Rol = new Rol();
                    usuario.Rol.Descripcion = (string)datos.Lector["DescripcionRol"];
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }
    }
}
