using Dominio;
using Negocio;
using System.Collections.Generic;
using System;

public class MeseroXMesa
{
    public List<Usuario> listarMesero()
    {
        List<Usuario> lista = new List<Usuario>();
        AccesoDatos datos = new AccesoDatos();

        try
        {
            datos.setConsulta("SELECT IdUsuario, CONCAT(Nombre, ' ', Apellido) AS NombreCompleto FROM Usuarios WHERE IdRol = 2;");
            datos.ejecutarLectura();

            while (datos.Lector.Read())
            {
                Usuario usu = new Usuario();
                usu.ID = (int)datos.Lector["IdUsuario"];
                usu.Nombre = datos.Lector["NombreCompleto"].ToString();
                lista.Add(usu);
            }

            return lista;
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
