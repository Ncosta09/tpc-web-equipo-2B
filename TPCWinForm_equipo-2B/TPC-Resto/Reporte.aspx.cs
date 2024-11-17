using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace TPC_Resto
{
    public partial class Reporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Seguridad.sesionIniciada(Session["usuario"]))
            //{
            //    Response.Redirect("Default.aspx", false);
            //}

            //if (!Seguridad.esAdmin(Session["usuario"]))
            //{
            //    Response.Redirect("HomeMenu.aspx", false);
            //}

            if (!IsPostBack)
            {
                CargarGrafico();
            }
        }

        private void CargarGrafico()
        {
            var datos = new Charts();
            var listaGanancias = datos.ObtenerGananciasDiarias();

            // Configurar el Chart
            ChartGanancias.Series[0].Points.DataBindXY(
                listaGanancias.Select(g => g.FechaCierre.ToString("yyyy-MM-dd")).ToArray(),
                listaGanancias.Select(g => g.PrecioTotalMesa).ToArray()
            );

            ChartGanancias.Series[0].ChartType = SeriesChartType.Column;
        }
    }
}