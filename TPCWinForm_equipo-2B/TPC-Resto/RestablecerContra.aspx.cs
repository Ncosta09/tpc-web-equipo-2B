using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Resto
{
    public partial class RestablecerContra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los campos
            string email = txtEmailReset.Text.Trim();
            string nuevaContrasenia = txtNewPassword.Text.Trim();

            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(nuevaContrasenia))
            {
                lblResetMessage.Text = "Por favor, completa todos los campos.";
                lblResetMessage.CssClass = "error-message";
                lblResetMessage.Visible = true;
                return;
            }

            try
            {
                // Instanciar la capa de negocio
                LogInUsuario negocio = new LogInUsuario();

                // Llamar al método para cambiar la contraseña
                bool cambioExitoso = negocio.CambiarContrasenia(email, nuevaContrasenia);

                // Verificar si la contraseña se actualizó correctamente
                if (cambioExitoso)
                {
                    lblResetMessage.Text = "La contraseña se ha actualizado correctamente.";
                    lblResetMessage.CssClass = "success-message";
                    lblResetMessage.Visible = true;

                    // Limpiar los campos (opcional)
                    txtEmailReset.Text = string.Empty;
                    txtNewPassword.Text = string.Empty;
                }
                else
                {
                    lblResetMessage.Text = "No se pudo restablecer la contraseña. Verifique el correo electrónico.";
                    lblResetMessage.CssClass = "error-message";
                    lblResetMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores y notificación al usuario
                lblResetMessage.Text = "Ocurrió un error al intentar restablecer la contraseña. Intenta nuevamente.";
                lblResetMessage.CssClass = "error-message";
                lblResetMessage.Visible = true;

                // Loguear el error (opcional)
                Console.WriteLine(ex.Message);
            }
        }
    }
}