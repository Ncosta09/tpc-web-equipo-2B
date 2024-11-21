<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="TPC_Resto.Perfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <main class="main-container">
        <div class="profile-container">

            <div class="profile-image">
                <asp:Image ID="imgPerfil" runat="server" CssClass="profile-img" AlternateText="Imagen de perfil" />
            </div>

            <div class="profile-info">

                <div class="form-group-profile">
                    <asp:Label ID="lblNombre" runat="server" CssClass="profile-data profile-bold"></asp:Label>
                    <asp:Label ID="lblApellido" runat="server" CssClass="profile-data profile-bold"></asp:Label>
                </div>

                <div class="form-group-profile">
                    <label class="form-label profile-bold">DNI:</label>
                    <asp:Label ID="lblDNI" runat="server" CssClass="profile-data"></asp:Label>
                </div>

                <div class="form-group-profile">
                    <label class="form-label profile-bold">Email:</label>
                    <asp:Label ID="lblEmail" runat="server" CssClass="profile-data"></asp:Label>
                </div>

            </div>

            <div class="profile-buttons">

                <%--<asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="profile-button" />--%>
                <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" CssClass="profile-button logout-button" OnClick="btnCerrarSesion_Click" />

            </div>
        </div>
    </main>

</asp:Content>
