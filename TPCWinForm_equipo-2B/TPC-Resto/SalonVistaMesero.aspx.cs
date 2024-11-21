using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Resto
{
    public partial class SalonVistaMesero : System.Web.UI.Page
    {
        private void listarMesas()
        {
            // Obtener el usuario desde la sesión
            Usuario usuarioLogueado = (Usuario)Session["usuario"];

            // Verificar si el usuario está logueado
            if (usuarioLogueado != null)
            {
                // Ahora puedes pasar el ID del usuario a listarMesasMesero
                MesasSalon mesasSalon = new MesasSalon();
                RepeaterMesasMeseros.DataSource = mesasSalon.listarMesasMesero(usuarioLogueado.ID);
                RepeaterMesasMeseros.DataBind();
            }
            else
            {
                // Si no hay un usuario logueado, redirigir a la página de login o mostrar un mensaje de error
                Response.Redirect("Default.aspx", false);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.sesionIniciada(Session["usuario"]))
            {
                Response.Redirect("Default.aspx", false);
            }


            if (!IsPostBack)
            {
                listarMesas();
            }

        }
        protected void Mesa_Click(object sender, EventArgs e)
        {
            MeseroXMesa meseroMesa = new MeseroXMesa();
            MesasSalon mesasSalon = new MesasSalon();
            Button clickedButton = (Button)sender;
            string numeroMesa = clickedButton.Text;
            Session["NumeroMesa"] = numeroMesa;
            int estadoMesa = mesasSalon.ObtenerEstadoMesa(int.Parse(numeroMesa));

            if (estadoMesa == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "showModal();", true);
            }
            else
            {
                Usuario meseroAsignado = meseroMesa.ObtenerMeseroPorMesa(int.Parse(numeroMesa));
                if (meseroAsignado != null)
                {
                    ViewState["IdMeseroAsignado"] = meseroAsignado.ID;
                    numero.Text = numeroMesa;
                    nombre.Text = meseroAsignado.Nombre;
                }

                // Actualizo el grid con los detalles del pedido activo de la mesa seleccionada
                ActualizarGridInsumos(int.Parse(numeroMesa));

                ScriptManager.RegisterStartupScript(this, this.GetType(), "showCard", "document.querySelector('.card').style.display = 'flex';", true);
            }
        }


        //protected void btnAsignarMesero_Click(object sender, EventArgs e)
        //{
        //    MesasSalon mesasSalon = new MesasSalon();
        //    MeseroXMesa meseroMesa = new MeseroXMesa();
        //    string numeroMesaStr = Session["NumeroMesa"] as string;
        //    int idUsuario = int.Parse(ddlMeseros.SelectedValue);



        //    //Verifico que el número de mesa no sea nulo
        //    if (!string.IsNullOrEmpty(numeroMesaStr) && int.TryParse(numeroMesaStr, out int numeroMesa))
        //    {
        //        //Verifico que se haya seleccionado un mesero
        //        if (ddlMeseros.SelectedValue != "0")
        //        {
        //            //inserto mesero en la mesa (db)
        //            meseroMesa.InsertarMeseroMesa(idUsuario, numeroMesa);

        //            PedidosSalon pedidosSalon = new PedidosSalon();
        //            DateTime fechaInicio = DateTime.Now;
        //            int estado = 0;

        //            //Creacion de pedido
        //            pedidosSalon.RegistrarPedido(numeroMesa, idUsuario, fechaInicio, estado);

        //            //Actualizar estado de la mesa a 1 ocupada (db)
        //            mesasSalon.ActualizarEstadoMesaUno(numeroMesa);

        //            listarMesas();
        //        }
        //    }
        //}

        //protected void BtnCerrarMesa_Click(object sender, EventArgs e)
        //{
        //    MesasSalon mesasSalon = new MesasSalon();
        //    MeseroXMesa meseroMesa = new MeseroXMesa();
        //    PedidosSalon pedidosSalon = new PedidosSalon();

        //    int idMesa = Convert.ToInt32(Session["NumeroMesa"]);
        //    int idPedidoActivo = pedidosSalon.ObtenerPedidoActivoPorMesa(idMesa);

        //    string numeroMesaStr = Session["NumeroMesa"] as string;
        //    int idUsuario = (int)ViewState["IdMeseroAsignado"];

        //    if (!string.IsNullOrEmpty(numeroMesaStr) && int.TryParse(numeroMesaStr, out int numeroMesa))
        //    {

        //        //Traigo la lista con los totales de la mesa(db)
        //        List<decimal> totalesPedido = pedidosSalon.BuscarTotal(idPedidoActivo);
        //        //Sumo los totales para calcular el total de la mesa(db)
        //        decimal sumaTotales = totalesPedido.Sum();
        //        //Registro el total del pedido(db)
        //        pedidosSalon.RegistarTotal(sumaTotales, idPedidoActivo);
        //        //Cerrar pedido(db)
        //        pedidosSalon.CerrarPedido(idPedidoActivo);
        //        //Borro mesero de la mesa (db)
        //        meseroMesa.DeleteMeseroMesa(idUsuario, numeroMesa);
        //        //Actualizar estado de la mesa a 0 desocupada (db)
        //        mesasSalon.ActualizarEstadoMesaCero(numeroMesa);
        //        listarMesas();
        //    }
        //}

        protected void BtnAgregarInsumo_Click(object sender, EventArgs e)
        {
            Insumos insumos = new Insumos();

            ddlInsumos.DataSource = insumos.listarInsumos();
            ddlInsumos.DataTextField = "Nombre";
            ddlInsumos.DataValueField = "ID";
            ddlInsumos.DataBind();

            ddlInsumos.Items.Insert(0, new ListItem("Selecciona un insumo", "0"));

            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalInsumo", "showModalInsumo();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showCard", "document.querySelector('.card').style.display = 'flex';", true);
        }

        protected void Insumos_Click(object sender, EventArgs e)
        {
            if (ddlInsumos.SelectedValue != "0" && int.TryParse(txtCantidad.Text, out int cantidad) && cantidad > 0)
            {
                Insumos insumos = new Insumos();
                List<Insumo> listaInsumos = insumos.listarInsumos();

                string nombreInsumo = ddlInsumos.SelectedItem.Text;
                int insumoId = int.Parse(ddlInsumos.SelectedValue);

                Insumo insumoSeleccionado = listaInsumos.FirstOrDefault(i => i.ID == insumoId);
                if (insumoSeleccionado == null)
                {
                    return;
                }

                decimal precioUnitario = insumoSeleccionado.Precio;
                decimal precioTotal = precioUnitario * cantidad;
                int idMesa = Convert.ToInt32(Session["NumeroMesa"]);

                PedidosSalon pedidosSalon = new PedidosSalon();

                // Obtengo el pedido activo de la mesa
                int idPedidoActivo = pedidosSalon.ObtenerPedidoActivoPorMesa(idMesa);

                if (insumoSeleccionado.Stock < cantidad)
                {
                    string alertScript = "Swal.fire({ icon: 'error', title: 'Oops...', text: 'El stock es insuficiente.' });";
                    ClientScript.RegisterStartupScript(this.GetType(), "stockInsuficiente", alertScript, true);
                    return;
                }
                else
                {
                    // Registro el detalle de ese pedido
                    pedidosSalon.RegistrarDetallePedido(idPedidoActivo, insumoId, cantidad, precioUnitario, precioTotal);

                    // Actualizo el stock por detalle agregado
                    pedidosSalon.ModificarStock(insumoId, cantidad);

                    // Actualizo el gridInsumos con el nuevo pedido y sus detalles
                    ActualizarGridInsumos(idMesa);
                }
                // Reseteo el dropdown de insumos
                ddlInsumos.SelectedIndex = 0;
                txtCantidad.Text = "";
            }
            else
            {
                if (ddlInsumos.SelectedValue == "0")
                {
                    string alertScript = "Swal.fire({ icon: 'error', title: 'Oops...', text: 'Por favor, seleccione un insumo válido.' });";
                    ClientScript.RegisterStartupScript(this.GetType(), "insumoInvalido", alertScript, true);
                }
                else
                {
                    string alertScript = "Swal.fire({ icon: 'error', title: 'Oops...', text: 'Por favor ingrese una cantidad válida mayor a 0.' });";
                    ClientScript.RegisterStartupScript(this.GetType(), "ingresoInvalido", alertScript, true);
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "showCard", "document.querySelector('.card').style.display = 'flex';", true);
        }


        private void ActualizarGridInsumos(int numeroMesa)
        {
            PedidosSalon pedidosSalon = new PedidosSalon();

            // Obtener el pedido activo para la mesa
            int idPedidoActivo = pedidosSalon.ObtenerPedidoActivoPorMesa(numeroMesa);

            // Obtener detalles del pedido
            List<DetallePedido> detallesPedido = pedidosSalon.ObtenerDetallesPedido(idPedidoActivo);

            // Crear DataTable para cargar los detalles del pedido
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5]
            {
                new DataColumn("IdDetalle", typeof(int)),
                new DataColumn("Insumo", typeof(string)),
                new DataColumn("Cantidad", typeof(int)),
                new DataColumn("Precio Unitario", typeof(decimal)),
                new DataColumn("Precio Total", typeof(decimal))
            });

            decimal precioTotalMesa = 0;

            // Agregar los detalles del pedido al DataTable
            foreach (var detalle in detallesPedido)
            {
                dt.Rows.Add(detalle.ID, detalle.Insumo.Nombre, detalle.Cantidad, detalle.PrecioUnitario, detalle.PrecioTotal);
                precioTotalMesa += detalle.PrecioTotal;
            }

            // Enlazar el DataTable al GridView y mostrar el total
            gridInsumos.DataSource = dt;
            gridInsumos.DataBind();

            lblTotal.Text = "total = " + precioTotalMesa.ToString("C");

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

            PedidosSalon pedidosSalon = new PedidosSalon();
            Button btn = (Button)sender;
            int idDetallePedido = Convert.ToInt32(btn.CommandArgument);
            var resultado = pedidosSalon.ObtenerInsumoDetalle(idDetallePedido);

            int Insumo = resultado.IdInsumo;
            int cantidad = resultado.Cantidad;


            pedidosSalon.ModificarReestock(Insumo, cantidad);
            pedidosSalon.EliminarDetallePedido(idDetallePedido);

            int numeroMesa = Convert.ToInt32(Session["NumeroMesa"]);
            ActualizarGridInsumos(numeroMesa);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showCard", "document.querySelector('.card').style.display = 'flex';", true);
        }

    }
}
