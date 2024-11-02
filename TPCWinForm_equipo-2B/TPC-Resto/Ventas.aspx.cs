using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Negocio;

namespace TPC_Resto
{
    public partial class Ventas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                // Consulta SQL para obtener las ventas por mesa
                string consulta = @"SELECT M.NumeroMesa, SUM(V.Total) AS Total
                                    FROM Ventas V
                                    INNER JOIN Mesas M ON V.IdMesa = M.IdMesa
                                    GROUP BY M.NumeroMesa";

                datos.setConsulta(consulta);
                datos.ejecutarLectura();

                var listaVentas = new List<Venta>();

                while (datos.Lector.Read())
                {
                    // Crear objeto Venta con el detalle por mesa
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
                // Manejo de errores
                Response.Write("<script>alert('Error al cargar ventas: " + ex.Message + "');</script>");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private List<DetalleVenta> ObtenerDetalleVenta(int numeroMesa)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Consulta SQL para obtener el detalle de cada venta por mesa
                string consulta = @"SELECT P.Nombre AS Producto, DV.Cantidad, DV.PrecioUnitario, (DV.Cantidad * DV.PrecioUnitario) AS TotalItem
                                    FROM DetalleVentas DV
                                    INNER JOIN Productos P ON DV.IdProducto = P.IdProducto
                                    INNER JOIN Ventas V ON DV.IdVenta = V.IdVenta
                                    INNER JOIN Mesas M ON V.IdMesa = M.IdMesa
                                    WHERE M.NumeroMesa = @NumeroMesa";

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
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Clases para modelar los datos de ventas y detalles
        public class Venta
        {
            public int NumeroMesa { get; set; }
            public decimal Total { get; set; }
            public List<DetalleVenta> DetalleVenta { get; set; }
        }

        public class DetalleVenta
        {
            public string Producto { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal TotalItem { get; set; }
        }
    }
}

