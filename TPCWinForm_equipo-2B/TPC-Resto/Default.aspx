<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_Resto._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main class="main-container">
        <div class="login-container">
            <h2 class="login-title">Iniciar Sesión</h2>
            <div class="login-form">
                <div class="form-group">
                    <label class="form-label" for="txtEmail">Email:</label>
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="Email" CssClass="form-input" />
                </div>
                <div class="form-group">
                    <label class="form-label" for="txtContrasenia">Contraseña:</label>
                    <asp:TextBox ID="txtContrasenia" runat="server" TextMode="Password" placeholder="Contraseña" CssClass="form-input" />
                    
                    <asp:Label ID="lblErrorMessage" runat="server" Text="Correo o contraseña incorrectos." CssClass="error-message" Visible="false"></asp:Label>
                </div>


                <asp:Button ID="btnLogin" runat="server" Text="Ingresa" CssClass="login-button" OnClick="btnLogin_Click" />

            </div>
            <%--<p class="register-link">¿No tienes una cuenta? <a href="~/Registro">Registrate aquí</a></p>--%>
        </div>
    </main>

</asp:Content>
