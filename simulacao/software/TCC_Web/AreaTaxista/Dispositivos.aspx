<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Taxista.master" AutoEventWireup="true" CodeBehind="Dispositivos.aspx.cs" Inherits="TCC_Web.AreaTaxista.Dispositivos" %>
<%@ Register Src="~/AreaTaxista/TesteDispositivos.ascx" TagPrefix="ctrl" TagName="TesteDispositivos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Informações dos Dispositivos</h2>
    <table>
        <tr>
            <td><b>Número Série:</b></td>
            <td><asp:Label ID="lblNumeroSerie" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><b>Código Segurança:</b></td>
            <td><asp:Label ID="lblCodigoSeguranca" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2" style="padding:5px;"></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center;">
                <asp:Button ID="btnTestar" runat="server" Text="Testar" />
            </td>
        </tr>
    </table>
    <ctrl:TesteDispositivos id="ctrlTesteDispositivos" runat="server" />
</asp:Content>
