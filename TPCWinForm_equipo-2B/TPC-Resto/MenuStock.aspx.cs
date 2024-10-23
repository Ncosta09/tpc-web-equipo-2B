using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_Resto
{
    public partial class MenuStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //CargarInsumos(); a desarrollar 
                // friendly reminder mod DB para sumar IMG
            }
        }
    }
}