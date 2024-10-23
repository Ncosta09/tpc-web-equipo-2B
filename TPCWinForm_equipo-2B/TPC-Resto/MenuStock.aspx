<%@ Page Title="Menu Stock" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="MenuStock.aspx.cs" Inherits="TPC_Resto.MenuStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center mb-4">Listado de Insumos</h2>
        <div class="row">
            <asp:Repeater ID="rptInsumos" runat="server">
                <ItemTemplate>
                    <div class="col-md-4 mb-4">
                        <div class="card">
                            <img src='<%# Eval("Img") %>' class="card-img-top" alt="Imagen del insumo">
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                <p class="card-text"><%# Eval("Descripcion") %></p>
                                <p class="card-text"><strong>Stock:</strong> <%# Eval("Stock") %></p>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
