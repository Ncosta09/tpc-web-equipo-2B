using System;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_Resto
{
    public partial class MenuStock : System.Web.UI.Page
    {
        private const int ProductosPorPagina = 6; // Cambiado a 6 por página
        private int paginaActual
        {
            get { return (int)(ViewState["paginaActual"] ?? 1); }
            set { ViewState["paginaActual"] = value; }
        }

        private string TerminoBusqueda
        {
            get { return (string)(ViewState["TerminoBusqueda"] ?? string.Empty); }
            set { ViewState["TerminoBusqueda"] = value; }
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

     
                CargarInsumos();
            
        }

        private void CargarInsumos()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                int offset = (paginaActual - 1) * ProductosPorPagina;
                string consulta = $@"
        SELECT IdInsumo, Nombre, ImagenURL, Stock, Precio
        FROM Insumos
        WHERE Nombre LIKE @busqueda
        ORDER BY Nombre
        OFFSET {offset} ROWS
        FETCH NEXT {ProductosPorPagina} ROWS ONLY";

                datos.setConsulta(consulta);
                datos.setParametro("@busqueda", "%" + TerminoBusqueda + "%");
                datos.ejecutarLectura();

                insumosTable.Rows.Clear(); // Limpiar las filas anteriores

                bool hayProductosPaginaActual = false; // Para verificar si hay productos en esta página

                while (datos.Lector.Read())
                {
                    hayProductosPaginaActual = true;

                    // Crear una nueva fila de tabla
                    TableRow fila = new TableRow();

                    // Imagen
                    TableCell celdaImagen = new TableCell();
                    celdaImagen.Text = $"<img src='{datos.Lector["ImagenURL"]}' alt='{datos.Lector["Nombre"]}' class='img-thumbnail' style='width: 50px; height: 50px;'>";
                    fila.Cells.Add(celdaImagen);

                    // Nombre
                    TableCell celdaNombre = new TableCell();
                    celdaNombre.Text = datos.Lector["Nombre"].ToString();
                    fila.Cells.Add(celdaNombre);

                    // Stock
                    TableCell celdaStock = new TableCell();
                    celdaStock.Text = datos.Lector["Stock"].ToString();
                    fila.Cells.Add(celdaStock);

                    // Precio
                    TableCell celdaPrecio = new TableCell();
                    celdaPrecio.Text = Convert.ToDecimal(datos.Lector["Precio"]).ToString("C2");
                    fila.Cells.Add(celdaPrecio);

                    // Eliminar
                    TableCell celdaEliminar = new TableCell();
                    Button btnEliminar = new Button
                    {
                        Text = "Eliminar",
                        CssClass = "btn btn-danger btn-sm",
                        CommandArgument = datos.Lector["IdInsumo"].ToString(),
                        OnClientClick = "return confirm('¿Estás seguro de eliminar esta receta?');"
                    };
                    btnEliminar.Click += BtnEliminar_Click;
                    celdaEliminar.Controls.Add(btnEliminar);
                    fila.Cells.Add(celdaEliminar);

                    // Modificar
                    TableCell celdaModificar = new TableCell();
                    Button btnModificar = new Button
                    {
                        Text = "Modificar",
                        CssClass = "btn btn-warning btn-sm",
                        CommandArgument = datos.Lector["IdInsumo"].ToString() // Asocia el ID del insumo
                    };
                    btnModificar.Click += BtnModificar_Click; // Vincula el evento dinámico
                    celdaModificar.Controls.Add(btnModificar);
                    fila.Cells.Add(celdaModificar);

                    // Agregar la fila a la tabla
                    insumosTable.Rows.Add(fila);
                }

                // Actualizar botones de navegación
                btnAnterior.Enabled = paginaActual > 1; // Deshabilitar si es la primera página
                btnSiguiente.Enabled = hayProductosPaginaActual; // Deshabilitar si no hay más productos

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

        //  fnc modificar
        private void BtnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnModificar = (Button)sender;
                string idInsumo = btnModificar.CommandArgument;
                Response.Redirect($"IngreseNuevoProducto.aspx?id={idInsumo}");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al redirigir: " + ex.Message + "');</script>");
            }
        }
        //  fnc eliminar
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            Button btnEliminar = (Button)sender;
            int idInsumo = int.Parse(btnEliminar.CommandArgument);

            EliminarReceta(idInsumo);
        }

        private void EliminarReceta(int idInsumo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "DELETE FROM Insumos WHERE IdInsumo = @idInsumo";
                datos.setConsulta(consulta);
                datos.setParametro("@idInsumo", idInsumo);
                datos.ejecutarAccion();
                CargarInsumos(); // Recargar la lista después de eliminar
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al eliminar receta: " + ex.Message + "');</script>");
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
            int offset = paginaActual * ProductosPorPagina;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = $@"
                 SELECT COUNT(1) 
                 FROM Insumos 
                 WHERE Nombre LIKE @busqueda";

                datos.setConsulta(consulta);
                datos.setParametro("@busqueda", "%" + TerminoBusqueda + "%");
                int totalProductos = Convert.ToInt32(datos.ejecutarScalar());

                if (offset < totalProductos) // Verifica si hay mas recetas que cargar 
                {
                    paginaActual++;
                    CargarInsumos();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al avanzar de página: " + ex.Message + "');</script>");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            TerminoBusqueda = txtBusqueda.Text.Trim();
            paginaActual = 1; // Reiniciar a la primera página en cada nueva búsqueda
            CargarInsumos();
        }
    }

}
