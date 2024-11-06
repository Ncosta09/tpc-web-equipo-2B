﻿using Dominio;
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
                // Obtengo el mesero asignado en la ddl y muestro en el card
                Usuario meseroAsignado = meseroMesa.ObtenerMeseroPorMesa(int.Parse(numeroMesa));
                if (meseroAsignado != null)
                {
                    ViewState["IdMeseroAsignado"] = meseroAsignado.ID;
                    numero.Text = numeroMesa; 
                    nombre.Text = meseroAsignado.Nombre; 
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showCard", "document.querySelector('.card').style.display = 'flex';", true);
            }

        }

        protected void btnAsignarMesero_Click(object sender, EventArgs e)
        {
                MesasSalon mesasSalon = new MesasSalon();
                MeseroXMesa meseroMesa = new MeseroXMesa();
            string numeroMesaStr = Session["NumeroMesa"] as string;
            int idUsuario = int.Parse(ddlMeseros.SelectedValue);


            // Verifico que el número de mesa no sea nulo
            if (!string.IsNullOrEmpty(numeroMesaStr) && int.TryParse(numeroMesaStr, out int numeroMesa))
            {
                // Verifico que se haya seleccionado un mesero
                if (ddlMeseros.SelectedValue != "0")
                {
                    //inserto mesero en la mesa (db)
                    meseroMesa.InsertarMeseroMesa(idUsuario, numeroMesa);

                    // Actualizar estado de la mesa a 1 ocupada (db)
                    mesasSalon.ActualizarEstadoMesaUno(numeroMesa);
                    listarMesas();
                }
            }
        }

        protected void BtnCerrarMesa_Click(object sender, EventArgs e)
        {
            MesasSalon mesasSalon = new MesasSalon();
            MeseroXMesa meseroMesa = new MeseroXMesa();

            string numeroMesaStr = Session["NumeroMesa"] as string;
            int idUsuario = (int)ViewState["IdMeseroAsignado"];

            if (!string.IsNullOrEmpty(numeroMesaStr) && int.TryParse(numeroMesaStr, out int numeroMesa))
            {
                // Borro mesero de la mesa (db)
                meseroMesa.DeleteMeseroMesa(idUsuario, numeroMesa);
                // Actualizar estado de la mesa a 0 desocupada (db)
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
        }

        protected void Insumos_Click(object sender, EventArgs e)
        {
            if (ddlInsumos.SelectedValue != "0")
            {
                string nombreInsumo = ddlInsumos.SelectedItem.Text;
                int cantidad = 1;
                decimal precio = 100; 

                DataTable dt = Session["InsumosDataTable"] as DataTable;
                if (dt == null)
                {
                    dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[3]
                    {
                new DataColumn("Insumo", typeof(string)),
                new DataColumn("Cantidad", typeof(int)),
                new DataColumn("Precio", typeof(decimal))
                    });
                }

                dt.Rows.Add(nombreInsumo, cantidad, precio);

                Session["InsumosDataTable"] = dt;

                gridInsumos.DataSource = dt;
                gridInsumos.DataBind();

                ddlInsumos.SelectedIndex = 0;
            }
        }
    }
}