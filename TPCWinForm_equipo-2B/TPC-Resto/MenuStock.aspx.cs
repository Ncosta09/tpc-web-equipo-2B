using System;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_Resto
{
    public partial class MenuStock : System.Web.UI.Page
    {
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
                string consulta = "SELECT Nombre, ImagenURL, Stock, Precio FROM Insumos";
                datos.setConsulta(consulta);
                datos.ejecutarLectura();

                carouselItems.InnerHtml = string.Empty; // Limpiar contenido anterior
                bool firstItem = true;

                while (datos.Lector.Read())
                {
                    var nombre = datos.Lector["Nombre"].ToString();
                    var imagenUrl = datos.Lector["ImagenURL"].ToString();
                    var stock = datos.Lector["Stock"].ToString();
                    var precio = Convert.ToDecimal(datos.Lector["Precio"]).ToString("C2");

                    string activeClass = firstItem ? "active" : "";
                    firstItem = false;

                    carouselItems.InnerHtml += $@"
                        <div class='carousel-item {activeClass}'>
                            <img src='{imagenUrl}' alt='{nombre}' class='d-block w-100'>
                            <div class='carousel-caption'>
                                <h5>{nombre}</h5>
                                <p>Stock: {stock}</p>
                                <p>Precio: {precio}</p>
                            </div>
                        </div>";
                }
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
    }
}