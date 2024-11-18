<%@ Page Title="Menu Personas" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="MenuPersonas.aspx.cs" Inherits="TPC_Resto.MenuPersonas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>
    <link href="~/Content/Styles/MenuPersonas.css" rel="stylesheet" />

    <div class="container mt-5">
        <h2 class="text-center mb-4">Listado de Meseros</h2>

        <!-- Barra de búsqueda para usuarios -->
        <div class="filtro-container mb-4">
            <asp:TextBox ID="txtBusqueda" runat="server" CssClass="form-control mr-3" placeholder="Buscar meseros..." />
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-dark" OnClick="btnBuscar_Click" />
        </div>

        <!-- GridView personalizado -->
        <div class="table-responsive">
            <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-dark" PageSize="8">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                    <asp:BoundField DataField="Rol" HeaderText="Rol" />
                    <asp:BoundField DataField="DNI" HeaderText="DNI" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandArgument='<%# Eval("IdUsuario") %>' CssClass="btn btn-danger" OnClick="btnEliminar_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <!-- Controles de navegación -->
        <div class="pagination mt-4 d-flex justify-content-between">
            <asp:Button ID="btnAnterior" runat="server" CssClass="btn btn-dark" Text="Anterior" OnClick="btnAnterior_Click" />
            <asp:Label ID="lblPaginaActual" runat="server" CssClass="align-self-center font-weight-bold"></asp:Label>
            <asp:Button ID="btnSiguiente" runat="server" CssClass="btn btn-dark" Text="Siguiente" OnClick="btnSiguiente_Click" />
        </div>
    </div>
</asp:Content>