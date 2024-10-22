﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HomeMenu.aspx.cs" Inherits="TPC_Resto.HomeMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="container">
        <div class="left-side">
            <a runat="server" class="button salon" href="~/Salon.aspx">Salon</a>
            <a runat="server" class="button administracion" href="~/Administracion.aspx">Administracion</a>
        </div>
        <div class="right-side">
            <img src="https://hebbkx1anhila5yf.public.blob.vercel-storage.com/image-8FxEChlQEA05xuMTZZvJ7SlHbK5hOi.png" alt="Escena de restaurante">
        </div>
    </main>

</asp:Content>