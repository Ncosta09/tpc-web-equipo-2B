<%@ Page Title="Ventas" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="TPC_Resto.Ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>
   
    <div class="container mt-5">
        <h2 class="text-center mb-4">Ventas Totales por Pedido</h2>
        
        <asp:Repeater ID="rptVentas" runat="server">
            <ItemTemplate>
                <div class="card mb-4">
                    <div class="card-header">
                        <h4>Pedido <%# Eval("ID") %> - Total: $<%# Eval("PrecioTotalMesa") %></h4>
                        <p>Mesa <%# Eval("Mesa.NumeroMesa") %> - Fecha: <%# Eval("FechaInicio", "{0:dd/MM/yyyy}") %></p>
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

        <!-- Controles de paginación -->
        <div class="pagination mt-4">
            <asp:Button ID="btnPrev" runat="server" Text="Anterior" OnClick="btnPrev_Click" CssClass="btn btn-primary" />
            <asp:Button ID="btnNext" runat="server" Text="Siguiente" OnClick="btnNext_Click" CssClass="btn btn-primary" />
        </div>

    </div>
</asp:Content>
