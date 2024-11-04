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

            if (!IsPostBack)
            {

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3]
                {
                    new DataColumn("Insumo", typeof(string)),
                    new DataColumn("Cantidad", typeof(int)),
                    new DataColumn("Precio", typeof(decimal))
                });

                //datos forzados para mostrar el grid
                dt.Rows.Add("Insumo 1", 10, 100m);
                dt.Rows.Add("Insumo 2", 15, 450m);
                dt.Rows.Add("Insumo 3", 20, 340m);
                dt.Rows.Add("Insumo 4", 25, 520.25m);
                dt.Rows.Add("Insumo 5", 30, 213.44m);
                dt.Rows.Add("Insumo 6", 10, 100m);
                dt.Rows.Add("Insumo 7", 15, 450m);
                dt.Rows.Add("Insumo 8", 20, 340m);

                gridInsumos.DataSource = dt;
                gridInsumos.DataBind();
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showCard", "document.querySelector('.card').style.display = 'flex';", true);
            }

        }

        protected void btnAsignarMesero_Click(object sender, EventArgs e)
        {
                MesasSalon mesasSalon = new MesasSalon();
           
            string numeroMesaStr = Session["NumeroMesa"] as string;

            // Verificar que el número de mesa no sea nulo
            if (!string.IsNullOrEmpty(numeroMesaStr) && int.TryParse(numeroMesaStr, out int numeroMesa))
            {
              
                mesasSalon.ActualizarEstadoMesaUno(numeroMesa);
                listarMesas();
            }
        }

        protected void BtnCerrarMesa_Click(object sender, EventArgs e)
        {
            MesasSalon mesasSalon = new MesasSalon();

            string numeroMesaStr = Session["NumeroMesa"] as string;

            if (!string.IsNullOrEmpty(numeroMesaStr) && int.TryParse(numeroMesaStr, out int numeroMesa))
            {

                mesasSalon.ActualizarEstadoMesaCero(numeroMesa);
                listarMesas();
            }
        }
    }
}