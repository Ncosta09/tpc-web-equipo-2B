using System;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_Resto
{
    public partial class MenuPersonas : System.Web.UI.Page
    {
        // Variable para controlar el índice de página actual
        private int currentPageIndex
        {
            get { return (int)(ViewState["PageIndex"] ?? 0); }
            set { ViewState["PageIndex"] = value; }
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
                CargarUsuarios(currentPageIndex);
            }
        }

        private void CargarUsuarios(int pageIndex)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                int offset = pageIndex * 15;

                // Consulta SQL con paginación manual
                string consulta = @"
                    SELECT U.IdUsuario, U.Nombre, U.Apellido, R.Descripcion AS Rol, U.DNI
                    FROM Usuarios U
                    INNER JOIN Roles R ON U.IdRol = R.IdRol
                    ORDER BY U.IdUsuario
                    OFFSET @Offset ROWS FETCH NEXT 8 ROWS ONLY";

                datos.setConsulta(consulta);
                datos.setParametro("@Offset", offset);
                datos.ejecutarLectura();

                gvUsuarios.DataSource = datos.Lector;
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar usuarios: " + ex.Message + "');</script>");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Método para el botón "Anterior"
        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            if (currentPageIndex > 0)
            {
                currentPageIndex--;
                CargarUsuarios(currentPageIndex);
            }
        }

        // Método para el botón "Siguiente"
        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            currentPageIndex++;
            CargarUsuarios(currentPageIndex);
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idUsuario = Convert.ToInt32(btn.CommandArgument);
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "DELETE FROM Usuarios WHERE IdUsuario = @IdUsuario";
                datos.setConsulta(consulta);
                datos.setParametro("@IdUsuario", idUsuario);
                datos.ejecutarAccion();

                CargarUsuarios(currentPageIndex);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al eliminar usuario: " + ex.Message + "');</script>");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}