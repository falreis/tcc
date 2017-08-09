<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Taxista.master" AutoEventWireup="true" CodeBehind="Atendimentos.aspx.cs" Inherits="TCC_Web.AreaTaxista.Atendimentos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/AreaTaxista/DetalheAtendimentos.ascx" TagPrefix="ctrl" TagName="DetalheAtendimento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updAtendimentos" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <h2>Atendimentos</h2>
            <h3>Filtro:</h3>
            <table>
                <tr>
                    <td>Data:</td>
                    <td>
                        <asp:TextBox ID="txtDataInicial" runat="server" Width="80px"></asp:TextBox>
                        <asp:MaskedEditExtender ID="maskDataInicial" runat="server" Mask="99/99/9999" MaskType="Date"
                            PromptCharacter=" " TargetControlID="txtDataInicial" ClearMaskOnLostFocus="false" />
                        <asp:ImageButton runat="server" ID="btnDataInicial" ImageUrl="~/images/calendar.png" CausesValidation="false" />
                        <asp:CalendarExtender ID="calDataInicial" runat="server" TargetControlID="txtDataInicial" PopupButtonID="btnDataInicial" />
                        &nbsp;a&nbsp;
                        <asp:TextBox ID="txtDataFinal" runat="server" Width="80px"></asp:TextBox>
                        <asp:MaskedEditExtender ID="maskDataFinal" runat="server" Mask="99/99/9999" MaskType="Date"
                            PromptCharacter=" " TargetControlID="txtDataFinal" ClearMaskOnLostFocus="false" />
                        <asp:ImageButton runat="server" ID="btnDataFinal" ImageUrl="~/images/calendar.png" CausesValidation="false" />
                        <asp:CalendarExtender ID="calDataFinal" runat="server" TargetControlID="txtDataFinal" PopupButtonID="btnDataFinal" />
                    </td>
                </tr>
                <tr>
                    <td>Horário:</td>
                    <td>
                        <asp:TextBox ID="txtHoraInicial" runat="server" Width="100px"></asp:TextBox>
                        <asp:MaskedEditExtender ID="maskHoraInicial" runat="server" Mask="99:99:99" MaskType="Time"
                            PromptCharacter=" " TargetControlID="txtHoraInicial" ClearMaskOnLostFocus="false" />
                        &nbsp;a&nbsp;
                        <asp:TextBox ID="txtHoraFinal" runat="server" Width="100px"></asp:TextBox>
                        <asp:MaskedEditExtender ID="maskHoraFinal" runat="server" Mask="99:99:99" MaskType="Time"
                            PromptCharacter=" " TargetControlID="txtHoraFinal" ClearMaskOnLostFocus="false" />
                    </td>
                </tr>
                <tr>
                    <td>Status:</td>
                    <td><asp:DropDownList ID="ddlStatus" runat="server"  Width="230px"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td colspan="2" style="padding:5px;"></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center;">
                        <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
                    </td>
                </tr>
            </table>

            <br />
        </ContentTemplate>
    </asp:UpdatePanel>

    <ctrl:DetalheAtendimento ID="ctrlDetalhe" runat="server" />
</asp:Content>
