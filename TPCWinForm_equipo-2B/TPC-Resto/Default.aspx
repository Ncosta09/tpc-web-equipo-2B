<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_Resto._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<main class="main-container">
    <div class="login-container">
        <h2 class="login-title">Iniciar Sesión</h2>
        <form class="login-form" action="tu_ruta_de_login" method="POST">
            <div class="form-group">
                <label class="form-label" for="email">Email:</label>
                <input class="form-input" type="email" id="email" name="email" required>
            </div>
            <div class="form-group">
                <label class="form-label" for="password">Contraseña:</label>
                <input class="form-input" type="password" id="password" name="password" required>
            </div>
            <button class="login-button" type="submit">Iniciar Sesión</button>
        </form>
        <%--<p class="register-link">¿No tienes una cuenta? <a href="~/Registro">Registrate aquí</a></p>--%>
    </div>
</main>



</asp:Content>
