using System;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_Resto
{
    public partial class MenuStock : System.Web.UI.Page
    {
        private const int ProductosPorPagina = 8;
        private int paginaActual
        {
            get { return (int)(ViewState["paginaActual"] ?? 1); }
            set { ViewState["paginaActual"] = value; }
        }

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
                CargarInsumos();
            }
        }

        private void CargarInsumos()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Configurar la consulta para paginacaoon
                int offset = (paginaActual - 1) * ProductosPorPagina;
                string consulta = $@"
                    SELECT Nombre, ImagenURL, Stock, Precio
                    FROM Insumos
                    ORDER BY Nombre
                    OFFSET {offset} ROWS
                    FETCH NEXT {ProductosPorPagina} ROWS ONLY";

                datos.setConsulta(consulta);
                datos.ejecutarLectura();

                insumosTableBody.InnerHtml = string.Empty;

                while (datos.Lector.Read())
                {
                    var nombre = datos.Lector["Nombre"].ToString();
                    var imagenUrl = datos.Lector["ImagenURL"].ToString();
                    var stock = datos.Lector["Stock"].ToString();
                    var precio = Convert.ToDecimal(datos.Lector["Precio"]).ToString("C2");

                    insumosTableBody.InnerHtml += $@"
                        <tr>
                            <td><img src='{imagenUrl}' alt='{nombre}' style='width: 50px; height: 50px;'></td>
                            <td>{nombre}</td>
                            <td>{stock}</td>
                            <td>{precio}</td>
                        </tr>";
                }

                lblPaginaActual.Text = "Página " + paginaActual;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar insumos: " + ex.Message + "');</script>");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        protected void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            Response.Redirect("IngreseNuevoProducto.aspx");
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                CargarInsumos();
            }
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            paginaActual++;
            CargarInsumos();
        }
    }
}