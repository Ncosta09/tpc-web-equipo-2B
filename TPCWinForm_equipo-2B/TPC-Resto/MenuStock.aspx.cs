using System;
using System.Data.SqlClient;
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
                string consulta = "SELECT Nombre, ImagenURL, Stock FROM Insumos";
                datos.setConsulta(consulta);
                datos.ejecutarLectura();

                carouselItems.InnerHtml = string.Empty; // Limpiar contenido anterior
                bool firstItem = true;

                while (datos.Lector.Read())
                {
                    var nombre = datos.Lector["Nombre"].ToString();
                    var imagenUrl = datos.Lector["ImagenURL"].ToString();
                    var stock = datos.Lector["Stock"].ToString();

                    string activeClass = firstItem ? "active" : "";
                    firstItem = false;

                    carouselItems.InnerHtml += $@"
                        <div class='carousel-item {activeClass}'>
                            <img src='{imagenUrl}' alt='{nombre}' class='d-block w-100'>
                            <div class='carousel-caption'>
                                <h5 style='color:black; font-weight: bold;'>{nombre}</h5>
                                <p style='color:black; font-weight: bold;'>Stock: {stock}</p>
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

        protected void btnAgregarInsumo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtImagenURL.Text) || string.IsNullOrWhiteSpace(txtStock.Text))
            {
                Response.Write("<script>alert('Por favor, completa todos los campos');</script>");
                return;
            }

            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "INSERT INTO Insumos (Nombre, ImagenURL, Stock) VALUES (@Nombre, @ImagenURL, @Stock)";
                datos.setConsulta(consulta);
                datos.setParametro("@Nombre", txtNombre.Text);
                datos.setParametro("@ImagenURL", txtImagenURL.Text);
                datos.setParametro("@Stock", int.Parse(txtStock.Text));
                datos.ejecutarAccion();

                // Limpiar los campos del formulario
                txtNombre.Text = "";
                txtImagenURL.Text = "";
                txtStock.Text = "";

                // Recargar el carrusel
                CargarInsumos();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al agregar insumo: " + ex.Message + "');</script>");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
