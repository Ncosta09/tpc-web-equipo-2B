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
    }
}
