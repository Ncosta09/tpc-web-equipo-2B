using System;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_Resto
{
    public partial class MenuPersonas : System.Web.UI.Page
    {
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
                int offset = pageIndex * 8;

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

                // Verifica si hay más registros y actualiza el estado de los botones de paginación
                ActualizarEstadoBotones(pageIndex);
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

        private void ActualizarEstadoBotones(int pageIndex)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Consulta para contar cuántos registros quedan después de la página actual
                string consulta = @"
                    SELECT COUNT(*) 
                    FROM Usuarios";

                datos.setConsulta(consulta);
                int totalUsuarios = Convert.ToInt32(datos.ejecutarScalar());

                // Calcula el índice del último registro visible
                int totalMostrados = (pageIndex + 1) * 8;

                // Deshabilita el botón "Anterior" si estás en la primera página
                btnAnterior.Enabled = pageIndex > 0;

                // Deshabilita el botón "Siguiente" si no hay más registros para mostrar
                btnSiguiente.Enabled = totalMostrados < totalUsuarios;

                // Actualiza la etiqueta de la página actual
                lblPaginaActual.Text = $"Página {pageIndex + 1}";
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al actualizar estado de los botones: " + ex.Message + "');</script>");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            if (currentPageIndex > 0)
            {
                currentPageIndex--;
                CargarUsuarios(currentPageIndex);
            }
        }

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

                // Recalcula la página actual y recarga los datos
                if (currentPageIndex > 0 && gvUsuarios.Rows.Count == 1)
                {
                    currentPageIndex--;
                }
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string busqueda = txtBusqueda.Text.Trim();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
                    SELECT U.IdUsuario, U.Nombre, U.Apellido, R.Descripcion AS Rol, U.DNI
                    FROM Usuarios U
                    INNER JOIN Roles R ON U.IdRol = R.IdRol
                    WHERE U.Nombre LIKE @Busqueda OR U.Apellido LIKE @Busqueda
                    ORDER BY U.IdUsuario
                    OFFSET @Offset ROWS FETCH NEXT 8 ROWS ONLY";

                datos.setConsulta(consulta);
                datos.setParametro("@Busqueda", "%" + busqueda + "%");
                datos.setParametro("@Offset", currentPageIndex * 8);
                datos.ejecutarLectura();

                gvUsuarios.DataSource = datos.Lector;
                gvUsuarios.DataBind();

                ActualizarEstadoBotones(currentPageIndex);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al buscar usuarios: " + ex.Message + "');</script>");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
