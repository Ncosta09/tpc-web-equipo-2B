<%@ Page Title="Menu Stock" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="MenuStock.aspx.cs" Inherits="TPC_Resto.MenuStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>
    <script src="~/Scripts/Stock.js"></script>

    <div class="container mt-5">
        <h2 class="text-center mb-4">Listado de Insumos</h2>

        <!-- Formulario para agregar nuevo insumo -->
        <div class="card mb-4">
            <div class="card-header">
                Agregar Nuevo Insumo
            </div>
            <div class="card-body">
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control mb-2" Placeholder="Nombre del Insumo"></asp:TextBox>
                <asp:TextBox ID="txtImagenURL" runat="server" CssClass="form-control mb-2" Placeholder="URL de la Imagen"></asp:TextBox>
                <asp:TextBox ID="txtStock" runat="server" CssClass="form-control mb-2" Placeholder="Cantidad de Stock"></asp:TextBox>
                <asp:Button ID="btnAgregarInsumo" runat="server" CssClass="btn btn-primary" Text="Agregar Insumo" OnClick="btnAgregarInsumo_Click" />
            </div>
        </div>

        <!-- Carrusel de insumos -->
        <div id="carouselInsumos" class="carousel slide carousel-dark" data-ride="carousel">
            <div class="carousel-inner" id="carouselItems" runat="server"></div>

            <a class="carousel-control-prev" href="#carouselInsumos" role="button" data-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
            </a>
            <a class="carousel-control-next" href="#carouselInsumos" role="button" data-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="sr-only">Next</span>
            </a>
        </div>
    </div>
</asp:Content>