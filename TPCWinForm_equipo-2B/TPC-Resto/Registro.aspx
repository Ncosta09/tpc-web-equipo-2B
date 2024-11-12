<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="TPC_Resto.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<style>
    .invalido {
        background-color: #FF000033
    }
</style>


<main class="main-container">
    <div class="form-container">
        <h2 class="form-title">Registro</h2>
        <div class="form">
            <div class="form-group">
                <label class="form-label" for="txtNombre">Nombre:</label>
                <asp:TextBox ClientIDMode="Static" ID="txtNombre" runat="server" placeholder="Nombre" CssClass="form-input" />
            </div>
            <div class="form-group">
                <label class="form-label" for="txtApellido">Apellido:</label>
                <asp:TextBox ClientIDMode="Static" ID="txtApellido" runat="server" placeholder="Apellido" CssClass="form-input" />
            </div>
            <div class="form-group">
                <label class="form-label" for="txtDni">DNI:</label>
                <asp:TextBox ClientIDMode="Static" ID="txtDni" runat="server" placeholder="Documento" CssClass="form-input" />
            </div>
            <div class="form-group">
                <label class="form-label" for="txtEmail">Email:</label>
                <asp:TextBox ClientIDMode="Static" ID="txtEmail" runat="server" TextMode="Email" placeholder="Email" CssClass="form-input" />
            </div>
            <div class="form-group">
                <label class="form-label" for="txtImagen">Imagen:</label>
                <asp:TextBox ClientIDMode="Static" ID="txtImagen" runat="server" placeholder="Imagen" CssClass="form-input" />
            </div>
            <div class="form-group">
                <label class="form-label" for="txtContrasenia">Contraseña:</label>
                <asp:TextBox ClientIDMode="Static" ID="txtContrasenia" runat="server" TextMode="Password" placeholder="Contraseña" CssClass="form-input" />
            </div>

<%--            <div class="form-group">
                <label class="form-label" for="txtRepetirContrasenia">Contraseña:</label>
                <asp:TextBox ID="txtRepetirContrasenia" runat="server" TextMode="Password" placeholder="Contraseña" CssClass="form-input" />
            </div>--%>

            <asp:Button ID="btnRegistro" runat="server" Text="Registrate" CssClass="form-button" OnClientClick="return validarRegistro()" OnClick="btnRegistro_Click" />

        </div>
    </div>
</main>



</asp:Content>
