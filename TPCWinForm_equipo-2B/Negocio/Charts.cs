using Dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Charts
    {
        //GANANCIAS DIARIAS
        public List<Pedido> ObtenerGananciasDiarias()
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT CONVERT(DATE, p.FechaCierre) AS Fecha, SUM(dp.PrecioTotal) AS GananciaDiaria FROM Pedidos p INNER JOIN DetallePedidos dp ON p.IDPedido = dp.IdPedido WHERE p.Estado = 1 GROUP BY CONVERT(DATE, p.FechaCierre) ORDER BY Fecha;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido ganancia = new Pedido();
                    ganancia.FechaCierre = (DateTime)datos.Lector["Fecha"];
                    ganancia.PrecioTotalMesa = (decimal)datos.Lector["GananciaDiaria"];

                    lista.Add(ganancia);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las ganancias diarias.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        //GANACIAS MENSUALES
        public List<Pedido> ObtenerGananciasMensuales()
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT YEAR(p.FechaCierre) AS Anio, MONTH(p.FechaCierre) AS Mes, SUM(dp.PrecioTotal) AS GananciaMensual FROM Pedidos p INNER JOIN DetallePedidos dp ON p.IDPedido = dp.IdPedido WHERE p.Estado = 1 GROUP BY YEAR(p.FechaCierre), MONTH(p.FechaCierre) ORDER BY Anio, Mes;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido ganancia = new Pedido();
                    ganancia.FechaCierre = new DateTime(Convert.ToInt32(datos.Lector["Anio"]), Convert.ToInt32(datos.Lector["Mes"]), 1);
                    ganancia.PrecioTotalMesa = Convert.ToDecimal(datos.Lector["GananciaMensual"]);

                    lista.Add(ganancia);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las ganancias mensuales.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        //GANACIAS ANUALES
        public List<Pedido> ObtenerGananciasAnuales()
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT YEAR(p.FechaCierre) AS Anio, SUM(dp.PrecioTotal) AS GananciaAnual FROM Pedidos p INNER JOIN DetallePedidos dp ON p.IDPedido = dp.IdPedido WHERE p.Estado = 1 GROUP BY YEAR(p.FechaCierre) ORDER BY Anio;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido ganancia = new Pedido();
                    ganancia.FechaCierre = new DateTime(Convert.ToInt32(datos.Lector["Anio"]), 1, 1);
                    ganancia.PrecioTotalMesa = Convert.ToDecimal(datos.Lector["GananciaAnual"]);

                    lista.Add(ganancia);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las ganancias anuales.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
