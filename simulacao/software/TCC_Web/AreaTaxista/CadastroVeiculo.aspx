<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Taxista.master" AutoEventWireup="true" CodeBehind="CadastroVeiculo.aspx.cs" Inherits="TCC_Web.AreaTaxista.CadastroVeiculo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/AreaTaxista/Contrato.ascx" TagPrefix="ctrl" TagName="Contrato" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Informações do Veículo</h2>
    <table>
        <asp:UpdatePanel ID="updMarcaModelo" runat="server" UpdateMode="Always">
            <ContentTemplate>
            <tr>
                <td>Marca:</td>
                <td>
                    <asp:DropDownList ID="ddlMarca" runat="server" AutoPostBack="true" Width="150px"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvMarca" runat="server" Text="*" Display="Dynamic" ControlToValidate="ddlMarca" />
                </td>
            </tr>
            <tr>
                <td>Modelo:</td>
                <td>
                    <asp:DropDownList ID="ddlModelo" runat="server" Enabled="false" Width="150px"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvModelo" runat="server" Text="*" Display="Dynamic" ControlToValidate="ddlModelo" />
                </td>
            </tr>
            </ContentTemplate>
        </asp:UpdatePanel>
        <tr>
            <td>Ano Fab./Ano Mod.:</td>
            <td>
                <asp:TextBox ID="txtAnoFabMod" runat="server" Width="80px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAnoFabMod" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtAnoFabMod" />
                <asp:MaskedEditExtender ID="maskAnoFabMod" runat="server" Mask="9999/9999" PromptCharacter=" " 
                    TargetControlID="txtAnoFabMod" ClearMaskOnLostFocus="false"></asp:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td>Cor Predominante:</td>
            <td>
                <asp:TextBox ID="txtCor" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCor" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtCor" />
            </td>
        </tr>
        <tr>
            <td>Placa:</td>
            <td>
                <asp:TextBox ID="txtPlaca" runat="server"  Width="70px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPlaca" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtPlaca" />
                <asp:MaskedEditExtender ID="maskPlaca" runat="server" Mask="LLL-9999" PromptCharacter=" " TargetControlID="txtPlaca" ></asp:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td>Cod. Renavam:</td>
            <td>
                <asp:TextBox ID="txtRenavam" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvRenavam" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtRenavam" />
            </td>
        </tr>
    </table>

    <br />
    <h2>Informações do Responsável</h2>
    <table>
        <tr>
            <td>Autor. Tráfego (AT):</td>
            <td>
                <asp:TextBox ID="txtAutorizacaoTrafego" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAutorizacaoTrafego" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtAutorizacaoTrafego" />
            </td>
        </tr>
        <tr>
            <td>CNH:</td>
            <td>
                <asp:TextBox ID="txtCNH" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCNH" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtCNH" />
            </td>
        </tr>
        <tr>
            <td>Número Licença:</td>
            <td>
                <asp:TextBox ID="txtNumeroLicenca" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNumeroLicenca" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtNumeroLicenca" />
            </td>
        </tr>
    </table>

    <br />
    <h2>Informações do Rastreador</h2>
    <table>
        <tr>
            <td>Número Série:</td>
            <td>
                <asp:TextBox ID="txtNumeroSerie" runat="server" Width="250px"></asp:TextBox>
               <%-- <asp:MaskedEditExtender ID="maskNumeroSerie" runat="server" Mask="????????-????-????-????-????????????" 
                    PromptCharacter=" " TargetControlID="txtNumeroSerie"></asp:MaskedEditExtender>--%>
                <%--<asp:RequiredFieldValidator ID="rfvNumeroSerie" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtNumeroSerie" />--%>
            </td>
        </tr>
        <tr>
            <td>Código Segurança:</td>
            <td>
                <asp:TextBox ID="txtCodigoSeguranca" runat="server" Width="250px"></asp:TextBox>
                <%--<asp:MaskedEditExtender ID="maskCodigoSeguranca" runat="server" Mask="????????-????-????-????-????????????" 
                    PromptCharacter=" " TargetControlID="txtCodigoSeguranca"></asp:MaskedEditExtender>--%>
                <%--<asp:RequiredFieldValidator ID="rfvCodigoSeguranca" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtCodigoSeguranca" />--%>
            </td>
        </tr>
    </table>

    <br />
    <table id="tableContrato" runat="server">
        <tr>
            <td>
                <asp:UpdatePanel ID="updContrato" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:CheckBox ID="chkContrato" runat="server" Checked="false" AutoPostBack="true"
                            Text="Confirmo estar em acordo com os termos do contrato de uso de software, bem como valores e regras de funcionamento." />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <ctrl:Contrato id="ctrlContrato" runat="server" />
            </td>
        </tr>
    </table>

    <br />
    <table>
        <tr>
            <td>
                <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" />
            </td>
        </tr>
    </table>

    <br />
</asp:Content>
