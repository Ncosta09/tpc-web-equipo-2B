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
        protected void Page_Load(object sender, EventArgs e)
        {

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

                
                gridInsumos.DataSource = dt;
                gridInsumos.DataBind();
            }
        }
    }
}