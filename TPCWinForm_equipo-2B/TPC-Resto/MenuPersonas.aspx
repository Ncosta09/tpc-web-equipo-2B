<%@ Page Title="Menu Personas" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="MenuPersonas.aspx.cs" Inherits="TPC_Resto.MenuPersonas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center mb-4">Listado de Personas</h2>
        <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="false" CssClass="table table-striped">
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
</asp:Content>