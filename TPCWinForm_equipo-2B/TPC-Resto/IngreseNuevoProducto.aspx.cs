using System;
using System.Web.UI;
using Negocio;

namespace TPC_Resto
{
    public partial class IngreseNuevoProducto : Page
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
        }

        protected void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtImagenURL.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text))
            {
                Response.Write("<script>alert('Por favor, completa todos los campos');</script>");
                return;
            }

            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "INSERT INTO Insumos (Nombre, ImagenURL, Precio, Stock) VALUES (@Nombre, @ImagenURL, @Precio, @Stock)";
                datos.setConsulta(consulta);
                datos.setParametro("@Nombre", txtNombre.Text);
                datos.setParametro("@ImagenURL", txtImagenURL.Text);
                datos.setParametro("@Precio", decimal.Parse(txtPrecio.Text));
                datos.setParametro("@Stock", int.Parse(txtStock.Text));
                datos.ejecutarAccion();

                // Limpiar los campos del formulario
                txtNombre.Text = "";
                txtImagenURL.Text = "";
                txtPrecio.Text = "";
                txtStock.Text = "";

                // Mostrar mensaje y redirigir automáticamente
                Response.Write("<script>alert('Producto agregado exitosamente'); window.location='MenuStock.aspx';</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al agregar producto: " + ex.Message + "');</script>");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuStock.aspx");
        }
    }
}