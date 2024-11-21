<%@ Page Title="Ingrese Nuevo Producto" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="IngreseNuevoProducto.aspx.cs" Inherits="TPC_Resto.IngreseNuevoProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Styles/Header.css" rel="stylesheet" />
    <link href="Content/Styles/Footer.css" rel="stylesheet" />

    <div class="container mt-5" style="max-width: 500px;">
        <h2 class="text-center mb-4 text-secondary">Ingrese Nuevo Producto</h2>
        
        <div class="card shadow-sm">
            <div class="card-body">
                <div class="form-group">
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control mb-3" Placeholder="Nombre del Producto"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtImagenURL" runat="server" CssClass="form-control mb-3" Placeholder="URL de la Imagen"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control mb-3" Placeholder="Precio del Producto"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtStock" runat="server" CssClass="form-control mb-3" Placeholder="Cantidad de Stock"></asp:TextBox>
                </div>
                <div class="text-center">
                    <asp:Button ID="btnGuardarProducto" runat="server" CssClass="btn btn-dark btn-block" Text="Guardar Producto" OnClick="btnGuardarProducto_Click" />
                </div>
            </div>
        </div>

        <div class="text-center mt-3">
            <asp:Button ID="btnRegresar" runat="server" CssClass="btn btn-secondary" Text="Regresar al Menú de Stock" OnClick="btnRegresar_Click" />
        </div>
    </div>
</asp:Content>