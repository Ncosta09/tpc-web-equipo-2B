<%@ Page Title="Menu Stock" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="MenuStock.aspx.cs" Inherits="TPC_Resto.MenuStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>
    <link href="~/Content/Styles/Stock.css" rel="stylesheet" />

    <div class="container mt-5">
        <h2 class="text-center mb-4">Recetas </h2>

        <!-- Barra de búsqueda -->
        <div class="input-group mb-4">
            <asp:TextBox ID="txtBusqueda" runat="server" CssClass="form-control" placeholder="Buscar recetas..." />
            <div class="input-group-append">
                <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary" Text="Buscar" OnClick="btnBuscar_Click" />
            </div>
        </div>

        <!-- Botón para redirigir a la página de ingreso de nueva recet -->
        <div class="text-center mb-4">
            <asp:Button ID="btnNuevoProducto" runat="server" CssClass="btn btn-primary" Text="Ingresar Nueva Receta" OnClick="btnNuevoProducto_Click" />
        </div>

        <!-- Tabla de insumos con estilo oscuro -->
  <div class="table-responsive">
    <asp:Table ID="insumosTable" runat="server" CssClass="table table-dark table-striped">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>Imagen</asp:TableHeaderCell>
            <asp:TableHeaderCell>Nombre</asp:TableHeaderCell>
            <asp:TableHeaderCell>Stock</asp:TableHeaderCell>
            <asp:TableHeaderCell>Precio</asp:TableHeaderCell>
            <asp:TableHeaderCell>Acción</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
</div>

        <!-- Controles de paginación -->
        <div class="d-flex justify-content-between">
            <asp:Button ID="btnAnterior" runat="server" CssClass="btn btn-secondary" Text="Anterior" OnClick="btnAnterior_Click" />
            <asp:Label ID="lblPaginaActual" runat="server" CssClass="align-self-center"></asp:Label>
            <asp:Button ID="btnSiguiente" runat="server" CssClass="btn btn-secondary" Text="Siguiente" OnClick="btnSiguiente_Click" />
        </div>
    </div>
</asp:Content>
