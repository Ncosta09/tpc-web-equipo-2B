using System;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_Resto
{
    public partial class MenuPersonas : System.Web.UI.Page
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
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Consulta SQL para obtener los usuarios y sus roles
                string consulta = @"SELECT U.IdUsuario, U.Nombre, U.Apellido, R.Descripcion AS Rol, U.DNI
                                    FROM Usuarios U
                                    INNER JOIN Roles R ON U.IdRol = R.IdRol";

                datos.setConsulta(consulta);
                datos.ejecutarLectura();

                // Enlaza el lector con el GridView
                gvUsuarios.DataSource = datos.Lector;
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Response.Write("<script>alert('Error al cargar usuarios: " + ex.Message + "');</script>");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idUsuario = Convert.ToInt32(btn.CommandArgument);
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Consulta SQL para eliminar el usuario por su ID
                string consulta = "DELETE FROM Usuarios WHERE IdUsuario = @IdUsuario";
                datos.setConsulta(consulta);
                datos.setParametro("@IdUsuario", idUsuario);
                datos.ejecutarAccion();

                // Recargar el listado de usuarios después de la eliminación
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Response.Write("<script>alert('Error al eliminar usuario: " + ex.Message + "');</script>");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}