<%@ Page Title="Ventas" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="TPC_Resto.Ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center mb-4">Ventas Totales por Mesa</h2>
        <asp:Repeater ID="rptVentas" runat="server">
            <ItemTemplate>
                <div class="card mb-4">
                    <div class="card-header">
                        <h4>Mesa <%# Eval("NumeroMesa") %> - Total: $<%# Eval("Total") %></h4>
                    </div>
                    <div class="card-body">
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <th>Producto</th>
                                    <th>Cantidad</th>
                                    <th>Precio Unitario</th>
                                    <th>Total</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptDetalle" runat="server" DataSource='<%# Eval("DetalleVenta") %>'>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("Producto") %></td>
                                            <td><%# Eval("Cantidad") %></td>
                                            <td>$<%# Eval("PrecioUnitario") %></td>
                                            <td>$<%# Eval("TotalItem") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
