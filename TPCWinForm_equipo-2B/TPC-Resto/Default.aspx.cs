using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Resto
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            LogInUsuario negocio = new LogInUsuario();

            try
            {
                usuario.Email = txtEmail.Text;
                usuario.Contrasenia = txtContrasenia.Text;

                if (negocio.loguearse(usuario))
                {
                    Session.Add("usuario", usuario);
                    Response.Redirect("HomeMenu.aspx");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}