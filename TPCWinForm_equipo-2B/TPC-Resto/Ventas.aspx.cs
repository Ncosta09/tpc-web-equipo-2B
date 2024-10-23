using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Negocio;

namespace TPC_Resto
{
    public partial class Ventas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //CargarVentas(); a desarrollar 

            }
        }
    }
}

