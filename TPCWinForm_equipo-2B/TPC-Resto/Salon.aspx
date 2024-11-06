<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Salon.aspx.cs" Inherits="TPC_Resto.Salon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

            <div class = "section1">

            <div class = "col-izq"> <% //salon con mesas %>
                    <div class="container-mesas">   

                 <asp:Repeater ID="RepeaterMesas" runat="server">
                    <ItemTemplate>
                        <div>
                            <asp:Button class="mesa" runat="server" Text='<%# Eval("NumeroMesa") %>' OnClick="Mesa_Click"   BackColor='<%# Convert.ToInt32(Eval("Estado")) == 0 ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'   CommandArgument='<%# Eval("NumeroMesa") %>' />
                            <%--<%# Eval("NumeroMesa") %>  NUMERO A LA DERECHA DE LA MESA--%>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                </div>
            </div>
            <div class = "col-der"> <% //cards mesero x mesa %>
                 <div class="card">
                     <div class="row">
                     <div class="row-mesa">
                     <asp:Label ID="mesa" runat="server" Text="mesa: "></asp:Label>
                     <asp:Label ID="numero" runat="server" Text=""></asp:Label>  <% // falta traer mesa con la seleccion de la mesa dandole click al panel %>
                     </div>
                     <div class="row-mesero">
                     <asp:Label ID="mesero" runat="server" Text="mesero: "></asp:Label>
                     <asp:Label ID="nombre" runat="server" Text=""></asp:Label> <% // falta traer mesero con la seleccion de la mesa dandole click al panel %>
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
                     <asp:Button ID="BtnAgregarInsumo" runat="server" Text="agregar insumo" OnClick="BtnAgregarInsumo_Click"/> <%// habre popup con insumos, se seleccionan y se agregan al gridview (todos sus valores) %>
                     <asp:Button ID="BtnCerrarMesa" runat="server" Text="cerrar mesa" OnClick="BtnCerrarMesa_Click" /> <%// cierra la mesa %>
                         </div>
                         <asp:Label ID="lblTotal" runat="server" Text="total = 1.623,69"></asp:Label> <% // total forzado para primera vista %>
                      </div>
                 </div>
            </div>
        </div>
                    <%-- Modal --%>
            <div id="AgregarInsumoModal" class="modal">
              <div class="modal-content">
                <asp:Button class="close" runat="server" Text=&times; />
                <%--<span class="close"></span>--%>
                <h3>Agregar Insumo </h3>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

                <asp:DropDownList cssClass="ddlistMeseros" ID="ddlInsumos" runat="server"></asp:DropDownList>

                <asp:Button ID="Insumos" runat="server" Text="Agregar Insumo" OnClick="Insumos_Click"/>
              </div>
            </div>
                <%-- Modal --%>
            <div id="asignarMeseroModal" class="modal">
              <div class="modal-content">
                <asp:Button class="close" runat="server" Text=&times; />
                <%--<span class="close"></span>--%>
                <h3>Asignar Mesero</h3>
                <asp:Label ID="lblNumeroMesa" runat="server" Text=""></asp:Label>

                <asp:DropDownList cssClass="ddlistMeseros" ID="ddlMeseros" runat="server"></asp:DropDownList>

                <asp:Button ID="btnAsignarMesero" runat="server" Text="Asignar Mesero" OnClick="btnAsignarMesero_Click"/>
              </div>
            </div>
</asp:Content>
