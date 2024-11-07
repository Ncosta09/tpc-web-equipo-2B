using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PedidosSalon
    {

        public void CerrarPedido(int idPedido)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE Pedidos SET Estado = 1, FechaCierre = GETDATE() WHERE IdPedido = @IdPedido and Estado = 0;");
                datos.setParametro("@IdPedido", idPedido);
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

        public void RegistrarPedido(int idMesa, int idUsuario, DateTime fechaInicio, int estado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("INSERT INTO Pedidos (IdMesa, IdUsuario, Estado, FechaInicio) VALUES (@IdMesa, @IdUsuario, @Estado, @FechaInicio );");
                datos.setParametro("@IdMesa", idMesa);
                datos.setParametro("@IdUsuario", idUsuario);
                datos.setParametro("@Estado", estado); // PÓDRIA PASAR DIRECTAMENTE COMO VALUE EL 0 EN ESTADO PARA NO DECLARLO EN EL BACK
                datos.setParametro("@FechaInicio", fechaInicio); //MISMO CASO QUE EVENTO CON GETDATE()
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

        public void RegistrarDetallePedido(int idPedido, int idInsumo, int cantidad, decimal precioUnitario, decimal precioTotal)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setConsulta("INSERT INTO DetallePedidos (IdPedido, IdInsumo, Cantidad, PrecioUnitario, PrecioTotal) VALUES (@IdPedido, @IdInsumo, @Cantidad, @PrecioUnitario, @PrecioTotal);");
                datos.setParametro("@IdPedido", idPedido);
                datos.setParametro("@IdInsumo", idInsumo);
                datos.setParametro("@Cantidad", cantidad);
                datos.setParametro("@PrecioUnitario", precioUnitario);
                datos.setParametro("@PrecioTotal", precioTotal);
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

        public List<DetallePedido> ObtenerDetallesPedido(int idPedido)
        {
            List<DetallePedido> detalles = new List<DetallePedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT dp.IdDetalle, dp.IdInsumo, dp.Cantidad, dp.PrecioUnitario, dp.PrecioTotal, i.Nombre AS InsumoNombre " + "FROM DetallePedidos dp " + "INNER JOIN Insumos i ON dp.IdInsumo = i.IdInsumo " + "WHERE dp.IdPedido = @IdPedido;");
                datos.setParametro("@IdPedido", idPedido);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetallePedido detalle = new DetallePedido
                    {
                        ID = datos.Lector["IdDetalle"] is DBNull ? 0 : Convert.ToInt32(datos.Lector["IdDetalle"]),
                        Cantidad = datos.Lector["Cantidad"] is DBNull ? 0 : Convert.ToInt32(datos.Lector["Cantidad"]),
                        PrecioUnitario = datos.Lector["PrecioUnitario"] is DBNull ? 0 : Convert.ToDecimal(datos.Lector["PrecioUnitario"]),
                        PrecioTotal = datos.Lector["PrecioTotal"] is DBNull ? 0 : Convert.ToDecimal(datos.Lector["PrecioTotal"]),
                        Insumo = new Insumo
                        {
                            ID = datos.Lector["IdInsumo"] is DBNull ? 0 : Convert.ToInt32(datos.Lector["IdInsumo"]),
                            Nombre = datos.Lector["InsumoNombre"] is DBNull ? string.Empty : Convert.ToString(datos.Lector["InsumoNombre"])
                        }
                    };
                    detalles.Add(detalle);
                }

                return detalles;
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

        public Pedido ObtenerPedido(int idPedido)
        {
            Pedido pedido = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT p.IDPedido, p.Estado, p.FechaInicio, p.FechaCierre, p.IdMesa, p.IdUsuario, " + "m.NumeroMesa, u.Nombre AS UsuarioNombre " + "FROM Pedidos p " + "INNER JOIN Mesas m ON p.IdMesa = m.IdMesa " + "INNER JOIN Usuarios u ON p.IdUsuario = u.IdUsuario " + "WHERE p.IDPedido = @IdPedido;");
                datos.setParametro("@IdPedido", idPedido);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    pedido = new Pedido
                    {
                        ID = datos.Lector["IDPedido"] is DBNull ? 0 : Convert.ToInt32(datos.Lector["IDPedido"]),
                        Estado = datos.Lector["Estado"] is DBNull ? 0 : Convert.ToInt32(datos.Lector["Estado"]),
                        FechaInicio = datos.Lector["FechaInicio"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(datos.Lector["FechaInicio"]),
                        FechaCierre = datos.Lector["FechaCierre"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(datos.Lector["FechaCierre"]),
                        Mesa = new Mesa { NumeroMesa = datos.Lector["NumeroMesa"] is DBNull ? 0 : Convert.ToInt32(datos.Lector["NumeroMesa"]) },
                        Usuario = new Usuario { Nombre = datos.Lector["UsuarioNombre"] is DBNull ? string.Empty : Convert.ToString(datos.Lector["UsuarioNombre"]) }
                    };
                }

                return pedido;
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
        public int ObtenerPedidoActivoPorMesa(int idMesa)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT IDPedido FROM Pedidos WHERE IdMesa = @IdMesa AND Estado = 0");
                datos.setParametro("@IdMesa", idMesa);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return Convert.ToInt32(datos.Lector["IDPedido"]);
                }
                return -1;
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
