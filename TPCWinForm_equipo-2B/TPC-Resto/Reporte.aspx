<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reporte.aspx.cs" Inherits="TPC_Resto.Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="main-reports">
        <div class="menu-left">
            <ul>
                <li><a href="#">Reporte de Ganancias</a></li>
                <li><a href="#">Reporte de Insumos</a></li>
                <li><a href="#">Reporte de Personal</a></li>
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
                            <AxisY Title="Ganancia" Interval="200" Minimum="0" />
                            
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </div>
        </div>
    </main>

</asp:Content>