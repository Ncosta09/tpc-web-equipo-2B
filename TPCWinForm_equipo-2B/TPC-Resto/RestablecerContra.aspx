<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RestablecerContra.aspx.cs" Inherits="TPC_Resto.RestablecerContra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <main class="main-container">
        <div class="login-container">
            <h2 class="login-title">Restablecer Contraseña</h2>
            <div class="login-form">
                <div class="form-group">
                    <label class="form-label" for="txtEmailReset">Email:</label>
                    <asp:TextBox ID="txtEmailReset" runat="server" TextMode="Email" placeholder="Email" CssClass="form-input" />
                </div>
                <div class="form-group">
                    <label class="form-label" for="txtNewPassword">Nueva Contraseña:</label>
                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" placeholder="Nueva Contraseña" CssClass="form-input" />
                </div>

                <asp:Button ID="btnReset" runat="server" Text="Restablecer Contraseña" CssClass="login-button" OnClick="btnReset_Click" />

                <asp:Label ID="lblResetMessage" runat="server" Text="" CssClass="error-message" Visible="false"></asp:Label>
            </div>
            <a href="Default.aspx" class="register-link">Volver al inicio de sesión</a>
        </div>
    </main>
</asp:Content>
