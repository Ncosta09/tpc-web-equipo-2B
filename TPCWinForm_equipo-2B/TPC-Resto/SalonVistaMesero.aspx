﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalonVistaMesero.aspx.cs" Inherits="TPC_Resto.SalonVistaMesero" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <script type="text/javascript">
    function filterDropdown() {
        var input = document.getElementById("searchDropdown");
        var filter = input.value.toUpperCase();
        var dropdown = document.getElementById("<%= ddlInsumos.ClientID %>");
        var options = dropdown.getElementsByTagName("option");

        for (var i = 0; i < options.length; i++) {
            var optionText = options[i].text.toUpperCase();
            if (optionText.indexOf(filter) > -1) {
                options[i].style.display = "";
            } else {
                options[i].style.display = "none";
            }
        }
    }
    </script>

              <div class = "section1">

          <div class = "col-izq"> <% //salon con mesas %>
                  <div class="container-mesas">   

               <asp:Repeater ID="RepeaterMesasMeseros" runat="server">
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
                  <div class="grid">
                    <asp:GridView ID="gridInsumos" runat="server" AutoGenerateColumns="False" CssClass="gridInsumos">
                        <Columns>
                             <asp:BoundField DataField="IdDetalle" HeaderText="ID" Visible="false" SortExpression="IdDetalle" />
                            <asp:BoundField DataField="Insumo" HeaderText="Insumo" />
                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                            <asp:BoundField DataField="Precio Unitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                            <asp:BoundField DataField="Precio Total" HeaderText="Precio Total" DataFormatString="{0:C}" />
                              <asp:TemplateField>
                                <ItemTemplate>
                                      <asp:Button runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%#Eval("IdDetalle") %>' OnClick="btnEliminar_Click" cssClass="btn-eliminar"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                  </div>


                   <div class="row-btn">
                       <div class="botons">
                   <asp:Button ID="BtnAgregarInsumo" runat="server" Text="agregar insumo" OnClick="BtnAgregarInsumo_Click" OnClientClick="showModalInsumo(); return true;"/> <%// habre popup con insumos, se seleccionan y se agregan al gridview (todos sus valores) %>
                   <%--<asp:Button ID="BtnCerrarMesa" runat="server" Text="cerrar mesa" OnClick="BtnCerrarMesa_Click" />--%> <%// cierra la mesa %>
                       </div>
                       <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label> <% // total forzado para primera vista %>
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
                 <div class="filtrar-insumo">
                     <label class="search-text" for="searchDropdown">Filtrar insumo:</label>
                     <input type="text" id="searchDropdown" onkeyup="filterDropdown()" placeholder="Insumo...">
                 </div>
                <div class="container-insumos">
              <asp:DropDownList cssClass="ddlInsumos" ID="ddlInsumos" runat="server"></asp:DropDownList>
                <asp:TextBox cssClass="txtBoxInsumos" ID="txtCantidad" runat="server" TextMode="Number" oninput="this.value = this.value.replace(/[^1-9]/g, '')" Text="0"></asp:TextBox>
                 </div>

              <asp:Button ID="Insumos" runat="server" Text="Agregar Insumo" OnClick="Insumos_Click"/>
            </div>
          </div>


</asp:Content>
