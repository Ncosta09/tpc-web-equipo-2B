<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="TPC_Resto.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<main class="main-container">
    <div class="form-container">
        <h2 class="form-title">Registro</h2>
        <div class="form">
            <div class="form-group">
                <label class="form-label" for="txtNombre">Nombre:</label>
                 <asp:TextBox ID="txtNombre" runat="server" placeholder="Nombre" CssClass="form-input" />
            </div>
            <div class="form-group">
                <label class="form-label" for="txtApellido">Apellido:</label>
                <asp:TextBox ID="txtApellido" runat="server" placeholder="Apellido" CssClass="form-input" />
            </div>
            <div class="form-group">
                <label class="form-label" for="txtDni">DNI:</label>
                <asp:TextBox ID="txtDni" runat="server" placeholder="Documento" CssClass="form-input" />
            </div>
            <div class="form-group">
                <label class="form-label" for="txtEmail">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="Email" CssClass="form-input" />
            </div>

            <div class="form-group">
                <label class="form-label" for="txtContrasenia">Contraseña:</label>
                <asp:TextBox ID="txtContrasenia" runat="server" TextMode="Password" placeholder="Contraseña" CssClass="form-input" />
            </div>

            <div class="form-group">
                <label class="form-label" for="txtRepetirContrasenia">Contraseña:</label>
                <asp:TextBox ID="txtRepetirContrasenia" runat="server" TextMode="Password" placeholder="Contraseña" CssClass="form-input" />
            </div>

            <asp:Button ID="btnRegistro" runat="server" Text="Registrate" CssClass="form-button" OnClick="btnRegistro_Click" />

        </div>
    </div>
</main>



</asp:Content>
