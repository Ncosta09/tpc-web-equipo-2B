using Dominio;
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
                CargarGrafico("diario");
            }
        }

        protected void CambiarGrafico(object sender, EventArgs e)
        {
            if (sender == btnDiario)
            {
                CargarGrafico("diario");
            }
            else if (sender == btnMensual)
            {
                CargarGrafico("mensual");
            }
            else if (sender == btnAnual)
            {
                CargarGrafico("anual");
            }
        }

        private void CargarGrafico(string tipo)
        {
            var datos = new Charts();
            List<Pedido> listaGanancias;

            switch (tipo)
            {
                case "diario":
                    listaGanancias = datos.ObtenerGananciasDiarias();

                    ChartGanancias.Series[0].Points.DataBindXY(
                        listaGanancias.Select(g => g.FechaCierre.ToString("dd-MM-yyyy")).ToArray(),
                        listaGanancias.Select(g => g.PrecioTotalMesa).ToArray()
                    );

                    ChartGanancias.Series[0].ChartType = SeriesChartType.Column;

                    break;

                case "mensual":
                    listaGanancias = datos.ObtenerGananciasMensuales();

                    ChartGanancias.Series[0].Points.DataBindXY(
                        listaGanancias.Select(g => g.FechaCierre.ToString("MM-yyyy")).ToArray(),
                        listaGanancias.Select(g => g.PrecioTotalMesa).ToArray()
                    );

                    ChartGanancias.Series[0].ChartType = SeriesChartType.Column;

                    break;

                case "anual":
                    listaGanancias = datos.ObtenerGananciasAnuales();

                    ChartGanancias.Series[0].Points.DataBindXY(
                        listaGanancias.Select(g => g.FechaCierre.ToString("yyyy")).ToArray(),
                        listaGanancias.Select(g => g.PrecioTotalMesa).ToArray()
                    );

                    ChartGanancias.Series[0].ChartType = SeriesChartType.Column;

                    break;

                default:    
                break;
            }
        }
    }
}