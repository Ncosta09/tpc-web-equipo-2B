using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DatosReportes
    {
        public Insumo productoMasVendido()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //Consulta a la DB ¬
                datos.setConsulta("SELECT TOP 1 I.Nombre AS Nombre, SUM(D.Cantidad) AS TotalVendido FROM DetallePedidos D INNER JOIN Insumos I ON D.IdInsumo = I.IdInsumo GROUP BY I.IdInsumo, I.Nombre ORDER BY TotalVendido DESC;");
                datos.ejecutarLectura();

                Insumo insumo = new Insumo();

                while (datos.Lector.Read())
                {
                    insumo.Nombre = datos.Lector["Nombre"] is DBNull ? "Sin Nombre" : (string)datos.Lector["Nombre"];
                    insumo.TotalVendido = datos.Lector["TotalVendido"] is DBNull ? 0 : (int)datos.Lector["TotalVendido"];

                }

                return insumo;
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

        public Insumo productoMenosVendido()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //Consulta a la DB ¬
                datos.setConsulta("SELECT TOP 1 I.Nombre AS Nombre, SUM(D.Cantidad) AS TotalVendido FROM DetallePedidos D INNER JOIN Insumos I ON D.IdInsumo = I.IdInsumo GROUP BY I.IdInsumo, I.Nombre ORDER BY TotalVendido ASC;");
                datos.ejecutarLectura();

                Insumo insumo = new Insumo();

                while (datos.Lector.Read())
                {
                    insumo.Nombre = datos.Lector["Nombre"] is DBNull ? "Sin Nombre" : (string)datos.Lector["Nombre"];
                    insumo.TotalVendido = datos.Lector["TotalVendido"] is DBNull ? 0 : (int)datos.Lector["TotalVendido"];

                }

                return insumo;
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

        public List<Insumo> ObtenerTop3MasVendidos()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Insumo> insumos = new List<Insumo>();

            try
            {
                datos.setConsulta("SELECT TOP 3 I.Nombre AS Nombre, SUM(D.Cantidad) AS TotalVendido FROM DetallePedidos D INNER JOIN Insumos I ON D.IdInsumo = I.IdInsumo GROUP BY I.IdInsumo, I.Nombre ORDER BY TotalVendido DESC;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Insumo insumo = new Insumo();
                    insumo.Nombre = datos.Lector["Nombre"] is DBNull ? "Sin Nombre" : (string)datos.Lector["Nombre"];
                    insumo.TotalVendido = datos.Lector["TotalVendido"] is DBNull ? 0 : (int)datos.Lector["TotalVendido"];

                    insumos.Add(insumo);
                }

                return insumos;
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

        public List<Insumo> ObtenerTop3MenosVendidos()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Insumo> insumos = new List<Insumo>();

            try
            {
                datos.setConsulta("SELECT TOP 3 I.Nombre AS Nombre, SUM(D.Cantidad) AS TotalVendido FROM DetallePedidos D INNER JOIN Insumos I ON D.IdInsumo = I.IdInsumo GROUP BY I.IdInsumo, I.Nombre ORDER BY TotalVendido ASC;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Insumo insumo = new Insumo();
                    insumo.Nombre = datos.Lector["Nombre"] is DBNull ? "Sin Nombre" : (string)datos.Lector["Nombre"];
                    insumo.TotalVendido = datos.Lector["TotalVendido"] is DBNull ? 0 : (int)datos.Lector["TotalVendido"];

                    insumos.Add(insumo);
                }

                return insumos;
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
        public Usuario meseroDelMes()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //Consulta a la DB ¬
                datos.setConsulta("SELECT TOP 1 U.Nombre AS Nombre, U.Apellido AS Apellido, U.Imagen AS ImagenMesero, COUNT(P.IDPedido) AS MesasTotal FROM Usuarios AS U INNER JOIN Pedidos AS P ON P.IdUsuario = U.IdUsuario GROUP BY U.Nombre, U.Apellido, U.Imagen ORDER BY MesasTotal DESC;");
                datos.ejecutarLectura();

                Usuario mesero = new Usuario();

                while (datos.Lector.Read())
                {
                    mesero.Nombre = datos.Lector["Nombre"] is DBNull ? "Sin Nombre" : (string)datos.Lector["Nombre"];
                    mesero.Apellido = datos.Lector["Apellido"] is DBNull ? "Sin Apellido" : (string)datos.Lector["Apellido"];
                    mesero.Imagen = datos.Lector["ImagenMesero"] is DBNull ? "https://cdhcolima.org.mx/wp-content/uploads/2016/11/user.png" : (string)datos.Lector["ImagenMesero"];
                    mesero.MesasAtendidas = datos.Lector["MesasTotal"] is DBNull ? 0 : (int)datos.Lector["MesasTotal"];
                }

                return mesero;
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
