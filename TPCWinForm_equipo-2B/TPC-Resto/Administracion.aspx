<%@ Page Title="Administración" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Administracion.aspx.cs" Inherits="TPC_Resto.Administracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/Styles/Administracion.css" rel="stylesheet" />

    <div class="fullscreen-container">
        <div class="admin-card">
            <div class="admin-card-header">
                <h2>Panel de Administración</h2>
            </div>
            <div class="admin-card-body">
                <p>Elige una opción para gestionar el sistema:</p>
                
                <a href="MenuPersonas.aspx" class="btn btn-admin btn-admin-primary">Menú Personas</a>
                <a href="MenuStock.aspx" class="btn btn-admin btn-admin-info">Menú Stock</a>
                <a href="Ventas.aspx" class="btn btn-admin btn-admin-success">Ventas</a>
            </div>
            <div class="admin-card-footer">
                Gestión de Restaurante
            </div>
        </div>
    </div>
</asp:Content>