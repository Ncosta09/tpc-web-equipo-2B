<%@ Page Title="Menu Stock" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="MenuStock.aspx.cs" Inherits="TPC_Resto.MenuStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>
    <link href="~/Content/Styles/Stock.css" rel="stylesheet" />

    <div class="container mt-5">
        <h2 class="text-center mb-4">Recetas </h2>

        <!-- Botón para redirigir a la página de ingreso de nuevo producto -->
        <div class="text-center mb-4">
            <asp:Button ID="btnNuevoProducto" runat="server" CssClass="btn btn-primary" Text="Ingresar Nuevo Producto" OnClick="btnNuevoProducto_Click" />
        </div>

        <!-- Carrusel de insumos con tamaño mejorado -->
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