<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Salon.aspx.cs" Inherits="TPC_Resto.Salon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Salon</title>
    <link href="Content/Styles/salon.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class = "section1">

            <div class = "col-izq"> <% //salon con mesas %>

                <asp:Panel class="mesa" runat="server"> </asp:Panel>
                <asp:Panel class="mesa" runat="server"> </asp:Panel>
                <asp:Panel class="mesa" runat="server"> </asp:Panel>
                <asp:Panel class="mesa" runat="server"> </asp:Panel>
                <asp:Panel class="mesa" runat="server"> </asp:Panel>
                <asp:Panel class="mesa" runat="server"> </asp:Panel>
                <asp:Panel class="mesa" runat="server"> </asp:Panel>
                <asp:Panel class="mesa" runat="server"> </asp:Panel>
                <asp:Panel class="mesa" runat="server"> </asp:Panel>
                <asp:Panel class="mesa" runat="server"> </asp:Panel>
                <asp:Panel class="mesa" runat="server"> </asp:Panel>
                <asp:Panel class="mesa" runat="server"> </asp:Panel>

            </div>
            <div class = "col-der"> <% //cards mesero x mesa %>
                 <div class="card">
                     <div class="row">
                     <div class="row-mesa">
                     <asp:Label ID="mesa" runat="server" Text="mesa: "></asp:Label>
                     <asp:Label ID="numero" runat="server" Text="traigo numero mesa"></asp:Label>  <% // falta traer mesa con la seleccion de la mesa dandole click al panel %>
                     </div>
                     <div class="row-mesero">
                     <asp:Label ID="mesero" runat="server" Text="mesero: "></asp:Label>
                     <asp:Label ID="nombre" runat="server" Text="nombre mesero"></asp:Label> <% // falta traer mesero con la seleccion de la mesa dandole click al panel %>
                     </div>
                    </div>
                     
                     <%--<asp:ListView ID="lista_insumos" runat="server"></asp:ListView>--%>
                      <% //GRID CON COLUMNAS FORZADAS PARA PRIMERA ENTREGA VIEW FRONT %>
                    <div class="grid">
                        <asp:GridView ID="gridInsumos" runat="server" AutoGenerateColumns="false" CssClass="gridInsumos">
                            <Columns>
                                <asp:BoundField DataField="Insumo" HeaderText="Insumo" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
                            </Columns>
                        </asp:GridView>
                    </div>


                     <div class="row-btn">
                         <div class="botons">
                     <asp:Button ID="Button1" runat="server" Text="agregar insumo" /> <%// habre popup con insumos, se seleccionan y se agregan al gridview (todos sus valores) %>
                     <asp:Button ID="Button2" runat="server" Text="cerrar mesa" /> <%// cierra la mesa %>
                         </div>
                         <asp:Label ID="lblTotal" runat="server" Text="total = 1.623,69"></asp:Label> <% // total forzado para primera vista %>
                      </div>
                 </div>
            </div>
        </div>

    </form>
</body>
</html>