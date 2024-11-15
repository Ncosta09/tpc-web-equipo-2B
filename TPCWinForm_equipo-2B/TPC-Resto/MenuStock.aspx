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

        <!-- Tabla de insumos con estilo oscuro -->
        <div class="table-responsive">
            <table class="table table-dark table-striped">
                <thead>
                    <tr>
                        <th>Imagen</th>
                        <th>Nombre</th>
                        <th>Stock</th>
                        <th>Precio</th>
                    </tr>
                </thead>
                <tbody id="insumosTableBody" runat="server">
                    <!-- Filas generadas dinámicamente desde el código detrás -->
                </tbody>
            </table>
        </div>

        <!-- Controles de paginación -->
        <div class="d-flex justify-content-between">
            <asp:Button ID="btnAnterior" runat="server" CssClass="btn btn-secondary" Text="Anterior" OnClick="btnAnterior_Click" />
            <asp:Label ID="lblPaginaActual" runat="server" CssClass="align-self-center"></asp:Label>
            <asp:Button ID="btnSiguiente" runat="server" CssClass="btn btn-secondary" Text="Siguiente" OnClick="btnSiguiente_Click" />
        </div>
    </div>
</asp:Content>