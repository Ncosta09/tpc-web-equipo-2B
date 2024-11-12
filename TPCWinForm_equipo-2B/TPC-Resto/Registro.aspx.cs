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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario nuevo = new Usuario();
                RegistroUsuarios negocio = new RegistroUsuarios();

                nuevo.Nombre = txtNombre.Text;
                nuevo.Apellido = txtApellido.Text;
                nuevo.Dni = txtDni.Text;
                nuevo.Email = txtEmail.Text;
                nuevo.Imagen = txtImagen.Text;
                nuevo.Contrasenia = txtContrasenia.Text;

                negocio.registroUsuario(nuevo);
                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}