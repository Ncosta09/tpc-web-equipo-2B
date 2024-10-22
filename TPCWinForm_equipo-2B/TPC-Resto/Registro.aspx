<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="TPC_Resto.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<main class="main-container">
    <div class="form-container">
        <h2 class="form-title">Registro</h2>
        <form class="form" action="tu_ruta_de_registro" method="POST">
            <div class="form-group">
                <label class="form-label" for="nombre">Nombre:</label>
                <input class="form-input" type="text" id="nombre" name="nombre" required>
            </div>
            <div class="form-group">
                <label class="form-label" for="apellido">Apellido:</label>
                <input class="form-input" type="text" id="apellido" name="apellido" required>
            </div>
            <div class="form-group">
                <label class="form-label" for="dni">DNI:</label>
                <input class="form-input" type="text" id="dni" name="dni" required>
            </div>
            <div class="form-group">
                <label class="form-label" for="email">Email:</label>
                <input class="form-input" type="email" id="email" name="email" required>
            </div>
            <div class="form-group">
                <label class="form-label" for="password">Contraseña:</label>
                <input class="form-input" type="password" id="password" name="password" required>
            </div>
            <button class="form-button" type="submit">Registrarse</button>
        </form>
    </div>
</main>



</asp:Content>
