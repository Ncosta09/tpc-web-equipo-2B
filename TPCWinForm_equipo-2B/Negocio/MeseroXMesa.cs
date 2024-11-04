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

    public void InsertarMeseroMesa(int idUsuario, int idMesa)
    {
        AccesoDatos datos = new AccesoDatos();

        try
        {
            datos.setConsulta("INSERT INTO UsuariosxMesa (IdUsuario, IdMesa) VALUES (@idUsuario, @idMesa);");
            datos.setParametro("@idUsuario", idUsuario);
            datos.setParametro("@idMesa", idMesa);
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
    public Usuario ObtenerMeseroPorMesa(int idMesa)
    {
        AccesoDatos datos = new AccesoDatos();

        try
        {
            datos.setConsulta("SELECT u.IdUsuario, CONCAT(u.Nombre, ' ', u.Apellido) AS NombreCompleto FROM Usuarios u INNER JOIN UsuariosxMesa um ON u.IdUsuario = um.IdUsuario WHERE um.IdMesa = @idMesa;");
            datos.setParametro("@idMesa", idMesa);
            datos.ejecutarLectura();

            if (datos.Lector.Read())
            {
                Usuario usu = new Usuario();
                usu.ID = (int)datos.Lector["IdUsuario"];
                usu.Nombre = datos.Lector["NombreCompleto"].ToString();
                return usu;
            }
            return null;
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
    public void DeleteMeseroMesa(int idUsuario, int idMesa)
    {
        AccesoDatos datos = new AccesoDatos();

        try
        {
            datos.setConsulta("DELETE FROM UsuariosxMesa WHERE IdMesa = @idMesa and IdUsuario = @idUsuario;");
            datos.setParametro("@idUsuario", idUsuario);
            datos.setParametro("@idMesa", idMesa);
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
