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
            <div>
                <h2>Ganacias</h2>

                <asp:Chart ID="ChartGanancias" runat="server" Width="600px" Height="400px">
                    <Series>
                        <asp:Series Name="Ganancias" ChartType="Column"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>

            </div>

        </div>
    </main>

</asp:Content>