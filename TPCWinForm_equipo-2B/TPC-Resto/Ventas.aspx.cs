using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Negocio;
using Dominio;

namespace TPC_Resto
{
    public partial class Ventas : System.Web.UI.Page
    {
        private int pageIndex = 0;  // Página actual
        private int pageSize = 2;   // Número de registros por página

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.sesionIniciada(Session["usuario"]))
            {
                Response.Redirect("Default.aspx", false);
            }

            if (!Seguridad.esAdmin(Session["usuario"]))
            {
                Response.Redirect("HomeMenu.aspx", false);
            }

            if (!IsPostBack)
            {
                CargarVentas();
            }
        }

        private void CargarVentas()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Consulta SQL para obtener las ventas por mesa con paginación
                string consulta = @"SELECT M.NumeroMesa, SUM(V.Total) AS Total
                                    FROM Ventas V
                                    INNER JOIN Mesas M ON V.IdMesa = M.IdMesa
                                    GROUP BY M.NumeroMesa
                                    ORDER BY M.NumeroMesa
                                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

                // Parámetros de paginación
                datos.setConsulta(consulta);
                datos.setParametro("@Offset", pageIndex * pageSize);  // Calcular el offset según la página actual
                datos.setParametro("@PageSize", pageSize);
                datos.ejecutarLectura();

                var listaVentas = new List<Venta>();

                while (datos.Lector.Read())
                {
                    Venta venta = new Venta();
                    venta.NumeroMesa = (int)datos.Lector["NumeroMesa"];
                    venta.Total = (decimal)datos.Lector["Total"];
                    venta.DetalleVenta = ObtenerDetalleVenta(venta.NumeroMesa);
                    listaVentas.Add(venta);
                }

                rptVentas.DataSource = listaVentas;
                rptVentas.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar ventas: " + ex.Message + "');</script>");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Función optimizada para obtener el detalle de la venta
        private List<DetalleVenta> ObtenerDetalleVenta(int numeroMesa)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"SELECT IP.Nombre AS Producto, DP.Cantidad, DP.PrecioUnitario, 
                                   (DP.Cantidad * DP.PrecioUnitario) AS TotalItem
                                   FROM DetallePedidos DP
                                   INNER JOIN Pedidos P ON DP.IdPedido = P.IDPedido
                                   INNER JOIN Insumos IP ON DP.IdInsumo = IP.IdInsumo
                                   WHERE P.IdMesa = (SELECT IdMesa FROM Mesas WHERE NumeroMesa = @NumeroMesa)";

                datos.setConsulta(consulta);
                datos.setParametro("@NumeroMesa", numeroMesa);
                datos.ejecutarLectura();

                var detalleList = new List<DetalleVenta>();

                while (datos.Lector.Read())
                {
                    DetalleVenta detalle = new DetalleVenta
                    {
                        Producto = datos.Lector["Producto"].ToString(),
                        Cantidad = (int)datos.Lector["Cantidad"],
                        PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"],
                        TotalItem = (decimal)datos.Lector["TotalItem"]
                    };
                    detalleList.Add(detalle);
                }

                return detalleList;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener el detalle de la venta", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Lógica para avanzar a la siguiente página
        protected void btnNext_Click(object sender, EventArgs e)
        {
            pageIndex++;
            CargarVentas();
        }

        // Lógica para retroceder a la página anterior
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (pageIndex > 0)
            {
                pageIndex--;
                CargarVentas();
            }
        }
    }
}
