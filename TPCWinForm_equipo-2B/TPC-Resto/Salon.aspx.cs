using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace TPC_Resto
{
    public partial class Salon : System.Web.UI.Page
    {
        private void listarMesas()
        {
            MesasSalon mesasSalon = new MesasSalon();
            RepeaterMesas.DataSource = mesasSalon.listarMesa();
            RepeaterMesas.DataBind();
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
            lblNumeroMesa.Text = "Mesa: " + numeroMesa;
            Session["NumeroMesa"] = numeroMesa;
            int estadoMesa = mesasSalon.ObtenerEstadoMesa(int.Parse(numeroMesa));

            ddlMeseros.DataSource = meseroMesa.listarMesero();
            ddlMeseros.DataTextField = "Nombre";
            ddlMeseros.DataValueField = "ID";
            ddlMeseros.DataBind();
            ddlMeseros.Items.Insert(0, new ListItem("Selecciona un mesero", "0"));

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


        protected void btnAsignarMesero_Click(object sender, EventArgs e)
        {
                MesasSalon mesasSalon = new MesasSalon();
                MeseroXMesa meseroMesa = new MeseroXMesa();
            string numeroMesaStr = Session["NumeroMesa"] as string;
            int idUsuario = int.Parse(ddlMeseros.SelectedValue);



            //Verifico que el número de mesa no sea nulo
            if (!string.IsNullOrEmpty(numeroMesaStr) && int.TryParse(numeroMesaStr, out int numeroMesa))
            {
                //Verifico que se haya seleccionado un mesero
                if (ddlMeseros.SelectedValue != "0")
                {
                    //inserto mesero en la mesa (db)
                    meseroMesa.InsertarMeseroMesa(idUsuario, numeroMesa);

                    PedidosSalon pedidosSalon = new PedidosSalon();
                    DateTime fechaInicio = DateTime.Now;
                    int estado = 0;

                    //Creacion de pedido
                    pedidosSalon.RegistrarPedido(numeroMesa, idUsuario, fechaInicio, estado);

                    //Actualizar estado de la mesa a 1 ocupada (db)
                    mesasSalon.ActualizarEstadoMesaUno(numeroMesa);

                    listarMesas();
                }
                else
                {
                    string alertScript = "Swal.fire({ icon: 'error', title: 'Oops...', text: 'Seleccione algun mesero' });";
                    ClientScript.RegisterStartupScript(this.GetType(), "meseroInvalido", alertScript, true);
                    return;
                }
            }
        }

        protected void BtnCerrarMesa_Click(object sender, EventArgs e)
        {
            MesasSalon mesasSalon = new MesasSalon();
            MeseroXMesa meseroMesa = new MeseroXMesa();
            PedidosSalon pedidosSalon = new PedidosSalon();
           
            int idMesa = Convert.ToInt32(Session["NumeroMesa"]);
            int idPedidoActivo = pedidosSalon.ObtenerPedidoActivoPorMesa(idMesa);

            string numeroMesaStr = Session["NumeroMesa"] as string;
            int idUsuario = (int)ViewState["IdMeseroAsignado"];

            if (!string.IsNullOrEmpty(numeroMesaStr) && int.TryParse(numeroMesaStr, out int numeroMesa))
            {

                //Traigo la lista con los totales de la mesa(db)
                List<decimal> totalesPedido = pedidosSalon.BuscarTotal(idPedidoActivo);
                //Sumo los totales para calcular el total de la mesa(db)
                decimal sumaTotales = totalesPedido.Sum();
                //Registro el total del pedido(db)
                pedidosSalon.RegistarTotal(sumaTotales, idPedidoActivo);
                //Cerrar pedido(db)
                pedidosSalon.CerrarPedido(idPedidoActivo);
                //Borro mesero de la mesa (db)
                meseroMesa.DeleteMeseroMesa(idUsuario, numeroMesa);
                //Actualizar estado de la mesa a 0 desocupada (db)
                mesasSalon.ActualizarEstadoMesaCero(numeroMesa);
                listarMesas();
            }
        }

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
                dt.Rows.Add(detalle.ID , detalle.Insumo.Nombre, detalle.Cantidad, detalle.PrecioUnitario, detalle.PrecioTotal);
                precioTotalMesa += detalle.PrecioTotal;
            }

            // Enlazar el DataTable al GridView y mostrar el total
            gridInsumos.DataSource = dt;
            gridInsumos.DataBind();

            lblTotal.Text = "total = " + precioTotalMesa.ToString("C");

        }

        protected void BtnCerrarMesaAlert_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalAlert", "showModalAlert();", true);
        }
        protected void BtnCerrarFacturar_Alert(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalFactura", "showModalFactura();", true);
        }

        private void EnviarCorreoResumen(dynamic resumen, string destinatario)
        {
            string emailRemitente = "sorteosutnfrgp@gmail.com";
            string passwordRemitente = "vndz ynfs siso lsrq";  // Asegúrate de usar una contraseña segura

            try
            {
                // Crear el correo
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(emailRemitente);
                correo.To.Add(destinatario);  // Correo del destinatario
                correo.Subject = $"Resumen de Pedido de Mesa {resumen.Mesa}";

                // Crear el cuerpo del correo (con formato HTML)
                StringBuilder cuerpo = new StringBuilder();
                cuerpo.AppendLine($"<h3>Resumen de Pedido de Mesa {resumen.Mesa}</h3>");
                cuerpo.AppendLine($"<p><strong>Total:</strong> {resumen.Total:C}</p>");
                cuerpo.AppendLine("<p><strong>Detalles:</strong></p>");
                cuerpo.AppendLine("<ul>");

                // Añadir los detalles del pedido de forma dinámica
                foreach (var detalle in resumen.Detalles)
                {
                    cuerpo.AppendLine($"<li>{detalle}</li>");
                }

                cuerpo.AppendLine("</ul>");

                correo.Body = cuerpo.ToString();
                correo.IsBodyHtml = true;  // Establecer el cuerpo como HTML

                // Configurar el cliente SMTP
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(emailRemitente, passwordRemitente);
                smtp.EnableSsl = true;

                // Enviar el correo
                smtp.Send(correo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCerrarFacturar_Click(object sender, EventArgs e)
        {
            MesasSalon mesasSalon = new MesasSalon();
            MeseroXMesa meseroMesa = new MeseroXMesa();
            PedidosSalon pedidosSalon = new PedidosSalon();

            int idMesa = Convert.ToInt32(Session["NumeroMesa"]);
            int idPedidoActivo = pedidosSalon.ObtenerPedidoActivoPorMesa(idMesa);

            string numeroMesaStr = Session["NumeroMesa"] as string;
            int idUsuario = (int)ViewState["IdMeseroAsignado"];

            if (!string.IsNullOrEmpty(numeroMesaStr) && int.TryParse(numeroMesaStr, out int numeroMesa))
            {
                // Traigo la lista con los totales de la mesa(db)
                List<decimal> totalesPedido = pedidosSalon.BuscarTotal(idPedidoActivo);
                // Sumo los totales para calcular el total de la mesa(db)
                decimal sumaTotales = totalesPedido.Sum();
                // Registro el total del pedido(db)
                pedidosSalon.RegistarTotal(sumaTotales, idPedidoActivo);
                // Cerrar pedido(db)
                pedidosSalon.CerrarPedido(idPedidoActivo);
                // Borro mesero de la mesa (db)
                meseroMesa.DeleteMeseroMesa(idUsuario, numeroMesa);
                // Actualizo estado de la mesa a 0 desocupada (db)
                mesasSalon.ActualizarEstadoMesaCero(numeroMesa);

                // Obtener los detalles del pedido (varios productos o insumos)
                var detalles = pedidosSalon.ObtenerDetallesPedido(idPedidoActivo);
                List<string> detallesResumen = detalles.Select(d => $"{d.Cantidad} x {d.Insumo.Nombre} - {d.PrecioUnitario:C} c/u (Total: {d.PrecioTotal:C})").ToList();

                // Generar resumen del pedido
                var resumen = new
                {
                    Mesa = numeroMesa,
                    Total = sumaTotales,
                    Detalles = detallesResumen
                };

                // Enviar resumen por correo
                string correoCliente = txtCorreoCliente.Text.Trim();
                EnviarCorreoResumen(resumen, correoCliente);
                
                //string correoDestinatario = "ncosta981@gmail.com"; // Dirección de correo del destinatario
                //EnviarCorreoResumen(resumen, correoDestinatario);

                // Actualizar la vista o mostrar mensaje
                listarMesas();  // Asumiendo que esto refresca la lista de mesas
            }
        }

        //protected void gridInsumos_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Eliminar")
        //    {
        //        int rowIndex = Convert.ToInt32(e.CommandArgument);
        //        int idInsumo = Convert.ToInt32(gridInsumos.DataKeys[rowIndex].Values["IdInsumo"]); // Obtiene el ID del insumo
        //        int cantidad = Convert.ToInt32(gridInsumos.DataKeys[rowIndex].Values["Cantidad"]); // Obtiene la cantidad

        //        PedidosSalon pedidosNegocio = new PedidosSalon();
        //        try
        //        {
        //            // Elimina el detalle del pedido
        //            pedidosNegocio.EliminarDetallePedido(idInsumo, cantidad);

        //            // Actualiza el stock
        //            pedidosNegocio.ModificarStock(idInsumo, -cantidad);
        //        }
        //        catch (Exception ex)
        //        {
        //            // Manejo de errores
        //            throw ex;
        //        }
        //    }
        //}

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