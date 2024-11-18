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
                            <AxisY Title="Ganancia" Interval="500" Minimum="0" />
                            
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </div>

            <div class="dashboard-two">
                <h2>Insumos</h2>

                <div class="insumo-list">

                </div>

                <div class="insumo-left">
                    <h3>Insumo mas vendido</h3>
                    <%--<asp:Image ID="Image1" runat="server" />--%>
                    <asp:Label runat="server" Text="Nombre:"></asp:Label>
                    <asp:Label ID="lblNombreMas" runat="server"></asp:Label>
                    <asp:Label runat="server" Text="Cantidad Vendidos:"></asp:Label>
                    <asp:Label ID="lblMasCantVendido" runat="server" ></asp:Label>
                </div>

                <div class="insumo-right">
                    <h3>Insumo menos vendido</h3>
                    <%--<asp:Image ID="Image1" runat="server" />--%>
                    <asp:Label runat="server" Text="Nombre:"></asp:Label>
                    <asp:Label ID="lblNombreMenos" runat="server"></asp:Label>
                    <asp:Label runat="server" Text="Cantidad Vendidos:"></asp:Label>
                    <asp:Label ID="lblMenosCantVendido" runat="server"></asp:Label>
                </div>

            </div>
        </div>
    </main>

</asp:Content>