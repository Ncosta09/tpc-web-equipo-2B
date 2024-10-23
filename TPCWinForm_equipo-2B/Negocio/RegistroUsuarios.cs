using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class RegistroUsuarios
    {
        public void registroUsuario(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("INSERT INTO Usuarios (Nombre, Apellido, Contraseña, Correo, DNI, IdRol) VALUES (@Nombre, @Apellido, @Contrasenia, @Correo, @DNI, 2);");

                datos.setParametro("@Nombre", nuevo.Nombre);
                datos.setParametro("@Apellido", nuevo.Apellido);
                datos.setParametro("@Contrasenia", nuevo.Contrasenia);
                datos.setParametro("@Correo", nuevo.Email);
                datos.setParametro("@DNI", nuevo.Dni);

                datos.ejecutarAccion();

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
