<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HomeMenu.aspx.cs" Inherits="TPC_Resto.HomeMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="contenedor">
        <div class="left-side">



             <%if (Negocio.Seguridad.esAdmin(Session["usuario"]))
               { %>
                    <a runat="server" class="boton salon" href="~/Salon.aspx">Salon</a>
                    <a runat="server" class="boton administracion" href="~/Administracion.aspx">Administracion</a>
                    <a runat="server" class="boton reporte" href="~/Reporte.aspx">Reportes</a>
             <%}
               else
               { %>
                    <a runat="server" class="boton salon" href="~/SalonVistaMesero.aspx">Salon</a>
                    <a runat="server" class="boton NoAdministracion">Administracion</a>
                    <a runat="server" class="boton NoReporte">Reportes</a>
             <%} %>

        </div>
        <div class="right-side">
            <img src="https://img.freepik.com/foto-gratis/camarero-feliz-sirviendo-comida-grupo-amigos-alegres-pub_637285-12525.jpg" alt="Escena mesero">
        </div>
    </main>

</asp:Content>
