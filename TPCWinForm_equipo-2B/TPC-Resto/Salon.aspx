﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Salon.aspx.cs" Inherits="TPC_Resto.Salon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        function validarCorreo() {
            var correo = document.getElementById('<%= txtCorreoCliente.ClientID %>').value.trim();
            if (!correo) {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Por favor, ingrese un correo válido."
                });
                return false;
            }
            return true;
        }
    </script>

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

    <div class="section1">

        <div class="col-izq">
            <% //salon con mesas %>
            <div class="container-mesas">

                <asp:Repeater ID="RepeaterMesas" runat="server">
                    <ItemTemplate>
                        <div>
                            <asp:Button class="mesa" runat="server" Text='<%# Eval("NumeroMesa") %>' OnClick="Mesa_Click" BackColor='<%# Convert.ToInt32(Eval("Estado")) == 0 ? System.Drawing.Color.Green : System.Drawing.Color.Red %>' CommandArgument='<%# Eval("NumeroMesa") %>' />
                            <%--<%# Eval("NumeroMesa") %>  NUMERO A LA DERECHA DE LA MESA--%>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
        </div>
        <div class="col-der">
            <% //cards mesero x mesa %>
            <div class="card">
                <div class="row">
                    <div class="row-mesa">
                        <asp:Label ID="mesa" runat="server" Text="mesa: "></asp:Label>
                        <asp:Label ID="numero" runat="server" Text=""></asp:Label>
                        <% // falta traer mesa con la seleccion de la mesa dandole click al panel %>
                    </div>
                    <div class="row-mesero">
                        <asp:Label ID="mesero" runat="server" Text="mesero: "></asp:Label>
                        <asp:Label ID="nombre" runat="server" Text=""></asp:Label>
                        <% // falta traer mesero con la seleccion de la mesa dandole click al panel %>
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
                        <asp:Button CssClass="boton-carrito" ID="BtnAgregarInsumo" runat="server" Text="agregar insumo" OnClick="BtnAgregarInsumo_Click" OnClientClick="showModalInsumo(); return true;" />
                        <%// habre popup con insumos, se seleccionan y se agregan al gridview (todos sus valores) %>
                        <asp:Button CssClass="boton-carrito" ID="BtnCerrarMesa" runat="server" Text="cerrar mesa"  OnClick="BtnCerrarMesaAlert_Click" OnClientClick="showModalAlert();"/>
                        <%// <asp:Button ID="BtnCerrarMesa" runat="server" Text="cerrar mesa" OnClick="BtnCerrarMesa_Click" />  cierra la mesa %>
                    </div>
                    <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                    <% // total forzado para primera vista %>
                </div>
            </div>
        </div>
    </div>

    <%-- Modal Insumo--%>
    <div id="AgregarInsumoModal" class="modal">
        <div class="modal-content">
            <asp:Button class="close" runat="server" Text="&times;" />
            <%--<span class="close"></span>--%>
            <h3>Agregar Insumo </h3>
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            <div class="filtrar-insumo">
                <label class="search-text" for="searchDropdown">Filtrar insumo:</label>
                <input type="text" id="searchDropdown" onkeyup="filterDropdown()" placeholder="Insumo...">
            </div>
            <div class="container-insumos">
                <asp:DropDownList CssClass="ddlInsumos" ID="ddlInsumos" runat="server"></asp:DropDownList>
                <asp:TextBox CssClass="txtBoxInsumos" ID="txtCantidad" runat="server" TextMode="Number" oninput="this.value = this.value.replace(/[^1-9]/g, '')" Text="0"></asp:TextBox>
            </div>

            <asp:Button CssClass="boton-carrito" ID="Insumos" runat="server" Text="Agregar Insumo" OnClick="Insumos_Click" />
        </div>
    </div>

    <%-- Modal Mesero --%>
    <div id="asignarMeseroModal" class="modal">
        <div class="modal-content">
            <asp:Button class="close" runat="server" Text="&times;" />
            <%--<span class="close"></span>--%>
            <h3>Asignar Mesero</h3>
            <asp:Label ID="lblNumeroMesa" runat="server" Text=""></asp:Label>
            <div class="ajustar-tamaño">
                <asp:DropDownList CssClass="ddlistMeseros tamanio-aparte-lista" ID="ddlMeseros" runat="server"></asp:DropDownList>
                <asp:Button CssClass="boton-carrito" ID="btnAsignarMesero" runat="server" Text="Asignar Mesero" OnClick="btnAsignarMesero_Click" />
            </div>
        </div>
    </div>

        <%-- Modal Alert --%>
    <div id="CierreMesa" class="modal tamanio-modal">
        <div class="modal-content">
            <div class="texto-botonera">
                <asp:Label ID="lblTextoBotonera" runat="server" Text="Seleccione una opcion!"></asp:Label>
            </div>
            <div class="botonera-modal">
                <asp:Button CssClass="boton-carrito" ID="btnCancelar" runat="server" Text="Cancelar" OnClientClick="closeModalAlert(); return false;" />
                <asp:Button CssClass="boton-carrito" ID="btnCerrarFacturar" runat="server" Text="Cerrar mesa y Enviar Factura" OnClick="BtnCerrarFacturar_Alert" OnClientClick="showModalFactura();"/>
                <asp:Button CssClass="boton-carrito" ID="btnCerrar" runat="server" Text="Cerrar mesa" OnClick="BtnCerrarMesa_Click"/>
            </div>
        </div>
    </div>

            <%-- Modal Enviar Factura --%>
    <div id="enviarFactura" class="modal">
        <div class="modal-content ">
            <asp:Button class="close" runat="server" Text="&times;" />
            <asp:Label ID="lblCorreoCliente" runat="server" Text="Correo Cliente:"></asp:Label>
            <div class="mail-container">
                <asp:TextBox CssClass="place-text" TextMode="Email" placeholder="Correo" ID="txtCorreoCliente" runat="server"></asp:TextBox>
                <asp:Button CssClass="boton-carrito" ID="Button2" runat="server" Text="Enviar" OnClick="btnCerrarFacturar_Click" OnClientClick="return validarCorreo();" />
            </div>
        </div>
    </div>
</asp:Content>
