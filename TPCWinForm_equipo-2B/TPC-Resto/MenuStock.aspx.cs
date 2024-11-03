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
                // Consulta SQL para obtener los insumos
                string consulta = @"SELECT Nombre, Descripcion, Img, Stock FROM Insumos"; 
                //falta IMG en  DB!

                datos.setConsulta(consulta);
                datos.ejecutarLectura();

                // Crear una lista temporal para almacenar los datos de insumos
                var insumosList = new System.Data.DataTable();
                insumosList.Load(datos.Lector);

                // Enlazar el Repeater con los datos
                rptInsumos.DataSource = insumosList;
                rptInsumos.DataBind();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Response.Write("<script>alert('Error al cargar insumos: " + ex.Message + "');</script>");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
