using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Resto
{
    public partial class Perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.sesionIniciada(Session["usuario"]))
            {
                Response.Redirect("Default.aspx", false);
            }

            mostrarUsuario();
        }

        private void mostrarUsuario()
        {
            UsuarioPerfil perfilId = new UsuarioPerfil();
            Usuario perfil = perfilId.perfilUsuario(Session["usuario"]);

            if (perfil != null)
            {
                lblNombre.Text = perfil.Nombre;
                lblApellido.Text = perfil.Apellido;
                lblDNI.Text = perfil.Dni;
                lblEmail.Text = perfil.Email;
                imgPerfil.ImageUrl = perfil.Imagen;
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }
    }
}