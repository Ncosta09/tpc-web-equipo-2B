<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reporte.aspx.cs" Inherits="TPC_Resto.Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .reports-dashboard > div {
            display: none;
        }

        .reports-dashboard > .active {
            display: flex;
        }

        .menu-left ul li a {
            cursor: pointer;
            text-decoration: none;
        }
    </style>

    <main class="main-reports">
        <div class="menu-left">
            <ul>
                <li><a>Reporte de Ganancias</a></li>
                <li><a>Reporte de Insumos</a></li>
                <li><a>Reporte de Personal</a></li>
            </ul>
        </div>
        <div class="reports-dashboard">
            <div class="dashboard-one">
                <h2>Ganacias</h2>

                <div class="buttonbox">
                    <asp:Button ID="btnDiario" runat="server" Text="Ganancia Diaria" CssClass="single-button" OnClick="CambiarGrafico" />
                    <asp:Button ID="btnMensual" runat="server" Text="Ganancia Mensual" CssClass="single-button" OnClick="CambiarGrafico" />
                    <asp:Button ID="btnAnual" runat="server" Text="Ganancia Anual" CssClass="single-button" OnClick="CambiarGrafico" />
                </div>

                <asp:Chart ID="ChartGanancias" runat="server" Width="800px" Height="600px">
                    <Series>
                        <asp:Series Name="Ganancias" ChartType="Column"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">

                            <AxisX Title="Fecha"/>
                            <AxisY Title="Ganancia" Interval="1000" Minimum="0" />
                            
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </div>

            <div class="dashboard-two">
                <h2>Insumos</h2>

                <div class="insumo-list">
                    <div class="top-sellers">
                        <h3>Top 3 Insumos Más Vendidos</h3>
                        <asp:GridView ID="gvMasVendidos" runat="server" CssClass="table-style" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="TotalVendido" HeaderText="Cantidad Vendida" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div class="least-sellers">
                        <h3>Top 3 Insumos Menos Vendidos</h3>
                        <asp:GridView ID="gvMenosVendidos" runat="server" CssClass="table-style" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="TotalVendido" HeaderText="Cantidad Vendida" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                
                <div class="insumo-list">
                    <div class="insumo-one">
                        <h3>Insumo mas vendido</h3>
                        <%--<asp:Image ID="Image1" runat="server" />--%>
                        <div class="order-list">
                            <asp:Label runat="server" Text="Nombre:"></asp:Label>
                            <asp:Label CssClass="negrita" ID="lblNombreMas" runat="server"></asp:Label>
                        </div>
                        <div class="order-list">
                            <asp:Label runat="server" Text="Cantidad Vendidos:"></asp:Label>
                            <asp:Label CssClass="negrita" ID="lblMasCantVendido" runat="server" ></asp:Label>
                        </div>
                    </div>

                    <div class="insumo-two">
                        <h3>Insumo menos vendido</h3>
                        <%--<asp:Image ID="Image1" runat="server" />--%>
                        <div class="order-list">
                            <asp:Label runat="server" Text="Nombre:"></asp:Label>
                            <asp:Label CssClass="negrita" ID="lblNombreMenos" runat="server"></asp:Label>
                        </div>
                        <div class="order-list">
                            <asp:Label runat="server" Text="Cantidad Vendidos:"></asp:Label>
                            <asp:Label CssClass="negrita" ID="lblMenosCantVendido" runat="server"></asp:Label>
                        </div>
                    </div>    
                </div>

            </div>

             <div class="dashboard-three">
                <h2>Mesero</h2>
                 <div class="mesero-dashboard">
                    <h3>Mesero que mas mesas atendio</h3>
                        <div class="profile-image-mesero">
                            <asp:Image ID="imgMesero" runat="server" AlternateText="Imagen de perfil" />
                        </div>
                        <div class="informacion-mesas-dashboard">
                            <asp:Label runat="server" Text="Nombre:" CssClass="txtLiso"></asp:Label>
                            <asp:Label ID="lblNombreMesero" runat="server" CssClass="txtCargado"></asp:Label>
                            <asp:Label runat="server" Text="Apellido:" CssClass="txtLiso"></asp:Label>
                            <asp:Label ID="lblApellidoMesero" runat="server" CssClass="txtCargado"></asp:Label>
                        </div>
                        <div class="info-cantidad-mesas-dashboard">
                            <asp:Label runat="server" Text="Cantidad de mesas atendidas:" CssClass="txtLiso"></asp:Label>
                            <asp:Label ID="lblCantidadMesas" runat="server" CssClass="txtMesas"></asp:Label>
                        </div>
                 </div>
            </div>
        </div>
    </main>

</asp:Content>