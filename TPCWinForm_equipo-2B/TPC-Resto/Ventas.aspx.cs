using System;
using System.Collections.Generic;
using Dominio;
using Negocio;

namespace TPC_Resto
{
    public partial class Ventas : System.Web.UI.Page
    {
        public List<Pedido> ListaPedidos { get; set; }
        private int currentPage
        {
            get
            {
                return ViewState["currentPage"] != null ? (int)ViewState["currentPage"] : 1;
            }
            set
            {
                ViewState["currentPage"] = value;
            }
        }

        private int pageSize = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPedidos();
            }
        }

        private void CargarPedidos()
        {
            AccesoDatos datos = new AccesoDatos();
            ListaPedidos = new List<Pedido>();
            DateTime? fechaInicio = null;

            // Si se selecciona una fecha la asigna al filtro
            if (DateTime.TryParse(txtFechaInicio.Value, out DateTime fecha))
            {
                fechaInicio = fecha;
            }

            try
            {
                // filtro por fecha
                string query = @"
                    SELECT P.IDPedido, M.NumeroMesa, P.FechaInicio, P.PrecioTotalMesa
                    FROM Pedidos AS P
                    INNER JOIN Mesas AS M ON P.IdMesa = M.IdMesa
                    WHERE P.Estado = 1";

                if (fechaInicio.HasValue)
                {
                    query += " AND P.FechaInicio >= @FechaInicio";
                }

                query += @"
                    ORDER BY P.FechaInicio
                    OFFSET @skip ROWS FETCH NEXT @pageSize ROWS ONLY";

                
                datos.setConsulta(query);
                datos.setParametro("@skip", (currentPage - 1) * pageSize);
                datos.setParametro("@pageSize", pageSize);

                if (fechaInicio.HasValue)
                {
                    datos.setParametro("@FechaInicio", fechaInicio.Value);
                }

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Pedido pedido = new Pedido
                    {
                        ID = (int)datos.Lector["IDPedido"],
                        Mesa = new Mesa
                        {
                            NumeroMesa = (int)datos.Lector["NumeroMesa"]
                        },
                        FechaInicio = (DateTime)datos.Lector["FechaInicio"],
                        PrecioTotalMesa = (decimal)datos.Lector["PrecioTotalMesa"],
                        DetalleVenta = ObtenerDetallePedido((int)datos.Lector["IDPedido"])
                    };
                    ListaPedidos.Add(pedido);
                }

                rptVentas.DataSource = ListaPedidos;
                rptVentas.DataBind();

                //habilitación de los botone
                btnPrev.Enabled = currentPage > 1;
                btnNext.Enabled = ListaPedidos.Count == pageSize;
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private List<DetalleVenta> ObtenerDetallePedido(int idPedido)
        {
            AccesoDatos datos = new AccesoDatos();
            List<DetalleVenta> detalles = new List<DetalleVenta>();

            try
            {
                datos.setConsulta(@"
                    SELECT I.Nombre AS Producto, DP.Cantidad, DP.PrecioUnitario, DP.PrecioTotal
                    FROM DetallePedidos AS DP
                    INNER JOIN Insumos AS I ON DP.IdInsumo = I.IdInsumo
                    WHERE DP.IdPedido = @IdPedido");
                datos.setParametro("@IdPedido", idPedido);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    DetalleVenta detalle = new DetalleVenta
                    {
                        Producto = (string)datos.Lector["Producto"],
                        Cantidad = (int)datos.Lector["Cantidad"],
                        PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"],
                        TotalItem = (decimal)datos.Lector["PrecioTotal"]
                    };
                    detalles.Add(detalle);
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return detalles;
        }

        //filtro por fecha
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            currentPage = 1; // Reiniciar a la primera página al aplicar el filtro
            CargarPedidos();
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                CargarPedidos();
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            currentPage++;
            CargarPedidos();
        }
    }
}
