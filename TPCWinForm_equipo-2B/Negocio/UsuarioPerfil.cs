using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Negocio
{
    public class UsuarioPerfil
    {
        public Usuario perfilUsuario(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;

            AccesoDatos datos = new AccesoDatos();
            Usuario perfil = null;

            try
            {
                if (usuario != null)
                {
                    //Consulta a la DB ¬
                    datos.setConsulta("SELECT IdUsuario, Nombre, Apellido, DNI, Correo, Imagen FROM Usuarios WHERE IdUsuario = @Id");
                    datos.setParametro("@Id", usuario.ID);
                    datos.ejecutarLectura();

                    perfil = new Usuario();

                    while (datos.Lector.Read())
                    {
                        if (perfil.ID == 0)
                        {
                            perfil.ID = (int)datos.Lector["IdUsuario"];
                            perfil.Nombre = datos.Lector["Nombre"] is DBNull ? "Sin Nombre" : (string)datos.Lector["Nombre"];
                            perfil.Apellido = datos.Lector["Apellido"] is DBNull ? "Sin Apellido" : (string)datos.Lector["Apellido"];
                            perfil.Dni = datos.Lector["DNI"] is DBNull ? "Sin DNI" : (string)datos.Lector["DNI"];
                            perfil.Email = datos.Lector["Correo"] is DBNull ? "Sin Correo" : (string)datos.Lector["Correo"];
                            perfil.Imagen = datos.Lector["Imagen"] is DBNull ? "https://cdhcolima.org.mx/wp-content/uploads/2016/11/user.png" : (string)datos.Lector["Imagen"];

                        }

                    }

                }
                    
                return perfil;
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
