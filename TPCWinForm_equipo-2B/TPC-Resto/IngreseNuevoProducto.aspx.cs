using System;
using System.Web.Optimization;
using System.Web.UI;
using Negocio;

namespace TPC_Resto
{
    public partial class IngreseNuevoProducto : Page
    {
        private int? IdInsumo // Propiedad para gestionar el ID del insumo
        {
            get
            {
                string idInsumoStr = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(idInsumoStr) && int.TryParse(idInsumoStr, out int id))
                {
                    return id;
                }
                return null;
            }
        }

        private const string ImagenPorDefecto = "~/Content/Images/default-recipe.png"; // Ruta de la imagen por defecto

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
                if (IdInsumo.HasValue)
                {
                    CargarProducto(IdInsumo.Value); // Precargar los datos si se está editando
                }
            }
        }

        private void CargarProducto(int idInsumo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT Nombre, ImagenURL, Precio, Stock FROM Insumos WHERE IdInsumo = @IdInsumo";
                datos.setConsulta(consulta);
                datos.setParametro("@IdInsumo", idInsumo);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    // Precargar los datos en los controles
                    txtNombre.Text = datos.Lector["Nombre"].ToString();
                    txtImagenURL.Text = datos.Lector["ImagenURL"].ToString();
                    txtPrecio.Text = Convert.ToDecimal(datos.Lector["Precio"]).ToString("0.00");
                    txtStock.Text = datos.Lector["Stock"].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Producto no encontrado');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar el producto: " + ex.Message + "');</script>");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        protected void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text))
            {
                string alertScript = "Swal.fire({ icon: 'error', title: 'Oops...', text: 'Por favor, complete todos los campos correctamente.' });";
                ClientScript.RegisterStartupScript(this.GetType(), "NewProductError", alertScript, true);
                return;
            }

            string imagenURL = string.IsNullOrWhiteSpace(txtImagenURL.Text) ? ImagenPorDefecto : txtImagenURL.Text;

            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta;

                if (IdInsumo.HasValue) // Si estamos editando un producto
                {
                    consulta = @"
                        UPDATE Insumos
                        SET Nombre = @Nombre, ImagenURL = @ImagenURL, Precio = @Precio, Stock = @Stock
                        WHERE IdInsumo = @IdInsumo";

                    datos.setConsulta(consulta);
                    datos.setParametro("@IdInsumo", IdInsumo.Value);
                }
                else // Si estamos agregando un nuevo producto
                {
                    consulta = "INSERT INTO Insumos (Nombre, ImagenURL, Precio, Stock) VALUES (@Nombre, @ImagenURL, @Precio, @Stock)";
                    datos.setConsulta(consulta);
                }

                // Asignar los parámetros comunes
                datos.setParametro("@Nombre", txtNombre.Text);
                datos.setParametro("@ImagenURL", imagenURL);
                datos.setParametro("@Precio", decimal.Parse(txtPrecio.Text));
                datos.setParametro("@Stock", int.Parse(txtStock.Text));
                datos.ejecutarAccion();

                string mensaje = IdInsumo.HasValue ? "Producto actualizado exitosamente" : "Producto agregado exitosamente";
                string alertScript = $@"Swal.fire({{ title: '{mensaje}', icon: 'success', confirmButtonText: 'OK' }}).then((result) => {{ if (result.isConfirmed) {{ window.location.href = 'MenuStock.aspx'; }} }});";
                ClientScript.RegisterStartupScript(this.GetType(), "productoAgregado", alertScript, true);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al guardar el producto: " + ex.Message + "');</script>");
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
